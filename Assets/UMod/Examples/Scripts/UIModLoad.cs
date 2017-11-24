using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.IO;

// Access the uMod api
using UMod;

namespace UMod.Example
{
    public class UIModLoad : MonoBehaviour
    {
        // Private
        private ModHost host = null;

        // Public
        public bool persistent = true;
        public EventSystem eventSystem;
        public InputField directoryInput;
        public InputField nameInput;

        public Button load;
        public Button unload;

        // Methods
        private void Start()
        {
            // Should the UI survive scene loads
            if (persistent == true)
            {
                DontDestroyOnLoad(gameObject);
                DontDestroyOnLoad(eventSystem.gameObject);
            }

            // Check for any saved data
            if (PlayerPrefs.HasKey("umod.example.loaddirectory") == true)
            {
                // Try to load values
                directoryInput.text = PlayerPrefs.GetString("umod.example.loaddirectory");
                nameInput.text = PlayerPrefs.GetString("umod.example.loadname");
            }

            // Check for empty
            if (string.IsNullOrEmpty(directoryInput.text) == true)
                directoryInput.text = Application.persistentDataPath + "/Mods";
        }

        public void OnLoadClicked()
        {
            // Save settings
            PlayerPrefs.SetString("umod.example.loaddirectory", directoryInput.text);
            PlayerPrefs.SetString("umod.example.loadname", nameInput.text);

            // Disable button
            load.interactable = false;

            // Begin loading
            host = Mod.Load(GetModPath());

            if(host.IsModLoaded == true)
            {
                unload.interactable = true;
            }
            else
            {
                load.interactable = true;
            }
        }

        public void OnUnloadClicked()
        {
            // Unload the host
            if (host != null)
                host.UnloadMod(true);

            // Change button state
            load.interactable = true;
            unload.interactable = false;
        }

        private ModPath GetModPath()
        {
            // Check for empty strings
            if (string.IsNullOrEmpty(directoryInput.text) == true ||
                string.IsNullOrEmpty(nameInput.text) == true)
                return null;

            // Get the full path
            string path = Path.Combine(directoryInput.text, nameInput.text);

            // Create a mod path
            return new ModPath(path);
        }
    }
}