using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Hyperbridge.Core;
using Hyperbridge.Profile;
public class SettingsManager : MonoBehaviour
{
    public Settings currentSettings;

    void Start()
    {
        CodeControl.Message.AddListener<UpdateSettingsEvent>(OnSettingsUpdated);
        CodeControl.Message.AddListener<ProfileInitializedEvent>(OnProfileInitialized);

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

        StartCoroutine(LoadSettings(e.activeProfile.uuid));
    }

    IEnumerator LoadSettings(string ID)
    {
        if (File.Exists("/Resources/Profiles/" + ID + "/Settings/settings.json"))
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
