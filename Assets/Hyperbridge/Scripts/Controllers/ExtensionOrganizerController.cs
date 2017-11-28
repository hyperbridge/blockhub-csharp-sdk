using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UMod;
using Hyperbridge.Core;
using Hyperbridge.Extension;

public class ExtensionOrganizerController : MonoBehaviour
{
    public GameObject extensionLauncherAccessButton;

    private GridLayoutGroup gridLayoutGroup;

    private void Awake()
    {
        this.gridLayoutGroup = GetComponent<GridLayoutGroup>();

        CodeControl.Message.AddListener<ExtensionUpdateEvent>(this.OnExtensionUpdate);
    }

    private void OnExtensionUpdate(ExtensionUpdateEvent e)
    {
        this.DetectInstalledExtensions();
    }

    private void DetectInstalledExtensions()
    {
        Debug.Log("Loading extension into group");

        foreach (ExtensionInfo extension in AppManager.instance.extensionManager.GetInstalledExtensions())
        {
            GameObject extensionButton = Instantiate(this.extensionLauncherAccessButton, transform);
            extensionButton.GetComponentInChildren<Text>().text = extension.name;
            extensionButton.GetComponent<Button>().onClick.AddListener(() =>
            {
                AppManager.instance.extensionManager.ActivateExtension(extension);
            });
        }
    }
}
