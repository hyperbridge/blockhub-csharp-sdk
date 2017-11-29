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


    void UpdateView(bool chromeExtensionIntegration, bool chromeDataAggregation, bool enableVPN, bool allowThirdPartyExtensions, string walletPath, string extensionPath)
    {
        chromeExtensionIntegrationToggle.isOn = chromeExtensionIntegration;
        chromeDataAggregationToggle.isOn = chromeDataAggregation;
        VPNToggle.isOn = enableVPN;
        allowThirdPartyExtensionsToggle.isOn = allowThirdPartyExtensions;
        walletSavingDirectoryDisplay.text = walletPath;
        extensionSavingDirectoryDisplay.text = extensionPath;
    }

    void OnSettingsUpdated(UpdateSettingsEvent e)
    {
        UpdateView(e.chromeExtensionIntegration, e.chromeDataAggregation, e.enableVPN,
            e.allowThirdPartyExtensions, e.walletSavingDirectory,
            e.extensionSavingDirectory);

    }
    void OnSettingsLoaded(SettingsLoadedEvent e)
    {
        UpdateView(e.chromeExtensionIntegration, e.chromeDataAggregation, e.enableVPN,
            e.allowThirdPartyExtensions, e.walletSavingDirectory,
            e.extensionSavingDirectory);
    }

    public void DispatchSettingsUpdateEvent()
    {
        
        UpdateSettingsEvent message = new UpdateSettingsEvent();

        message.allowThirdPartyExtensions = allowThirdPartyExtensionsToggle.isOn;
        message.chromeDataAggregation = chromeDataAggregationToggle.isOn;
        message.chromeExtensionIntegration = chromeExtensionIntegrationToggle.isOn;
        message.enableVPN = VPNToggle.isOn;
        message.walletSavingDirectory = this.walletSavingDirectoryDisplay.text;
        message.extensionSavingDirectory = this.extensionSavingDirectoryDisplay.text;
        message.profileID = AppManager.instance.profileManager.activeProfile.uuid;
        CodeControl.Message.Send<UpdateSettingsEvent>(message);
    }
}
