using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Hyperbridge.Extension;

public class ExtensionModalDetailsView : MonoBehaviour
{
    public Text extensionName, extensionInfo, lastUpdated, installs, tags, version, rating;
    public Image extensionImage;
    //TODO: Set a listener here? Maybe we could have a SetupView listener that passes all this stuff and all the DetailsView listen?
    public void SetupView(string name, string descriptionText, string lastUpdated, string installs, string tags, string version, string rating)
    {
        this.extensionName.text = name;
        this.extensionInfo.text = descriptionText;
        this.lastUpdated.text = "Last Updated: " + lastUpdated;
        this.installs.text = "Installs: " + installs;
        this.tags.text = "Tags: " + tags;
        this.version.text = "Version: " + version;
        this.rating.text = "Rating: " + rating;
        this.gameObject.SetActive(true);

        this.GetComponent<Devdog.General.UI.UIWindowPage>().Show();
    }
}
