using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExtensionInfoContainer : MonoBehaviour
{

    public Image icon;
    public Text extensionName, extensionDate, extensionRating, extensionVersion;
    public ExtensionInformationView extensionInfoView;
    ExtensionInfo myExtensionInfo;
    // Use this for initialization
    void Start()
    {
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
        Debug.Log(data.name + data.descriptionText + data.updateDate + data.version+data.rating+data.installs+data.URL+data.imageURL    );
        

        extensionName.text = data.name;
        extensionDate.text = data.updateDate;
       // extensionRating.text = data.rating.ToString();
       // extensionVersion.text = data.version.ToString();


        GetComponent<Button>().onClick.AddListener(() =>
        {
            if(extensionInfoView == null)
            {
                extensionInfoView = FindObjectOfType<ExtensionManagerView>().extensionInfoView;

            }
            extensionInfoView.SetupView(data);

        });
    }

}
