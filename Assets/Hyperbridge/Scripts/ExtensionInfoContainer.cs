using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Devdog.General.UI;

public class ExtensionInfoContainer : MonoBehaviour
{

    public Image icon;
    public Text extensionName, extensionDate, extensionRating, extensionVersion;
    public Button installButton, uninstallButton;
    public ExtensionManagerView extensionManagerView;
    ExtensionInfo data;


    /** Name
* Description
* Version
* Tags
* Rating
* Last Updated Date
* Number of installs*/
    public void SetupExtension(ExtensionInfo data)
    {
        this.data = data;
        //  Debug.Log(data.name + data.descriptionText + data.updateDate + data.version+data.rating+data.installs+data.URL+data.imageURL    );
        extensionName.text = data.name;
        extensionDate.text = data.updateDate;
        // extensionRating.text = data.rating.ToString();
        // extensionVersion.text = data.version.ToString();


        GetComponent<Button>().onClick.AddListener(() =>
        {
            StartCoroutine(extensionManagerView.extensionInfoView.SetupView(data));
        });

        installButton.onClick.AddListener(() =>
        {
            StartCoroutine(AppManager.instance.modManager.InstallMod(data.path, data.name));
        });

        uninstallButton.onClick.AddListener(() =>
        {
            //AppManager.instance.modManager.DeleteMod(data);
        });
    }

}
