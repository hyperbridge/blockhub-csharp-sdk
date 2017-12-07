using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Hyperbridge.Core;

namespace Hyperbridge.UI
{
    public class Link : MonoBehaviour
    {
        public string path;

        private void Awake()
        {
            CodeControl.Message.AddListener<AppStateChangeEvent>(this.OnAppStateChange);

            var button = this.gameObject.GetComponent<Button>();

            button.onClick.AddListener(this.OnButtonClick);
        }

        public void OnButtonClick()
        {
            CodeControl.Message.Send<NavigateEvent>(new NavigateEvent { path = this.path });
        }

        public void OnAppStateChange(AppStateChangeEvent e)
        {
            var state = e.state;

            var animator = this.gameObject.GetComponent<Animator>();
            var button = this.gameObject.GetComponent<Button>();

            if (state.uri.StartsWith(this.path, System.StringComparison.CurrentCultureIgnoreCase))
            {
                if (!animator.GetBool(button.animationTriggers.highlightedTrigger))
                {
                    animator.ResetTrigger(button.animationTriggers.normalTrigger);
                    animator.SetTrigger(button.animationTriggers.highlightedTrigger);
                }
            }
            else
            {
                if (!animator.GetBool(button.animationTriggers.normalTrigger))
                {
                    animator.ResetTrigger(button.animationTriggers.highlightedTrigger);
                    animator.SetTrigger(button.animationTriggers.normalTrigger);
                }
            }
        }
    }
}