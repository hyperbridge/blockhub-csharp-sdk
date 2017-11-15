using UnityEngine;
using System.Collections;

// Make sure we can access the uMod api
using UMod;

namespace UMod.Example
{
    /// <summary>
    /// An example script that shows how to list all mods currently installed.
    /// This example makes use of the 'ModDirectory' but there are other methods.
    /// To use this script simply attach it to a game object.
    /// </summary>
    [ExecuteInEditMode]
    public class Ex06_ListModsExample : MonoBehaviour
    {
        // The path used for the mod directory
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

            // Check if there are any installed mods.
            if (ModDirectory.HasMods == true)
            {
                // We can now use the mod directory to list all mods installed.
                // Note that this method will list all valid mods located in the 'modDirectory' path.
                foreach (string modName in ModDirectory.GetModNames())
                {
                    // Print the mod name to the console.
                    ExampleUtil.Log(this, modName);
                }
            }
            else
            {
                // There are no mods installed in 'modDirectory' so just print a message.
                ExampleUtil.LogError(this, "There are no mods installed in the mod directory");
            }
        }
    }
}
