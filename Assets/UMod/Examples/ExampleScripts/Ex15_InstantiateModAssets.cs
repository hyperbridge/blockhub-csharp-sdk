using UnityEngine;
using System.Collections;

// Make sure we can access the umod api for loading
using UMod;

namespace UMod.Example
{
    /// <summary>
    /// An example script that shows how to instantiate mod assets.
    /// </summary>
    public class Ex15_InstantiateModAssets : MonoBehaviour
    {
        // The host used by this class
        private ModHost host = null;

        // The path to the mod
        public string modPath = "C:/Mods/Test Mod";

        // The asset to instantiate from the mod
        public string assetName = "Test Prefab";

        private void Start()
        {
            // Make sure we initialize umod before anything else
            Mod.Initialize();

            // Load the mod as in previous examples
            host = Mod.LoadMod(new ModPath(modPath));

            // Add a listener for the load event
            host.OnModLoadComplete += OnModLoadComplete;
        }

        private void OnModLoadComplete(ModLoadCompleteArgs args)
        {
            // We need to make sure the mod is loaded before attempting to load assets
            if (args.IsLoaded == true)
            {
                // We are now ready to issue a load request for an asset
                // First we will make sure that there is an asset with the specified name
                if (host.Assets.Exists(assetName) == true)
                {
                    // Now we can call the instantiate method which will create an instance of the asset with the specified name
                    // This method works in a very similar way to the 'Resources.Load' method returning a prefab object that must be instantiated into the scene
                    GameObject go = host.Assets.Instantiate(assetName) as GameObject;

                    // go is now an active game object in the scene
                    go.transform.position = Vector3.zero;
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
