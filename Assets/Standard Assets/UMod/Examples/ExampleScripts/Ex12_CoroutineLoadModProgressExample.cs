using UnityEngine;
using System.Collections;

using UMod;

namespace UMod.Example
{
    public class Ex12_CoroutineLoadModProgressExample : MonoBehaviour
    {
        // The path to the mod
        public string modPath = "C:/Mods/Test Mod";

        private void Start()
        {
            // Make sure we initialize uMod before anything else
            Mod.Initialize();

            // We need to specify the location of the mod using the 'ModPath' class.
            ModPath path = new ModPath(modPath);

            // Start the couroutine as you would expect
            StartCoroutine(LoadRoutine(path));
        }

        private IEnumerator LoadRoutine(ModPath path)
        {
            // Create a mod load request
            WaitForModLoad request = new WaitForModLoad(path);

            // Wait for the load to complete by polling.
            // This allows us to access the current progress value of the load and do something with it.
            while (request.IsDone == false)
            {
                // Get the current progress of the request
                DisplayLoadingProgress(request.Progress);

                // Wait for the next frame
                yield return null;
            }

            // Check for success
            if (request.WasLoadSuccessful == true)
            {
                // The mod is now loaded
                ExampleUtil.Log(this, "Mod Loaded!");
            }
            else
            {
                ExampleUtil.LogError(this, "Failed to load the mod");

                // Print the error code to the console
                ExampleUtil.LogError(this, request.LoadStatus.ToString());
            }
        }

        private void DisplayLoadingProgress(float progress)
        {
            // Normally you whould update a UI progress bar or similar but for this example we will simply print the value to the console.
            ExampleUtil.Log(this, string.Format("Loading Progress = {0}", progress));
        }
    }
}
