using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace UMod.Example
{
    public class UIModElement : MonoBehaviour
    {
        // Events
        public Action<UIModElement> OnClicked;

        // Public
        public Text nameText;
        public Text versionText;
        public Text pathText;

        // Properties
        public string Name
        {
            get { return nameText.text; }
            set { nameText.text = value; }
        }

        public string Version
        {
            set { versionText.text = value; }
        }

        public string Path
        {
            set { pathText.text = value; }
        }

        // Methods
        public void OnElementClicked()
        {
            if (OnClicked != null)
                OnClicked(this);
        }
    }
}
