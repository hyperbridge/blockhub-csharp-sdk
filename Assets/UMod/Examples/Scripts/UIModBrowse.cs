using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.IO;

using UMod;
using UnityEngine.EventSystems;


class Something {
    public void MyMethod() {
        Debug.Log("MyMethod called");
    }
}

namespace UMod.Example
{
    public class UIModBrowse : MonoBehaviour
    {
        // Private
        private IModInfo active = null;

        // Public
        public bool persistent = true;
        public EventSystem eventSystem;
        public Button loadButton;
        public GameObject list;
        public GameObject uiPrefab;
        public ModHost host;

        // Methods
        public void Start()
        {
            // Should the UI survive scene loads
            if (persistent == true)
            {
                DontDestroyOnLoad(gameObject);
                DontDestroyOnLoad(eventSystem.gameObject);
            }

            // Populate list
            SetActiveSelection(null);
            GenerateUIList();
        }

        public void OnLoadClicked()
        {
            if (active != null)
            {
                Mod.BroadcastMessage("OnScriptStart");

                // Begin loading
                host = Mod.Load(ModDirectory.GetModPath(active.NameInfo.ModName));

                if (host.IsModLoaded == true)
                {
                    StartCoroutine(MessageLoop());
                    //string res = (string)
                    //Debug.Log(res);
                }
                else
                {
                }
            }
        }

        public IEnumerator MessageLoop()
        {
            while(true) {
                var domain = host.ScriptDomain;

                Debug.Log("Assembly count: " + domain.Assemblies.Length);

                foreach (var assembly in domain.Assemblies)
                {
                    Debug.Log(assembly.Name);

                }

                //host.ScriptDomain.ExecutionContext.BroadcastMessage("OnScriptStart");

                Debug.Log("Script count: " + host.ScriptDomain.ExecutionContext.ExecutingScripts.Length);

                Debug.Log(host.ScriptDomain.ExecutionContext.IsExecutingScripts);


                foreach (var script in host.ScriptDomain.ExecutionContext.ExecutingScripts)
                {
                    List<string> messages = (List<string>)script.SafeCall("GetMessages");

                    Debug.Log(messages.Count);

                    script.SafeCall("ClearMessages");

                    List<string> newMessages = new List<string> {
                        "test",
                        "abc"
                    };

                    script.SafeCall("AddMessages", newMessages);
                }

                //Debug.Log(host.ScriptDomain.ExecutionContext.ExecutingScripts[0].Properties["foo"]);

                yield return new WaitForSeconds(1f);
            }
        }

        private void GenerateUIList()
        {
            // Destroy all cells
            foreach(Transform t in list.transform)
                Destroy(t.gameObject);

#if UNITY_EDITOR
            // Assign the mod directory
            ModDirectory.DirectoryLocation = Application.dataPath + "/UMod/Examples/ExampleMods";
#else
            DirectoryInfo dir = new DirectoryInfo(Application.dataPath);

            ModDirectory.DirectoryLocation = dir.Parent.FullName;
#endif

            // Create new cells
            foreach (IModInfo info in ModDirectory.GetMods())
            {
                CreateUICell(list, info);
            }
        }

        private void CreateUICell(GameObject owner, IModInfo mod)
        {
            GameObject go = Instantiate(uiPrefab);

            // Set as child
            go.transform.SetParent(owner.transform, false);

            // Access the script
            UIModElement element = go.GetComponent<UIModElement>();

            // Add a click listener
            element.OnClicked += OnElementClicked;

            // Fill out the fields
            element.Name = mod.NameInfo.ModName;
            element.Version = mod.NameInfo.ModVersion;

            // Use a relative path
            string relative = ModDirectory.GetModPath(mod.NameInfo.ModName).ToString();
            relative = relative.Replace(Application.dataPath + "/", "");

            element.Path = relative;
        }

        private void OnElementClicked(UIModElement element)
        {
            // Reload the mod
            IModInfo info = ModDirectory.GetMod(element.Name);

            // Set as the selection
            SetActiveSelection(info);
        }

        private void SetActiveSelection(IModInfo mod)
        {
            // Store the value
            active = mod;

            // Activate the load button
            loadButton.interactable = (active != null);

            if(active != null)
                Debug.Log(active.NameInfo.ModName);
        }
    }
}
