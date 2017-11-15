using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UMod;
using System.IO;
using System.Text.RegularExpressions;


public class ModManager : MonoBehaviour
{
    ModHost latestLoadedMod;
    List<ModHost> activeMods;

    private void Awake()
    {
        Mod.Initialize();
        activeMods = new List<ModHost>();
    }

    private void Start()
    {
        CheckMods();
    }

    void CheckMods()
    {
        foreach (string mod in InstalledExtensionPaths()) {
            LoadMod(mod);
        }
    }
    /* public IEnumerator LoadMod(string modPath)
      {
          Debug.Log(modPath);

          DirectoryInfo newDirectory = new DirectoryInfo(modPath);

          ModPath loadFromPath = new ModPath(newDirectory);
          WaitForModLoad request = new WaitForModLoad(loadFromPath);

          yield return request;

          Debug.Log("Loaded " + request.WasLoadSuccessful);
      }*/
    
    public void LoadMod(string modPath)
    {
        Debug.Log(modPath);

        DirectoryInfo newDirectory = new DirectoryInfo(modPath);

        ModPath loadFromPath = new ModPath(newDirectory);

        ModHost loadInHost = Mod.LoadMod(loadFromPath);
        latestLoadedMod = loadInHost;
        activeMods.Add(loadInHost);
        loadInHost.OnModLoadComplete += OnModLoadComplete;
    }

    public void InstallMod(string modPath, string modName)
    {

        if (Directory.Exists(Application.dataPath + "/Resources/Extensions/" + modName)) {
            // If the mod is already in our files, we don't load anything
        }
        else { // Simulating a download
            FileUtil.CopyFileOrDirectory(modPath, Application.dataPath + "/Resources/Extensions/" + modName);
            LoadMod(Application.dataPath + "/Resources/Extensions/" + modName);
        }
    }

    private void OnModLoadComplete(ModLoadCompleteArgs args)
    {
        if (args.IsLoaded) {
            Debug.Log("Mod loaded");
            /* if (latestLoadedMod.Assets.Exists("UIPrefab1"))
             {
                 GameObject gO = latestLoadedMod.Assets.Load("UIPrefab1") as GameObject;

                 Instantiate(gO, FindObjectOfType<ExtensionManagerView>().transform);
             }
             */

        }
        else if (!args.IsLoaded) {
            Debug.LogError("Can't load that mod");
        }
    }

    public void UninstallMod(ModHost mod)
    {
        mod.DestroyModObjects();
        mod.UnloadMod();

        File.Delete(mod.CurrentModPath.ToString());
    }

    public List<ModHost> ModList()
    {
        return activeMods;
    }

    public string[] InstalledExtensionPaths()
    {
        string[] directories = Directory.GetDirectories(Application.dataPath + "/Resources/Extensions");
        for (int i = 0; i < directories.Length; i++) {
            directories[i] = directories[i].Replace(@"\", @"/");
        }

        return directories;
    }
}
