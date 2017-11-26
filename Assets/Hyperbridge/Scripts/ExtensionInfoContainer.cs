using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Devdog.General.UI;
using Hyperbridge.Extension;
using Hyperbridge.Core;

public class ExtensionInfoContainer : MonoBehaviour
{
    public Image icon;
    public Text extensionName, extensionDate, extensionRating, extensionVersion, descriptionText;
    public Button settingsButton, installButton, disableButton;
    public ExtensionsView extensionsView;

    private ExtensionInfo data;
    private bool isInstalled;
    private bool isEnabled;

    public void SetupExtension(ExtensionInfo data, bool installed)
    {
        this.data = data;
        this.isInstalled = installed;
        this.isEnabled = data.enabled;
        this.descriptionText.text = data.descriptionText;
        this.extensionName.text = data.name;
        this.extensionDate.text = data.updateDate;
        this.extensionRating.text = data.rating.ToString();
        this.extensionVersion.text = data.version.ToString();

        this.settingsButton.onClick.AddListener(() =>
        {
            this.StartCoroutine(extensionsView.extensionInfoView.SetupView(data));
        });

        this.disableButton.onClick.AddListener(() =>
        {
            this.isEnabled = !this.isEnabled;

            if (this.isEnabled)
            {
                AppManager.instance.modManager.EnableMod(data);
            }
            else
            {
                AppManager.instance.modManager.DisableMod(data);
            }

            this.UpdateButtonState();
        });

        this.installButton.onClick.AddListener(() =>
        {
            this.isInstalled = !this.isInstalled;

            if (this.isInstalled)
            {
                this.StartCoroutine(AppManager.instance.modManager.InstallMod(data));
            }
            else
            {

                this.StartCoroutine(AppManager.instance.modManager.UninstallMod(data));
            }

            this.UpdateButtonState();
        });

        this.UpdateButtonState();
    }

    public void UpdateButtonState()
    {
        if (this.isInstalled)
        {
            this.disableButton.gameObject.SetActive(true);
            this.settingsButton.gameObject.SetActive(true);

            this.installButton.GetComponentInChildren<Text>().text = "Uninstall";

            if (this.isEnabled)
            {
                this.disableButton.GetComponentInChildren<Text>().text = "Disable";
            }
            else
            {
                this.disableButton.GetComponentInChildren<Text>().text = "Enable";
            }
        }
        else
        {
            this.disableButton.gameObject.SetActive(false);
            this.settingsButton.gameObject.SetActive(false);

            this.installButton.GetComponentInChildren<Text>().text = "Install";
        }
    }
}
