using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Devdog.General.UI;

public class ExtensionInfoContainer : MonoBehaviour
{
    public Image icon;
    public Text extensionName, extensionDate, extensionRating, extensionVersion,descriptionText;
    public Button settingsButton, installButton, disableButton;
    public ExtensionManagerView extensionManagerView;

    private ExtensionInfo data;
    private bool installed;

    public void SetupExtension(ExtensionInfo data, bool installed)
    {
        this.data = data;
        this.installed = installed;
        this.descriptionText.text = data.descriptionText;
        this.extensionName.text = data.name;
        this.extensionDate.text = data.updateDate;
        this.extensionRating.text = data.rating.ToString();
        this.extensionVersion.text = data.version.ToString();

        this.settingsButton.onClick.AddListener(() =>
        {
            this.StartCoroutine(extensionManagerView.extensionInfoView.SetupView(data));
        });
        if (data.enabled)
        {
            this.disableButton.GetComponentInChildren<Text>().text = "Disable";

            this.disableButton.onClick.AddListener(() =>
            {
                this.disableButton.GetComponentInChildren<Text>().text = "Enable";

                AppManager.instance.modManager.DisableMod(data);
                this.SetupExtension(this.data, this.installed);
            });
        }
        else
        {
            this.disableButton.onClick.AddListener(() =>
            {
                this.disableButton.GetComponentInChildren<Text>().text = "Disable";

                AppManager.instance.modManager.EnableMod(data);
                this.SetupExtension(this.data, this.installed);
            });
        }
        if (this.installed)
        {
            this.disableButton.interactable = true;

            this.installButton.GetComponentInChildren<Text>().text = "Uninstall";
            this.installButton.onClick.AddListener(() =>
            {
                this.StartCoroutine(AppManager.instance.modManager.UninstallMod(data));
                this.SetupExtension(this.data, false);
            });
        }
        else
        {
            this.disableButton.image.color = new Color32(1,1,1,0);
            this.disableButton.GetComponentInChildren<Text>().text = "";

            this.installButton.GetComponentInChildren<Text>().text = "Install";

            this.installButton.onClick.AddListener(() =>
            {
                this.StartCoroutine(AppManager.instance.modManager.InstallMod(data));
                this.SetupExtension(this.data, true);
            });
        }

      
    }
}
