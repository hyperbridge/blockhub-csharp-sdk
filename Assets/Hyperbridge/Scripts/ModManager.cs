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
        //TODO: This is deactivated because Umod isn't real (we're not using it until they update)
        /*foreach (string mod in InstalledExtensionPaths())
        {
            LoadMod(mod);
        }*/
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
        // File Transfer
        string ID = GUID.Generate().ToString();
        // Directory.CreateDirectory(Application.dataPath + "/Resources/Extensions/" + ID + "/");
        FileUtil.CopyFileOrDirectory(modPath, Application.dataPath + "/Resources/Extensions/" + ID + "/" + modName);
        //We're not ready for mod loading yet   LoadMod(Application.dataPath + "/Resources/Extensions/" + modName);
        LoadData loader = LoadData.LoadFromPath("/Resources/Extensions/" + ID + "/" + modName);
        UnityEditor.AssetDatabase.Refresh();

        ExtensionInfo extensionToEdit = loader.LoadThisData<ExtensionInfo>(modName);
        extensionToEdit.installed = true;
        extensionToEdit.active = true;
        AppManager.instance.saveDataManager.SaveExtensionJSON(ID, extensionToEdit);



    }

    private void OnModLoadComplete(ModLoadCompleteArgs args)
    {
        if (args.IsLoaded)
        {
            Debug.Log("Mod loaded");
            /* if (latestLoadedMod.Assets.Exists("UIPrefab1"))
             {
                 GameObject gO = latestLoadedMod.Assets.Load("UIPrefab1") as GameObject;

                 Instantiate(gO, FindObjectOfType<ExtensionManagerView>().transform);
             }
             */

        }
        else if (!args.IsLoaded)
        {
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
        for (int i = 0; i < directories.Length; i++)
        {
            directories[i] = directories[i].Replace(@"\", @"/");
        }

        return directories;
    }
}
