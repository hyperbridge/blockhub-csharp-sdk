using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UMod;
using System.IO;


public class ModManager : MonoBehaviour {

    ModHost latestLoadedMod;
    List<ModHost> activeMods;

    private void Awake()
    {
        Mod.Initialize();
        activeMods = new List<ModHost>();
        
    }
  
    public void LoadMod(string modPath)
    {
        DirectoryInfo newDirectory = new DirectoryInfo(modPath);
        ModPath loadFromPath = new ModPath(newDirectory);
        Debug.Log(loadFromPath.ModName);
        ModHost loadInHost = Mod.LoadMod(loadFromPath);

      //  loadInHost.LoadMod(loadFromPath);
        latestLoadedMod = loadInHost;
        activeMods.Add(loadInHost);
        loadInHost.OnModLoadComplete += OnModLoadComplete;
    }

    private void OnModLoadComplete(ModLoadCompleteArgs args)
    {
        if (args.IsLoaded)
        {
            Debug.Log("Mod loaded");
            if (latestLoadedMod.Assets.Exists("UIPrefab1"))
            {
                GameObject gO = latestLoadedMod.Assets.Load("UIPrefab1") as GameObject;

                Instantiate(gO,FindObjectOfType<ExtensionManagerView>().transform);
            }


        } else if (!args.IsLoaded)
        {
            Debug.LogError("Can't load that mod");
        }
    }

    public List<ModHost> modList()
    {
        return activeMods;
    }

  
}
