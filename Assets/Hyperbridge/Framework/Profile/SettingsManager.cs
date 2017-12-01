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
        CodeControl.Message.AddListener<UpdateProfilesEvent>(OnProfileUpdated);

    }



    void OnSettingsUpdated(UpdateSettingsEvent e)
    {

        Settings s = new Settings();
        s = e.loadedSettings;

        Database.SaveJSON<Settings>("/Resources/Profiles/" + s.profileID + "/Settings", "settings", s);
    }

    void OnProfileUpdated(UpdateProfilesEvent e)
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
        Settings s = new Settings();
        s.allowThirdPartyExtensions = currentSettings.allowThirdPartyExtensions;
        s.chromeDataAggregation = currentSettings.chromeDataAggregation;
        s.chromeExtensionIntegration = currentSettings.chromeExtensionIntegration;
        s.enableVPN = currentSettings.enableVPN;
        s.extensionSavingDirectory = currentSettings.extensionSavingDirectory;
        s.walletSavingDirectory = currentSettings.walletSavingDirectory;
        s.profileID = currentSettings.profileID;
        message.loadedSettings = s;
        CodeControl.Message.Send<SettingsLoadedEvent>(message);
    }
}
