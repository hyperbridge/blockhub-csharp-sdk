using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UMod;

public class ExtensionOrganizerController : MonoBehaviour
{
    public GameObject extensionLauncherAccessButton;

    private GridLayoutGroup gridLayoutGroup;

    private void Awake()
    {
        this.gridLayoutGroup = GetComponent<GridLayoutGroup>();

        CodeControl.Message.AddListener<AppInitializedEvent>(this.OnAppInitialized);
    }

    private void OnAppInitialized(AppInitializedEvent e)
    {
        this.DetectInstalledExtensions();
    }

    private void DetectInstalledExtensions()
    {
        foreach (ModHost mod in AppManager.instance.modManager.ModList())
        {
            GameObject extensionButton = Instantiate(this.extensionLauncherAccessButton, transform);
            extensionButton.GetComponentInChildren<Text>().text = mod.CurrentMod.ModName;
            extensionButton.GetComponent<Button>().onClick.AddListener(() =>
            {
                this.ActivateExtension(mod);
            });
        }
    }

    public void ActivateExtension(ModHost mod)
    {
        Debug.Log(mod.name);

        mod.DestroyModObjects();

        if (mod.HasScenes)
        {
            mod.LoadDefaultScene();
        }
        else if (mod.HasAssets)
        {
            if (mod.Assets.Exists("UIPrefab1"))
            {
                GameObject gO = mod.Assets.Load("UIPrefab1") as GameObject;

                Instantiate(gO, FindObjectOfType<ExtensionsView>().transform);
            }
        }
    }
}
