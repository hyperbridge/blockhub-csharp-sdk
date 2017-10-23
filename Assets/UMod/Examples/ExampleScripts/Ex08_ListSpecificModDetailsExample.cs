using UnityEngine;
using System.Collections;

// Make sure we can access the uMod api
using UMod;

namespace UMod.Example
{
    /// <summary>
    /// An example script that shows how to access detailed information for a specific mod.
    /// This example assumes that the mod name is known and is installed in the 'modDirectory' path.
    /// This example makes ure of the 'ModDirectory' but there are other methods.
    /// To use this script simply attatch it to a game object.
    /// </summary>
    [ExecuteInEditMode]
    public class Ex08_ListSpecificModDetailsExample : MonoBehaviour
    {
        // The path used for the mod directory
        public string modDirectory = "";

        // The name of the mod to display details for
        public string modName = "ExampleMod";

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
            if(ModDirectory.HasMods == true)
            {
                // Attempt to find the installed mod
                IModInfo mod = ModDirectory.GetMod(modName);

                // Make sure we have a valid mod file object
                if(mod != null)
                {
                    // Build a detailed message for the mod
                    string fullMessage = string.Format("Name = {0}, Version = {1}, Core Version = {2}, Author = {3}, Description = {4}",
                        mod.ModName,
                        mod.ModVersion,
                        mod.ModCoreVersion,
                        mod.ModAuthor,
                        mod.ModDescription);

                    // Print the message to the console
                    ExampleUtil.Log(this, fullMessage);
                }
                else
                {
                    // 'modName' was not found in the mod directory so just print a message
                    ExampleUtil.LogError(this, string.Format("Failed to find an installed mod with the name '{0}'", modName));
                }
            }
            else
            {
                // There are no mods installed in 'modDirectory' so just print a message
                ExampleUtil.LogError(this, string.Format("There are no mods installed, Can't display details for '{0}'", modName));
            }
        }
    }
}
