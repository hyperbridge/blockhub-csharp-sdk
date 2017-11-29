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



    //TODO: Saver will change
    void OnSettingsUpdated(UpdateSettingsEvent e)
    {
        Debug.Log(e.profileID);
        SaveData saver = SaveData.SaveAtPath("/Resources/Profiles/" + e.profileID + "/Settings");

        Settings s = new Settings();
        s.allowThirdPartyExtensions = e.allowThirdPartyExtensions;
        s.chromeDataAggregation = e.chromeDataAggregation;
        s.chromeExtensionIntegration = e.chromeExtensionIntegration;
        s.enableVPN = e.enableVPN;
        s.extensionSavingDirectory = e.extensionSavingDirectory;
        s.walletSavingDirectory = e.walletSavingDirectory;
        s.profileID = e.profileID;
        currentSettings = s;

        saver.Save<Settings>("settings", s);
    }
    //TODO: Loader will change
    void OnProfileUpdated(UpdateProfilesEvent e)
    {

        StartCoroutine(LoadSettings(e.activeProfile.uuid));
    }

    IEnumerator LoadSettings(string ID)
    {
        LoadData loader = LoadData.LoadFromPath("/Resources/Profiles/" + ID + "/Settings");

        yield return currentSettings = loader.LoadJSONByName<Settings>("settings");
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
