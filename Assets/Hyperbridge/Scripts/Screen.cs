using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Hyperbridge {
    public class Screen : MonoBehaviour
    {
        public string path;

        private void Awake()
        {
            CodeControl.Message.AddListener<AppStateChangeEvent>(this.OnAppStateChange);
        }

        public void OnAppStateChange(AppStateChangeEvent e)
        {
            var state = e.state;
            var window = this.gameObject.GetComponent<Devdog.General.UI.UIWindow>();

            if (state.uri.StartsWith(this.path, System.StringComparison.CurrentCultureIgnoreCase))
            {
                window.Show();
            }
            else
            {
                window.Hide();
            }
        }
    }
}
