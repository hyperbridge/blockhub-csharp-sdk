using UnityEngine;
using System.Collections;

// Make sure we can access the uMod api for loading
using UMod;

namespace UMod.Example
{
    /// <summary>
    /// An example script that shows ho uMod should be initialized before calling any other part of the API.
    /// To use this script simply attach it to a game object.
    /// </summary>
    public class Ex00_InitializeUMod : MonoBehaviour
    {
        // Should the command line be parsed
        bool parseCommandLine = true;
        

        private void Start()
        {
            // Call the mod initialize method before any other uMod method.
            // If the parse command line value is true then uMod will parse the program command line looking for mod paths.
            // The mod command line is covered in later examples.
            Mod.Initialize(parseCommandLine);
        }
    }
}