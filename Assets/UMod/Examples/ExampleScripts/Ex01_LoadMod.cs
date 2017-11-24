using UnityEngine;
using System;
using System.Collections;

// Make sure we can access the uMod api for loading
using UMod;

namespace UMod.Example
{
    /// <summary>
    /// An example script that shows how to load a mod from file using the uMod API.
    /// To use this script simply attach it to a game object and ensure that the path variable points to a valid mod.
    /// </summary>
    public class Ex01_LoadMod : MonoBehaviour
    {
        // The path to the mod
        public string modPath = "C:/Mods/Test Mod";


        private void Start()
        {
            // Make sure we initialize uMod before anything else
            Mod.Initialize();

            // We need to specify the location of the mod using the 'ModPath' class.
            // Note that the path specified below points to the directory for the mod and not the files inside. 
            ModPath path = new ModPath(modPath);

            // By calling load mod, we are essentially creating a dedicated ModHost component to manage the mod.
            ModHost host = Mod.Load(path);

            if(host.IsModLoaded == true)
            {
                // The mod is now loaded
                ExampleUtil.Log(this, "Mod Loaded!");
            }
            else
            {
                ExampleUtil.LogError(this, "Failed to load the mod");

                // Print the error code, error message and exception (if any) to the console to find out what went wrong
                ExampleUtil.LogError(this, "Error message: " + host.LoadResult.Message);

                switch (host.LoadResult.Error)
                {
                    case ModLoadError.NoError: break; // Indicates that the load request completed without error 
                    case ModLoadError.UnknownError: break; // An error occurred that could not be recognised (E.g sharing IO exception, Security exception)
                    case ModLoadError.RemoteDownloadFailed: break; // Failed to doawnload the required files from the remote host
                    case ModLoadError.LocalDownloadFailed: break; // Failed to download the required files from the local file system
                    case ModLoadError.InvalidMod: break; // The mod is not compatible with this version of uMod or is corrupted or has not been built with the uMod Exporter
                    case ModLoadError.InvalidPath: break; // The mod path supplied does not point to a lodable mod
                    case ModLoadError.ModNotFound: break; // The mod path is valid but the required mod files do not exist in the mod directory
                    case ModLoadError.MissingResources: break; // The mod does not contain the required resources file
                    case ModLoadError.MissingReferences: break; // One or more of the mods dependencies cannot be loaded
                    case ModLoadError.UModVersionIncompatibility: break; // The mod was created by a newer version of uMod Exporter and cannot be loaded
                    case ModLoadError.UnityVersionIncompatibility: break; // The mod was created in a diferent version of Unity
                    case ModLoadError.SecurityError: break; // The mod breaches the security restrictions imposed by the host and as a result was not loaded
                }
            }
        }
    }
}
