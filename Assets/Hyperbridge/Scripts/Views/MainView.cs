using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Hyperbridge.Profile;
namespace Hyperbridge.UI
{
    public class MainView : MonoBehaviour
    {

        public Text notificationAmountText;
        // Use this for initialization
        void Awake()
        {
            CodeControl.Message.AddListener<UpdateProfilesEvent>(OnProfileUpdated);
        }

        void OnProfileUpdated(UpdateProfilesEvent e)
        {
            if (e.activeProfile == null) return;
            if (e.activeProfile.notifications == null)
            {
                notificationAmountText.text = "0";

                return;

            }

            notificationAmountText.text = e.activeProfile.notifications.Count.ToString();
        }
    }
}

