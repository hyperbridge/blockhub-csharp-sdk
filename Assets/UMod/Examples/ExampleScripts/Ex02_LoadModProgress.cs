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
    public class Ex02_LoadModProgress : MonoBehaviour
    {
        // The path to the mod
        public string modPath = "C:/Mods/Test Mod";
        

        private IEnumerator Start()
        {
            // Make sure we initialize uMod before anything else
            Mod.Initialize();

            // We need to specify the location of the mod using the 'ModPath' class.
            // Note that the path specified below points to the directory for the mod and not the files inside. 
            ModPath path = new ModPath(modPath);

            // By calling load mod, we are essentially creating a dedicated ModHost component to manage the mod. We can subscribe to events after the mod host has been created since all events are guarenteed to never be called in the same frame.
            ModAsyncOperation<ModHost> request = Mod.LoadAsync(path);

            // Wait until the request is done
            while(request.IsDone == false)
            {
                // We are loading the mod, Log a message to the console
                ExampleUtil.Log(this, "Loading mod: " + request.ProgressPercentage + "%");

                // Make sure we yield to prevent blocking the main thread
                yield return null;
            }
        }
    }
}