using UnityEngine;
using System.Collections;

// Make sure we can access the umod api for loading
using UMod;

namespace UMod.Example
{
    /// <summary>
    /// An example script that shows how to load a prefab from a mod once successfully loaded.
    /// To use this script simply attach it to a game object and ensure that the path variable points to a valid mod, and that the asset variable is a valid prefab in the mod.
    /// It is important that the mod has been successfully loaded before attempting to load assets otherwise an exception will be thrown.
    /// </summary>
    public class Ex13_LoadModAssets : MonoBehaviour
    {
        // The host used by this class
        private ModHost host = null;

        // The path to the mod
        public string modPath = "C:/Mods/Test Mod";

        // The asset to load from the mod
        public string assetName = "Test Prefab";

        
        private void Start()
        {
            // Make sure we initialize umod before anything else
            Mod.Initialize();

            // Load the mod as in previous examples
            host = Mod.Load(new ModPath(modPath));

            // We need to make sure the mod is loaded before attempting to load assets
            if (host.IsModLoaded == true)
            {
                // We are now ready to issue a load request for an asset
                // First we will make sure that there is an asset with the specified name
                if (host.Assets.Exists(assetName) == true)
                {
                    // Now we can call the load method which will provide a Unity asset
                    // This method works in a very similar way to the 'Resources.Load' method returning a prefab object that must be instantiated into the scene
                    GameObject go = host.Assets.Load(assetName) as GameObject;

                    // Just like 'Resources' we can also use the generic method:
                    // host.Assets.load<GameObject>(assetName);

                    // Make sure nothing went wrong
                    // The only thing that can fail at this point is the cast to GameObject if the asset is of a different type
                    if (go != null)
                    {
                        // Create an instance of this prefab
                        Instantiate(go, Vector3.zero, Quaternion.identity);
                    }
                }
                else
                {
                    // There is no asset with the specified name
                    ExampleUtil.LogError(this, "An asset called '" + assetName + "' does not exist in the mod");
                }
            }
            else
            {
                // We cannot continue with asset loading
                ExampleUtil.LogError(this, "Failed to load the mod");
            }
        }
    }
}
