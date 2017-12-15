using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Hyperbridge.Profile;
namespace Hyperbridge.UI {
    public class NotificationAmountDisplay : MonoBehaviour
    {


        public Text notificationAmountText;
        Image background;
        Color baseColor;

        void Awake()
        {
            background = GetComponent<Image>();
            baseColor = background.color;
            CodeControl.Message.AddListener<UpdateProfilesEvent>(OnProfileUpdated);
            CodeControl.Message.AddListener<ProfileInitializedEvent>(OnProfileInitialized);
        }

        void OnProfileUpdated(UpdateProfilesEvent e)
        {
           

            UpdateNotificationsDisplay(e.activeProfile.notifications);
        }

        void OnProfileInitialized(ProfileInitializedEvent e)
        {
            UpdateNotificationsDisplay(e.activeProfile.notifications);
        }

        void UpdateNotificationsDisplay(List<Notification> notifications)
        {
            if (notifications == null)
            {
                throw new System.ArgumentNullException(nameof(notifications));
            }

            if (notifications == null || notifications.Count == 0)
            {
                notificationAmountText.text = "";
                background.color = new Color32(0, 0, 0, 0);
                return;

            }
            if(background.color != baseColor)
            {
                background.color = baseColor;
            }
            background.color = baseColor;
            notificationAmountText.text = notifications.Count.ToString();

        }
    }
}

