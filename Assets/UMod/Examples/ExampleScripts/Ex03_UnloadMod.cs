using UnityEngine;
using System.Collections;

// Make sure we can access the uMod api for loading
using UMod;

namespace UMod.Example
{
    /// <summary>
    /// An example script that shows how to correctly unload a mod.
    /// It is important to correctly unload a mod and not just destroy the host as memory may not be released using the latter method.
    /// To use this script simply attach it to a game object and ensure that loadedHost references a valid mod host.
    /// </summary>
    public class Ex03_UnloadMod : MonoBehaviour
    {
        // The mod host that should be unloaded
        public ModHost loadedHost;


        private void Start()
        {
            // Make sure we initialize uMod before anything else
            Mod.Initialize();

            // The example assumes that the loadedHost field references a valid and loaded mod host.

            // This value will determine whether the host will be destroyed or kept alive when the mod is unloaded.
            // You should set this value to false if you want to reuse the host or destroy the host manually.
            bool destroyHost = false;

            // Call the unload method to begin shutting down the mod.
            // If the host does not have a mod loaded then this method will do nothing.
            loadedHost.UnloadMod(destroyHost);
        }
    }
}
