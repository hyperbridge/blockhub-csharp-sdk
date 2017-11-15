#pragma warning disable 0168 // variable declared but not used.
#pragma warning disable 0219 // variable assigned but not used.
#pragma warning disable 0414 // private field assigned but not used.


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UMod;

public class ExtensionManagerView : MonoBehaviour
{

    List<ExtensionInfo> availableExtensions = new List<ExtensionInfo>();
    public GameObject extensionInfoPrefab, extensionInfoOrganizer;
    LoadData loader;
    Coroutine activeRoutine;
    public ExtensionInfoView extensionInfoView;
    ExtensionChecker extensionChecker;

    void Awake()
    {
        loader = LoadData.LoadFromPath("Extensions");
        extensionChecker = new ExtensionChecker();

        activeRoutine = StartCoroutine(LoadExtensions());
        extensionInfoView.gameObject.SetActive(true);
    }

    public void UpdateExtensionList()
    {
        if (activeRoutine != null) StopCoroutine(activeRoutine);

        activeRoutine = StartCoroutine(LoadExtensions());
    }

    IEnumerator LoadExtensions()
    {
        if (extensionInfoOrganizer.transform.childCount > 0)
        {
            foreach (Transform child in extensionInfoOrganizer.transform)
            {
                GameObject.Destroy(child.gameObject);
            }
        }

        availableExtensions = null;
       StartCoroutine( extensionChecker.CheckExternalExtensions(availableExtensions =>
        {
            GenerateExtensionContainers(availableExtensions);

        }));
        activeRoutine = null;
        yield return null;
      /*  List<ExtensionInfo> json = loader.LoadAllFromFolder<ExtensionInfo>();

        yield return json;
        foreach (ExtensionInfo extension in json)
        {
            if (Directory.Exists(Application.dataPath + "/Resources/Extensions/" + extension.name))
            {
                //If the mod is already in our files, we don't load anything
            }
            else
            {
                GameObject container = Instantiate(extensionInfoPrefab, extensionInfoOrganizer.transform);
                container.GetComponent<ExtensionInfoContainer>().extensionManagerView = this;
                container.GetComponent<ExtensionInfoContainer>().SetupExtension(extension);
                container.SetActive(true);
            }
          
        }

        activeRoutine = null;
        */

        /*<- This is for JSON when the JSON is online
      string url = RoomSettings.AbsoluteFilenamePath;

      if (Application.isEditor)
      {
          url = "file:///" + url;
      }

      var www = new WWW(url);
      yield return www;
      // Do some code, when file loaded*/ //<- This is for JSON when the JSON is online
    }
    void GenerateExtensionContainers(List<ExtensionInfo> extensions)
    {
        Debug.Log(extensions.Count);
        foreach (ExtensionInfo extension in extensions)
        {
            
                GameObject container = Instantiate(extensionInfoPrefab, extensionInfoOrganizer.transform);
                container.GetComponent<ExtensionInfoContainer>().extensionManagerView = this;
                container.GetComponent<ExtensionInfoContainer>().SetupExtension(extension);
                container.SetActive(true);
            

        }
    }

}
