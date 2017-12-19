using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Hyperbridge.Core;
using Hyperbridge.Profile;
public class SettingsManager : MonoBehaviour
{
    public Settings currentSettings;
    public ApplicationSettings applicationSettings;
    bool firstLoad;

    void Start()
    {
        firstLoad = true;
        CodeControl.Message.AddListener<UpdateSettingsEvent>(OnSettingsUpdated);
        CodeControl.Message.AddListener<ProfileInitializedEvent>(OnProfileInitialized);
        CodeControl.Message.AddListener<ApplicationSettingsUpdatedEvent>(OnApplicationSettingsUpdated);
        CodeControl.Message.AddListener<UpdateProfilesEvent>(OnProfilesUpdated);
        StartCoroutine(LoadApplicationSettings());
    }

    void OnProfilesUpdated(UpdateProfilesEvent e)
    {

        bool defaultProfileDeleted = true;
        bool activeProfileDeleted = true;
        foreach (ProfileData profile in e.profiles)
        {
            if(applicationSettings.defaultProfile != null)
            {
                if (profile.uuid == applicationSettings.defaultProfile.uuid)
                {
                    defaultProfileDeleted = false;
                }
            }
           
            if(applicationSettings.activeProfile != null)
            {
                if (profile.uuid == applicationSettings.activeProfile.uuid)
                {
                    activeProfileDeleted = false;
                }

            }
        
        }
        if (defaultProfileDeleted)
        {
            applicationSettings.defaultProfile = null;
            DispatchApplicationSettingsUpdatedEvent(applicationSettings);

        }
        if (activeProfileDeleted)
        {
            applicationSettings.activeProfile = null;
            DispatchApplicationSettingsUpdatedEvent(applicationSettings);

        }
    }
    void OnApplicationSettingsUpdated(ApplicationSettingsUpdatedEvent e)
    {
        if (e.applicationSettings.defaultProfile != null)
        {
            applicationSettings.defaultProfile = e.applicationSettings.defaultProfile;

        }
        if (e.applicationSettings.activeProfile != null)
        {
            applicationSettings.activeProfile = e.applicationSettings.activeProfile;

        }
        Database.SaveJSON<ApplicationSettings>("/Resources/Settings/", "applicationsettings", applicationSettings);

    }
    void OnSettingsUpdated(UpdateSettingsEvent e)
    {
        Settings s = new Settings();
        s = e.loadedSettings;

        Database.SaveJSON<Settings>("/Resources/Profiles/" + s.profileID + "/Settings", "settings", s);
        DispatchSettingsLoadedEvent();
    }

    void OnProfileInitialized(ProfileInitializedEvent e)
    {
        if (e.activeProfile == null) return;
        StartCoroutine(LoadSettings(e.activeProfile.uuid));
    }

    IEnumerator LoadApplicationSettings()
    {
        if (Database.CheckFileExistence(Application.dataPath + "/Resources/Settings/applicationsettings.json"))
        {
            yield return applicationSettings = Database.LoadJSONFile<ApplicationSettings>("/Resources/Settings/", "applicationsettings");
        }
        else
        {
            applicationSettings = new ApplicationSettings();
            applicationSettings.activeProfile = null;
            applicationSettings.defaultProfile = null;
        }
        DispatchApplicationSettingsUpdatedEvent(applicationSettings);

    }

    void DispatchApplicationSettingsUpdatedEvent(ApplicationSettings applicationSettings)
    {
        ApplicationSettingsUpdatedEvent message = new ApplicationSettingsUpdatedEvent { applicationSettings = this.applicationSettings, firstLoad = this.firstLoad };
        CodeControl.Message.Send<ApplicationSettingsUpdatedEvent>(message);
        firstLoad = false;
    }
    IEnumerator LoadSettings(string ID)
    {
        if (Database.CheckFileExistence(Application.dataPath + "/Resources/Profiles/" + ID + "/Settings/settings.json"))
        {
            yield return currentSettings = Database.LoadJSONByName<Settings>("/Resources/Profiles/" + ID + "/Settings", "settings");
            this.DispatchSettingsLoadedEvent();
        }
        else
        {
            Settings startingSettings = new Settings();
            startingSettings.allowThirdPartyExtensions = true;
            startingSettings.chromeDataAggregation = true;
            startingSettings.chromeExtensionIntegration = true;
            startingSettings.chromeExtensionVerified = false;
            startingSettings.enableVPN = false;
            startingSettings.extensionSavingDirectory = "Resources/Profiles/" + ID + "/Settings";
            startingSettings.walletSavingDirectory = Application.persistentDataPath + "/Profiles/" + ID + "/Wallets";
            currentSettings = startingSettings;
            this.DispatchSettingsLoadedEvent();

        }

        yield return null;
    }

    void DispatchSettingsLoadedEvent()
    {
        SettingsLoadedEvent message = new SettingsLoadedEvent();
        Settings s = currentSettings;

        message.loadedSettings = s;
        CodeControl.Message.Send<SettingsLoadedEvent>(message);
    }

}
