using UnityEngine;
using System.Collections;

// Make sure we can access the umod api for loading.
using UMod;

namespace UMod.Example
{
    /// <summary>
    /// An example script that shows how to load an asset from any of the loaded mods.
    /// Since it is possible for more than one mod to be loaded at any time this allows for multi-mod support.
    /// </summary>
    public class Ex16_LoadAnyModAssets : MonoBehaviour
    {
        // The asset to instantiate from the mod
        public string assetName = "Test Prefab";

        private void Start()
        {
            // Make sure we initialize umod before anything else
            Mod.Initialize();

            // In order to keep the example short, we will assume that one or more mods have been loaded before hand.

            // Check if a mod with the specified name exists
            // This is very similar to the previous example using 'Exists' but instead, all loaded mods are checked
            if (ModAssets.HasAsset(assetName) == true)
            {
                // Now we can attempt to load an asset
                // If there is more than one mod with an asset of the specified name then the first mod loaded will take priority and its asset will be used
                // Note that the 'ModAssets' class contains all of the loading methods shown in previous examples
                GameObject go = ModAssets.Load(assetName) as GameObject;

                // Make sure nothing went wrong
                // As before, the only thing that can fail at this point is the cast to GameObject since we have already checked that the asset exists
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
    }
}
