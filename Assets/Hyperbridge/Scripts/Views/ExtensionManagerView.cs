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
    public GameObject extensionInfoPrefab, extensionInfoOrganizer;
    public ExtensionInfoView extensionInfoView;

    private LoadData loader;
    private SaveData saver;
    private Coroutine activeRoutine;
    private ExtensionChecker extensionChecker;

    private void Awake()
    {
        this.loader = LoadData.LoadFromPath("/Resources/Extensions");
        this.saver = SaveData.SaveAtPath("/Resources/Extensions");
        this.extensionChecker = new ExtensionChecker();
        this.activeRoutine = StartCoroutine(this.InitLoadExtensions());
        this.extensionInfoView.gameObject.SetActive(true);
    }

    public void UpdateExtensionList()
    {
        if (this.activeRoutine != null)
            this.StopCoroutine(this.activeRoutine);

        this.activeRoutine = this.StartCoroutine(this.InitLoadExtensions());
    }

    private IEnumerator InitLoadExtensions()
    {
        if (this.extensionInfoOrganizer.transform.childCount > 0)
        {
            foreach (Transform child in this.extensionInfoOrganizer.transform)
            {
                if (child.gameObject.GetComponent<ExtensionInfoContainer>() != null) {
                    GameObject.Destroy(child.gameObject);
                }
            }
        }

        yield return StartCoroutine(this.extensionChecker.CheckExtensions(communityExtensions =>
        {
            AppManager.instance.modManager.extensionList.communityExtensions = communityExtensions;
            this.GenerateExtensionContainers(communityExtensions, false);
        }, installedExtensions => {
            AppManager.instance.modManager.extensionList.installedExtensions = installedExtensions;
            this.GenerateExtensionContainers(installedExtensions, true);
        }));

        this.StartCoroutine(AppManager.instance.saveDataManager.SaveCurrentExtensionData());
        this.activeRoutine = null;

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


    private void GenerateExtensionContainers(List<ExtensionInfo> extensions, bool installed)
    {
        foreach (ExtensionInfo extension in extensions)
        {
            GameObject container = Instantiate(this.extensionInfoPrefab, this.extensionInfoOrganizer.transform);
            container.GetComponent<ExtensionInfoContainer>().extensionManagerView = this;
            container.GetComponent<ExtensionInfoContainer>().SetupExtension(extension, installed);
            container.SetActive(true);
        }
    }

}
