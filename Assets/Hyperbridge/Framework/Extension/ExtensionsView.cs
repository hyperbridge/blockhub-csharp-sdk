#pragma warning disable 0168 // variable declared but not used.
#pragma warning disable 0219 // variable assigned but not used.
#pragma warning disable 0414 // private field assigned but not used.


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UMod;

public class ExtensionsView : MonoBehaviour
{
    public GameObject extensionInfoPrefab, extensionInfoOrganizer, installedExtensionsHeading, communityExtensionsHeading;
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

        CodeControl.Message.AddListener<AppInitializedEvent>(this.OnAppInitialized);
    }

    public void OnAppInitialized(AppInitializedEvent e)
    {
        this.activeRoutine = this.StartCoroutine(this.InitLoadExtensions());
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
                if (child.gameObject.GetComponent<ExtensionInfoContainer>() != null)
                {
                    Destroy(child.gameObject);
                }
            }
        }

        yield return StartCoroutine(this.extensionChecker.CheckExtensions(communityExtensions =>
        {
            AppManager.instance.modManager.extensionList.communityExtensions = communityExtensions;
        }, installedExtensions =>
        {
            AppManager.instance.modManager.extensionList.installedExtensions = installedExtensions;
        }));

        GenerateInstalledCommunityExtensionContainers();
        this.StartCoroutine(AppManager.instance.saveDataManager.SaveCurrentExtensionData());
        this.activeRoutine = null;
    }


    private void GenerateExtensionContainers(List<ExtensionInfo> extensions, bool installed)
    {
        foreach (ExtensionInfo extension in extensions)
        {
            GameObject container = Instantiate(this.extensionInfoPrefab, this.extensionInfoOrganizer.transform);

            container.GetComponent<ExtensionInfoContainer>().extensionsView = this;
            container.GetComponent<ExtensionInfoContainer>().SetupExtension(extension, installed);
            container.SetActive(true);
        }
    }

    public void GenerateInstalledCommunityExtensionContainers()
    {
        foreach (Transform child in this.extensionInfoOrganizer.transform)
        {
            if (child.name.Contains("Container"))
            {
                Destroy(child.gameObject);

            }
        }

        foreach (ExtensionInfo extension in AppManager.instance.modManager.extensionList.installedExtensions)
        {
            GameObject container = Instantiate(this.extensionInfoPrefab, this.extensionInfoOrganizer.transform);

            container.GetComponent<ExtensionInfoContainer>().extensionsView = this;
            container.GetComponent<ExtensionInfoContainer>().SetupExtension(extension, true);
            container.SetActive(true);
        }

        communityExtensionsHeading.transform.SetSiblingIndex(transform.parent.childCount - 1);

        foreach (ExtensionInfo extension in AppManager.instance.modManager.extensionList.communityExtensions)
        {
            bool found = false;
            foreach (ExtensionInfo ext in AppManager.instance.modManager.extensionList.installedExtensions)
            {
                if (ext.uuid == extension.uuid)
                {
                    found = true;
                }
                else
                {

                }
            }

            if (!found)
            {
                GameObject container = Instantiate(this.extensionInfoPrefab, this.extensionInfoOrganizer.transform);

                container.GetComponent<ExtensionInfoContainer>().extensionsView = this;
                container.GetComponent<ExtensionInfoContainer>().SetupExtension(extension, false);
                container.SetActive(true);

            }
        }

    }
}
