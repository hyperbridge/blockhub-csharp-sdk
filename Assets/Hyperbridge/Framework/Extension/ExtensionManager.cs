using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UMod;
using UMod.Scripting;
using System.IO;
using System.Text.RegularExpressions;
using System;

public class ExtensionManager : MonoBehaviour
{
    public ExtensionListManager extensionList;
    public ExtensionsView extensionsView;

    private List<ModHost> activeMods;

    private void Awake()
    {
        try {
            Mod.Initialize();
        }
        catch (ArgumentOutOfRangeException e) {
            
        }

        this.activeMods = new List<ModHost>();
        this.extensionList = new ExtensionListManager();
    }

    private void Start()
    {
        this.CheckMods();
    }

    private void CheckMods()
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


    public IEnumerator CommunicationLoop(ModHost host)
    {
        while (true)
        {
            var domain = host.ScriptDomain;

            //Debug.Log("Assembly count: " + domain.Assemblies.Length);

            foreach (var assembly in domain.Assemblies)
            {
                //Debug.Log(assembly.Name);
                ScriptType[] types = assembly.FindAllSubtypesOf<IExtensionBridge>();

                foreach(ScriptType type in types) {
                    IExtensionBridge extension = type.CreateRawInstance<IExtensionBridge>();

                    List<IExtensionCommand> outgoingCommands = extension.GetOutgoingCommands();

                    //Debug.Log(commands.Count);

                    extension.ClearOutgoingCommands();

                    var command1 = new ExtensionCommand { Name = "A", Value = "B" };
                    var command2 = new ExtensionCommand { Name = "X", Value = "Y" };

                    List<IExtensionCommand> incomingCommands = new List<IExtensionCommand> {
                        command1,
                        command2
                    };

                    extension.AddIncomingCommands(incomingCommands);
                }
            }

            //host.ScriptDomain.ExecutionContext.BroadcastMessage("OnScriptStart");

            //Debug.Log("Script count: " + host.ScriptDomain.ExecutionContext.ExecutingScripts.Length);

            //Debug.Log(host.ScriptDomain.ExecutionContext.IsExecutingScripts);


            //foreach (var script in host.ScriptDomain.ExecutionContext.ExecutingScripts)
            //{
            //    List<string> messages = (List<string>)script.SafeCall("GetMessages");

            //    //Debug.Log(messages.Count);

            //    script.SafeCall("ClearMessages");

            //    List<string> newMessages = new List<string> {
            //        "test",
            //        "abc"
            //    };

            //    script.SafeCall("AddMessages", newMessages);
            //}

            //Debug.Log(host.ScriptDomain.ExecutionContext.ExecutingScripts[0].Properties["foo"]);

            yield return new WaitForSeconds(1f);
        }
    }

    public void LoadMod(string modPath)
    {
        Debug.Log("Loading mod: " + modPath);

        DirectoryInfo newDirectory = new DirectoryInfo(modPath);

        ModPath loadFromPath = new ModPath(newDirectory);

        ModHost host = Mod.Load(loadFromPath);

        activeMods.Add(host);

        if (host.IsModLoaded == true)
        {
            StartCoroutine(CommunicationLoop(host));
        }
    }

    public IEnumerator InstallMod(ExtensionInfo extension)
    {
        // File Transfer
        /* string ID = GUID.Generate().ToString();

         Directory.CreateDirectory(Application.dataPath + "/Resources/Extensions/" + ID + "/");


         FileUtil.CopyFileOrDirectory(modPath+"/"+modName+"/", Application.dataPath + "/Resources/Extensions/" + ID + "/" + modName+"/");
         yield return new WaitForEndOfFrame();
         //We're not ready for mod loading yet   LoadMod(Application.dataPath + "/Resources/Extensions/" + modName);
         LoadData loader = LoadData.LoadFromPath("/Resources/Extensions/" + ID + "/" + modName);

         UnityEditor.AssetDatabase.Refresh();

         ExtensionInfo extensionToEdit = loader.LoadThisData<ExtensionInfo>(modName);

         extensionToEdit.enabled = true;
         extensionToEdit.path = Application.dataPath + "/Resources/Extensions/" + ID + "/";
         AppManager.instance.saveDataManager.SaveExtensionJSON(ID, extensionToEdit);*/
       /* for (int i = 0; i < extensionList.communityExtensions.Count; i++)
        {
            if (extensionList.communityExtensions[i].uuid == extension.uuid)
            {
                extensionList.communityExtensions.Remove(extensionList.communityExtensions[i]);
            }
        }*/
        extension.enabled = true;
        if (extensionList.installedExtensions.Contains(extension)) { yield break; }
        extensionList.installedExtensions.Add(extension);
        StartCoroutine(AppManager.instance.saveDataManager.SaveCurrentExtensionData());
        this.extensionsView.GenerateInstalledCommunityExtensionContainers();
        yield return new WaitForSeconds(0.5f);
    }

    public IEnumerator UninstallMod(ExtensionInfo extension)
    {
        /*  mod.DestroyModObjects();
          mod.UnloadMod();

          File.Delete(mod.CurrentModPath.ToString());*/

        for (int i = 0; i < extensionList.installedExtensions.Count; i++)
        {
            if (extensionList.installedExtensions[i].uuid == extension.uuid)
            {
                extensionList.installedExtensions.Remove(extensionList.installedExtensions[i]);
            }
        }

        extension.enabled = false;

        StartCoroutine(AppManager.instance.saveDataManager.SaveCurrentExtensionData());
        this.extensionsView.GenerateInstalledCommunityExtensionContainers();

        yield return new WaitForSeconds(0.5f);
    }

    public void DisableMod(ExtensionInfo extension)
    {
        extension.enabled = false;
        StartCoroutine(AppManager.instance.saveDataManager.SaveCurrentExtensionData());
    }

    public void EnableMod(ExtensionInfo extension)
    {
        extension.enabled = true;
        StartCoroutine(AppManager.instance.saveDataManager.SaveCurrentExtensionData());
    }

    public void DeleteMod(ExtensionInfo data)
    {
        Directory.Delete(data.path);
    }

    private void OnModLoadComplete(ModLoadCompleteArgs args)
    {
        if (args.IsLoaded)
        {
            Debug.Log("Mod loaded");
            /* if (latestLoadedMod.Assets.Exists("UIPrefab1"))
             {
                 GameObject gO = latestLoadedMod.Assets.Load("UIPrefab1") as GameObject;

                 Instantiate(gO, FindObjectOfType<ExtensionsView>().transform);
             }
             */

        }
        else if (!args.IsLoaded)
        {
            Debug.LogError("Can't load that mod");
        }
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
