using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Hyperbridge.Profile;
using Hyperbridge.Core;

public class MainSettingsView : MonoBehaviour
{

    public Toggle chromeExtensionIntegrationToggle, chromeDataAggregationToggle, VPNToggle, allowThirdPartyExtensionsToggle;
    public InputField walletSavingDirectoryDisplay, extensionSavingDirectoryDisplay;
    void Start()
    {
        CodeControl.Message.AddListener<UpdateSettingsEvent>(this.OnSettingsUpdated);
        CodeControl.Message.AddListener<SettingsLoadedEvent>(this.OnSettingsLoaded);
    }


    void UpdateView(Settings s)
    {
        chromeExtensionIntegrationToggle.isOn = s.chromeExtensionIntegration;
        chromeDataAggregationToggle.isOn = s.chromeDataAggregation;
        VPNToggle.isOn = s.enableVPN;
        allowThirdPartyExtensionsToggle.isOn = s.allowThirdPartyExtensions;
        walletSavingDirectoryDisplay.text = s.walletSavingDirectory;
        extensionSavingDirectoryDisplay.text = s.extensionSavingDirectory;
    }

    void OnSettingsUpdated(UpdateSettingsEvent e)
    {
        UpdateView(e.loadedSettings);

    }
    void OnSettingsLoaded(SettingsLoadedEvent e)
    {
        UpdateView(e.loadedSettings);
    }

    public void DispatchSettingsUpdateEvent()
    {
        
        UpdateSettingsEvent message = new UpdateSettingsEvent();
        Settings s = new Settings();
        s.allowThirdPartyExtensions = allowThirdPartyExtensionsToggle.isOn;
        s.chromeDataAggregation = chromeDataAggregationToggle.isOn;
        s.chromeExtensionIntegration = chromeExtensionIntegrationToggle.isOn;
        s.enableVPN = VPNToggle.isOn;
        s.walletSavingDirectory = this.walletSavingDirectoryDisplay.text;
        s.extensionSavingDirectory = this.extensionSavingDirectoryDisplay.text;
        s.profileID = AppManager.instance.profileManager.activeProfile.uuid;
        message.loadedSettings = s;
        CodeControl.Message.Send<UpdateSettingsEvent>(message);
    }
}
