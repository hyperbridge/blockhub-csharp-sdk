using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Hyperbridge.Extension;

public class ExtensionInfoView : MonoBehaviour
{
    public Text extensionName, extensionInfo, lastUpdated, installs, tags, version, rating;
    public Image extensionImage;

    public IEnumerator SetupView(ExtensionInfo data)
    {
        Debug.Log("Showing Extension Info");

        this.extensionName.text = data.name;
        this.extensionInfo.text = data.descriptionText;
        this.lastUpdated.text = "Last Updated: " + data.updateDate.ToString();
        this.installs.text = "Installs: " + data.installs.ToString();
        this.tags.text = "Tags: " + data.tags;
        this.version.text = "Version: " + data.version.ToString();
        this.rating.text = "Rating: " + data.rating.ToString();
        this.gameObject.SetActive(true);

        yield return new WaitForEndOfFrame();

        this.GetComponent<Devdog.General.UI.UIWindowPage>().Show();
    }
}
