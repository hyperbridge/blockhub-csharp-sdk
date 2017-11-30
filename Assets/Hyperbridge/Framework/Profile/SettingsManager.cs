using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
        s.allowThirdPartyExtensions = e.allowThirdPartyExtensions;
        s.chromeDataAggregation = e.chromeDataAggregation;
        s.chromeExtensionIntegration = e.chromeExtensionIntegration;
        s.enableVPN = e.enableVPN;
        s.extensionSavingDirectory = e.extensionSavingDirectory;
        s.walletSavingDirectory = e.walletSavingDirectory;
        s.profileID = e.profileID;
        currentSettings = s;

        Database.SaveJSON<Settings>("/Resources/Profiles/" + e.profileID + "/Settings","settings", s);
    }

    void OnProfileUpdated(UpdateProfilesEvent e)
    {

        StartCoroutine(LoadSettings(e.activeProfile.uuid));
    }

    IEnumerator LoadSettings(string ID)
    {

        yield return currentSettings = Database.LoadJSONByName<Settings>("/Resources/Profiles/" + ID + "/Settings","settings");
        if (currentSettings == null) yield break;
        this.DispatchSettingsLoadedEvent();
    }

    void DispatchSettingsLoadedEvent()
    {
        SettingsLoadedEvent s = new SettingsLoadedEvent();
        s.allowThirdPartyExtensions = currentSettings.allowThirdPartyExtensions;
        s.chromeDataAggregation = currentSettings.chromeDataAggregation;
        s.chromeExtensionIntegration = currentSettings.chromeExtensionIntegration;
        s.enableVPN = currentSettings.enableVPN;
        s.extensionSavingDirectory = currentSettings.extensionSavingDirectory;
        s.walletSavingDirectory = currentSettings.walletSavingDirectory;
        s.profileID = currentSettings.profileID;

        CodeControl.Message.Send<SettingsLoadedEvent>(s);
    }
}
