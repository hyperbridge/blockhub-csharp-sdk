using UnityEngine;
using System.Collections;

// Make sure we can access the uMod api for loading
using UMod;

namespace UMod.Example
{
    /// <summary>
    /// An example script that shows how to load a mod from file and receive progress updates.
    /// Note that in some cased loading will complete very quickly and progress status may be missed.
    /// To use this script simply attach it to a game object and ensure that the path variable points to a valid mod.
    /// </summary>
    public class Ex02_LoadModProgressExample : MonoBehaviour
    {
        // The path to the mod
        public string modPath = "C:/Mods/Test Mod";
        

        private void Start()
        {
            // Make sure we initialize uMod before anything else
            Mod.Initialize();

            // We need to specify the location of the mod using the 'ModPath' class.
            // Note that the path specified below points to the directory for the mod and not the files inside. 
            ModPath path = new ModPath(modPath);

            // By calling load mod, we are essentially creating a dedicated ModHost component to manage the mod. We can subscribe to events after the mod host has been created since all events are guarenteed to never be called in the same frame.
            ModHost host = Mod.LoadMod(path);

            // We will need to subscribe to this progress event to receive notifications about loading progress.
            host.OnModLoadProgress += OnModLoadProgress;
        }

        private void OnModLoadProgress(ModLoadProgressArgs args)
        {
            // Check if we are downloading
            if(args.IsDownloading == true)
            {
                // We are downloading, Log a message to the console
                ExampleUtil.Log(this, "Downloading mod: " + args.ToString());
            }
            else
            {
                // We are loading the mod, Log a message to the console
                ExampleUtil.Log(this, "Loading mod: " + args.ToString());
            }
        }
    }
}