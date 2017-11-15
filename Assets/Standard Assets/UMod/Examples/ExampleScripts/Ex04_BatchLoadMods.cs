using UnityEngine;
using System.Collections;

// Make sure we can access the uMod api for loading
using UMod;

namespace UMod.Example
{
    /// <summary>
    /// An example script that shows how to load a number of mods simultaneously using the uMod API.
    /// To use this script simlpe attatch it to a game object and ensure that the path varible points to atleat 1 valid mod.
    /// </summary>
    public class Ex04_BatchLoadMods : MonoBehaviour
    {
        // The path to the mod
        public string[] modPaths = new string[] { "C:/Mods/Test Mod" };

        private void Start()
        {
            // Make sure we initialize uMod before anything else
            Mod.Initialize();

            ModPath[] paths = new ModPath[modPaths.Length];

            // Create the paths
            for (int i = 0; i < modPaths.Length; i++)
                paths[i] = new ModPath(modPaths[i]);

            // Load all mods at the same time. This method will create and return a collection of Mod Hosts (1 per mod) which is responsible for managing the mod.
            Mod.BatchLoadMods(paths);

            // We need to know when each mod has been loaded or if the mod could not be loaded for any reason. To do this we can subscribe to a static event that is called for each mod.
            Mod.OnModLoadComplete += OnModHostLoadComplete;
        }

        private void OnModHostLoadComplete(ModHost host, ModLoadCompleteArgs args)
        {
            if(args.IsLoaded == true)
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
