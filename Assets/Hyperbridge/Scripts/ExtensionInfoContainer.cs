using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Devdog.General.UI;

public class ExtensionInfoContainer : MonoBehaviour
{

    public Image icon;
    public Text extensionName, extensionDate, extensionRating, extensionVersion;
    public Button installButton, disableButton;
    public ExtensionManagerView extensionManagerView;
    ExtensionInfo data;
    bool installed;


    /** Name
* Description
* Version
* Tags
* Rating
* Last Updated Date
* Number of installs*/
    public void SetupExtension(ExtensionInfo data, bool installed)
    {
        this.data = data;
        this.installed = installed;
        //  Debug.Log(data.name + data.descriptionText + data.updateDate + data.version+data.rating+data.installs+data.URL+data.imageURL    );
        extensionName.text = data.name;
        extensionDate.text = data.updateDate;
        // extensionRating.text = data.rating.ToString();
        // extensionVersion.text = data.version.ToString();


        GetComponent<Button>().onClick.AddListener(() =>
        {
            StartCoroutine(extensionManagerView.extensionInfoView.SetupView(data));
        });
        if (installed)
        {
            disableButton.interactable = true;

            installButton.GetComponentInChildren<Text>().text = "Uninstall";
            installButton.onClick.AddListener(() =>
            {
                StartCoroutine(AppManager.instance.modManager.UninstallMod(data));
                SetupExtension(this.data, false);
            });

        }
        else
        {
            disableButton.interactable = false;
            disableButton.GetComponentInChildren<Text>().text = "Not Installed";


            installButton.GetComponentInChildren<Text>().text = "Install";

            installButton.onClick.AddListener(() =>
            {
                StartCoroutine(AppManager.instance.modManager.InstallMod(data));
                SetupExtension(this.data, true);

            });

        }

        if (data.enabled)
        {
            disableButton.GetComponentInChildren<Text>().text = "Disable";

            disableButton.onClick.AddListener(() =>
            {
                disableButton.GetComponentInChildren<Text>().text = "Enable";

                AppManager.instance.modManager.DisableMod(data);
                SetupExtension(this.data, this.installed);
            });


        }
        else
        {

            disableButton.onClick.AddListener(() =>
            {
                disableButton.GetComponentInChildren<Text>().text = "Disable";

                AppManager.instance.modManager.EnableMod(data);
                SetupExtension(this.data, this.installed);

            });

        }

    }


}
