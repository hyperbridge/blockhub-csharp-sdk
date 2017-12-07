#pragma warning disable 0168 // variable declared but not used.
#pragma warning disable 0219 // variable assigned but not used.
#pragma warning disable 0414 // private field assigned but not used.


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UMod;
using Hyperbridge.Extension;
using Hyperbridge.Core;

public class ExtensionsView : MonoBehaviour
{
    public GameObject extensionInfoPrefab, extensionInfoOrganizer, installedExtensionsHeading, communityExtensionsHeading;
    public ExtensionModalDetailsView extensionInfoView;
    private Coroutine activeRoutine;
    private ExtensionChecker extensionChecker;

    private void Awake()
    {

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
                if (child.gameObject.GetComponent<ExtensionSummaryContainerView>() != null)
                {
                    Destroy(child.gameObject);
                }
            }
        }

        AppManager.instance.extensionManager.extensionList.communityExtensions = Database.LoadJSONFile<List<ExtensionInfo>>("/Resources/Extensions", "community-extensions");
        AppManager.instance.extensionManager.extensionList.installedExtensions = Database.LoadJSONFile<List<ExtensionInfo>>("/Resources/Extensions", "extensions");

        this.GenerateInstalledCommunityExtensionContainers();
        this.activeRoutine = null;

        yield return null;
    }


    private void GenerateExtensionContainers(List<ExtensionInfo> extensions, bool installed)
    {
        foreach (ExtensionInfo extension in extensions)
        {
            GameObject container = Instantiate(this.extensionInfoPrefab, this.extensionInfoOrganizer.transform);

            container.GetComponent<ExtensionSummaryContainerView>().extensionsView = this;
            container.GetComponent<ExtensionSummaryContainerView>().SetupExtension(extension, installed);
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

        foreach (ExtensionInfo extension in AppManager.instance.extensionManager.extensionList.installedExtensions)
        {
            GameObject container = Instantiate(this.extensionInfoPrefab, this.extensionInfoOrganizer.transform);

            container.GetComponent<ExtensionSummaryContainerView>().extensionsView = this;
            container.GetComponent<ExtensionSummaryContainerView>().SetupExtension(extension, true);
            container.SetActive(true);
        }

        communityExtensionsHeading.transform.SetSiblingIndex(transform.parent.childCount - 1);

        foreach (ExtensionInfo extension in AppManager.instance.extensionManager.extensionList.communityExtensions)
        {
            bool found = false;
            foreach (ExtensionInfo ext in AppManager.instance.extensionManager.extensionList.installedExtensions)
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

                container.GetComponent<ExtensionSummaryContainerView>().extensionsView = this;
                container.GetComponent<ExtensionSummaryContainerView>().SetupExtension(extension, false);
                container.SetActive(true);
            }
        }

    }
}
