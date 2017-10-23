using UnityEngine;
using System.Collections;

using UMod;

namespace UMod.Example
{
    /// <summary>
    /// An example script that shows how to load a prefab from a mod asynchronously.
    /// To use this script simply attach it to a game object and ensure that the path variable points to a valid mod, and that the asset variable is a valid prefab in the mod.
    /// It is important that the mod has been successfully loaded before attempting to load assets otherwise an exception will be thrown.
    /// </summary>
    public class Ex14_LoadModAssetsAsync : MonoBehaviour
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
                    // Now we can call the load method which will provide a Unity asset
                    // This method works in a very similar way to the 'Resources.Load' method returning a prefab object that must be instantiated into the scene
                    host.Assets.LoadAsync(assetName, OnModAssetLoadComplete);

                    // Just like 'Resources' we can also use the generic method:
                    // Note that the callback method would then need to accept a 'GameObject' argument instead of the 'Object' argument
                    // host.Assets.loadAsync<GameObject>(assetName, onModAssetLoadComplete);
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

        // This method will be called when the asset load has completed
        private void OnModAssetLoadComplete(Object obj)
        {
            // Check for load error
            if (obj != null)
            {
                // Convert the result to a Game Object which is what we are expecting
                GameObject go = obj as GameObject;

                // Check for conversion error
                if(go != null)
                {
                    // Create an instance of this prefab
                    Instantiate(go, Vector3.zero, Quaternion.identity);
                }
                else
                {
                    // At this point we know that the asset is not the type we were expecting
                    ExampleUtil.LogError(this, "Expected a 'GameObject' asset but got a '" + obj.GetType().Name + "' asset");
                }
            }
        }
    }
}
