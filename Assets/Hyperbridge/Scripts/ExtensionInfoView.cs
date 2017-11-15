using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExtensionInfoView : MonoBehaviour
{

    public Text extensionName, extensionInfo, lastUpdated, installs, tags, version, rating;
    public Image extensionImage;

    private void Start()
    {
    }

    public IEnumerator SetupView(ExtensionInfo data)
    {
        extensionName.text = data.name;
        extensionInfo.text = data.descriptionText;
        lastUpdated.text = "Last Updated: " + data.updateDate.ToString();
        installs.text = "Installs: " + data.installs.ToString();
        tags.text = "Tags: " + data.tags;
        version.text = "Version: " + data.version.ToString();
        rating.text = "Rating: " + data.rating.ToString();
        gameObject.SetActive(true);

        yield return new WaitForEndOfFrame();
        GetComponent<Devdog.General.UI.UIWindowPage>().Show();

    }
}
