using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UMod;
using UMod.Scripting;
using System.IO;
using System.Text.RegularExpressions;
using System;
using Hyperbridge.ExtensionInterface;
using Hyperbridge.Core;

namespace Hyperbridge.Extension
{
    public class ExtensionManager : MonoBehaviour
    {
        public ExtensionListManager extensionList;
        public ExtensionsView extensionsView;

        private void Awake()
        {
            try
            {
                Mod.Initialize();
            }
            catch (ArgumentOutOfRangeException e)
            {

            }

            this.extensionList = new ExtensionListManager();

            CodeControl.Message.AddListener<AppInitializedEvent>(this.OnAppInitialized);
        }

        private void OnAppInitialized(AppInitializedEvent e)
        {
            foreach (string modPath in this.InstalledExtensionPaths())
            {
                this.LoadMod(modPath);
            }
        }

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

                    foreach (ScriptType type in types)
                    {
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

        public ModHost LoadMod(string modPath)
        {
            Debug.Log("Loading mod: " + modPath);

            DirectoryInfo newDirectory = new DirectoryInfo(modPath);

            ModPath loadFromPath = new ModPath(newDirectory);

            ModHost host = Mod.Load(loadFromPath);

            if (host.IsModLoaded == true)
            {
                StartCoroutine(this.CommunicationLoop(host));
            }

            return host;
        }

        public IEnumerator InstallMod(ExtensionInfo extension)
        {
            ModHost mod = this.LoadMod(Application.dataPath + "/Resources/Extensions/" + extension.uuid + "/" + extension.name);

            extension.mod = mod;
            extension.enabled = true;

            if (extensionList.installedExtensions.Contains(extension))
            {
                yield break;
            }

            extensionList.installedExtensions.Add(extension);

            StartCoroutine(AppManager.instance.saveDataManager.SaveCurrentExtensionData());

            this.extensionsView.GenerateInstalledCommunityExtensionContainers();

            yield return new WaitForSeconds(0.5f);
        }

        public IEnumerator UninstallMod(ExtensionInfo extension)
        {
            extension.mod.DestroyModObjects();
            extension.mod.UnloadMod();

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

        public List<ExtensionInfo> GetInstalledExtensions()
        {
            return this.extensionList.installedExtensions;
        }

        public void ActivateExtension(ExtensionInfo extension)
        {
            extension.mod.DestroyModObjects();

            if (extension.mod.HasScenes)
            {
                extension.mod.LoadDefaultScene();
            }
            else if (extension.mod.HasAssets)
            {
                if (extension.mod.Assets.Exists("UIPrefab1"))
                {
                    GameObject gO = extension.mod.Assets.Load("UIPrefab1") as GameObject;

                    Instantiate(gO, FindObjectOfType<ExtensionsView>().transform);
                }
            }

            CodeControl.Message.Send<ExtensionUpdateEvent>(new ExtensionUpdateEvent());
        }

        public string[] InstalledExtensionPaths()
        {
            string[] directories = new string[extensionList.installedExtensions.Count];

            for (int i = 0; i < extensionList.installedExtensions.Count; i++)
            {
                directories[i] = Application.dataPath + "/Resources/Extensions/" + extensionList.installedExtensions[i].uuid + "/" + extensionList.installedExtensions[i].name;
            }

            return directories;
        }
    }
}