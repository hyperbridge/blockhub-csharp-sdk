using UnityEngine;
using System.Collections;

using UMod;

namespace UMod.Example
{
    // An example script that shows how to load a mod from file using coroutines in a similar way to WWW.
    // To use this script simply attach it to a game object and ensure that the path variable points to a valid mod.
    public class Ex11_LoadModAsync : MonoBehaviour
    {
        // The path to the mod
        public string modPath = "C:/Mods/Test Mod";

        private IEnumerator Start()
        {
            // Make sure we initialize uMod before anything else
            Mod.Initialize();

            // We need to specify the location of the mod using the 'ModPath' class.
            ModPath path = new ModPath(modPath);

            // Create a mod load request
            ModAsyncOperation<ModHost> request = Mod.LoadAsync(path);

            // Wait for the load to complete
            yield return request;

            // Check for success
            if (request.IsSuccessful == true)
            {
                // The mod is now loaded
                ExampleUtil.Log(this, "Mod Loaded!");

                // Get the mod host that loaded the mod
                ModHost loadedHost = request.Result;
            }
            else
            {
                ExampleUtil.LogError(this, "Failed to load the mod");

                // Print the error code to the console
                ExampleUtil.LogError(this, request.Status);
            }
        }
    }
}
