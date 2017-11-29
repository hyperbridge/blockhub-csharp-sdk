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
        CodeControl.Message.AddListener<UpdateSettingsEvent>(UpdateSettings);
        CodeControl.Message.AddListener<UpdateProfilesEvent>(LoadSettings);
    }
    //TODO: Saver will change
    void UpdateSettings(UpdateSettingsEvent e)
    {
        Debug.Log(e.profileID);
        SaveData saver = SaveData.SaveAtPath("/Resources/" + e.profileID + "/Settings");

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
    //TODO: Leader will change
    void LoadSettings(UpdateProfilesEvent e)
    {
        LoadData loader = LoadData.LoadFromPath("/Resources/" + e.activeProfile + "/Settings");

        currentSettings = loader.LoadJSONByName<Settings>("settings");

    }
}
