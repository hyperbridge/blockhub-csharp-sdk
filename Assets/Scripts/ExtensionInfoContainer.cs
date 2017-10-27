using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExtensionInfoContainer : MonoBehaviour
{

    public Image icon;
    public Text extensionName, extensionDate, extensionRating, extensionVersion;
    ExtensionInformationView extensionInfoView;
    ExtensionInfo myExtensionInfo;
    // Use this for initialization
    void Start()
    {
        extensionInfoView = FindObjectOfType<ExtensionInformationView>();
    }
    
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
        if (data.iconSprite != null)
        {
            icon.sprite = data.iconSprite;
        }

        extensionName.text = data.name;
        extensionDate.text = data.updateDate;
        extensionRating.text = data.rating.ToString();
        extensionVersion.text = data.version.ToString();


        GetComponent<Button>().onClick.AddListener(() =>
        {
            if(extensionInfoView == null)
            {
                extensionInfoView = FindObjectOfType<ExtensionInformationView>();

            }
            extensionInfoView.SetupView(data);

        });
    }

}
