using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Hyperbridge.Core;

public class MainSettingsView : MonoBehaviour
{

    public Toggle chromeExtensionIntegrationToggle, chromeDataAggregationToggle, VPNToggle, allowThirdPartyExtensionsToggle;
    public InputField walletSavingDirectoryDisplay, extensionSavingDirectoryDisplay;
    void Start()
    {
        CodeControl.Message.AddListener<UpdateSettingsEvent>(OnSettingsUpdated);
    }

    void OnSettingsUpdated(UpdateSettingsEvent e)
    {
        chromeExtensionIntegrationToggle.isOn = e.chromeExtensionIntegration;
        chromeDataAggregationToggle.isOn = e.chromeDataAggregation;
        VPNToggle.isOn = e.enableVPN;
        allowThirdPartyExtensionsToggle.isOn = e.allowThirdPartyExtensions;
        walletSavingDirectoryDisplay.text = e.walletSavingDirectory;
        extensionSavingDirectoryDisplay.text = e.extensionSavingDirectory;
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
