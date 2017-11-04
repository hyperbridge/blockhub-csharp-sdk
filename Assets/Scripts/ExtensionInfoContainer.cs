#pragma warning disable 0168 // variable declared but not used.
#pragma warning disable 0219 // variable assigned but not used.
#pragma warning disable 0414 // private field assigned but not used.


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExtensionInfoContainer : MonoBehaviour
{

    public Image icon;
    public Text extensionName, extensionDate, extensionRating, extensionVersion;
    public Button installButton, uninstallButton;
    public ExtensionManagerView extensionManagerView;
    ExtensionInfo myExtensionInfo;
    
    
    /** Name
* Description
* Version
* Tags
* Rating
* Last Updated Date
* Number of installs*/
    public void SetupExtension(ExtensionInfo data)
    {
        myExtensionInfo = data;
      //  Debug.Log(data.name + data.descriptionText + data.updateDate + data.version+data.rating+data.installs+data.URL+data.imageURL    );
        
        extensionName.text = data.name;
        extensionDate.text = data.updateDate;
       // extensionRating.text = data.rating.ToString();
       // extensionVersion.text = data.version.ToString();


        GetComponent<Button>().onClick.AddListener(() =>
        {
            if(extensionManagerView == null)
            {
                extensionManagerView = FindObjectOfType<ExtensionManagerView>();

            }

            extensionManagerView.extensionInfoView.SetupView(data);
        });

        installButton.onClick.AddListener(() =>
        {
            extensionManagerView.InstallExtension(data);
        });

        uninstallButton.onClick.AddListener(() =>
        {
            extensionManagerView.UninstallExtension(data);
        });
    }

}
