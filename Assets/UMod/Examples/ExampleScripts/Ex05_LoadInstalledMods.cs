using UnityEngine;
using System.Collections;

// Make sure we can access the uMod api for loading
using UMod;

namespace UMod.Example
{
    /// <summary>
    /// An example script that shows how to load all installed mods.
    /// This example makes use of the 'ModDirectory' but there are other methods.
    /// To use this script simlpe attatch it to a game object.
    /// </summary>
    [ExecuteInEditMode]
    public class Ex05_LoadInstalledMods : MonoBehaviour
    {
        // The path to the mod
        public string modDirectory = "";

        private void Start()
        {
            // Make sure we initialize uMod before anything else
            Mod.Initialize();

            // Check if we are in-editor and not in play mode - if so the component has just been added
            if (Application.isEditor == true && Application.isPlaying == false)
            {
                // Make sure we dont alter the users suggestion
                if (string.IsNullOrEmpty(modDirectory) == true)
                {
                    // Initialize to a default directory
                    modDirectory = Application.persistentDataPath + "/Mods";
                }
                return;
            }

            // Setup the mod directory before we can use it
            ModDirectory.DirectoryLocation = modDirectory;

            // Check if there are any installed mods
            if (ModDirectory.HasMods == true)
            {
                // This method will attempt to locate any mods installed in the 'modDirectory' location.
                Mod.LoadAllMods();

                // We need to know when each mod has been loaded or if the mod could not be loaded for any reason. To do this we can subscribe to a static event that is called for each mod.
                Mod.OnModLoadComplete += OnModHostLoadComplete;
            }
            else
            {
                // There are no mods installed in 'modDirectory' so just print a message.
                ExampleUtil.LogError(this, "There are no mods installed in the mod directory");
            }
        }

        private void OnModHostLoadComplete(ModHost host, ModLoadCompleteArgs args)
        {
            if (args.IsLoaded == true)
            {
                // The mod is now loaded
                ExampleUtil.Log(this, string.Format("Mod Loaded: {0}", host.CurrentModPath));
            }
            else
            {
                // The mod did not load correctly
                ExampleUtil.LogError(this, string.Format("Failed to load mod: {0}, ({1})", host.CurrentModPath, args.ErrorMessage));
            }
        }
    }
}
