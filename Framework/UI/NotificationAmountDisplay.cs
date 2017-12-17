using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Hyperbridge.Profile;

namespace Hyperbridge.UI
{
    public class NotificationAmountDisplay : MonoBehaviour
    {
        public Text notificationAmountText;
        private Image background;
        private Color baseColor;

        private void Awake()
        {
            this.background = GetComponent<Image>();
            this.baseColor = this.background.color;

            CodeControl.Message.AddListener<UpdateProfilesEvent>(this.OnProfileUpdated);
            CodeControl.Message.AddListener<ProfileInitializedEvent>(this.OnProfileInitialized);
        }

        private void OnProfileUpdated(UpdateProfilesEvent e)
        {
            UpdateNotificationsDisplay(e.activeProfile.notifications);
        }

        private void OnProfileInitialized(ProfileInitializedEvent e)
        {
            UpdateNotificationsDisplay(e.activeProfile.notifications);
        }

        private void UpdateNotificationsDisplay(List<Notification> notifications)
        {
            if (notifications == null)
            {
                throw new System.ArgumentNullException(nameof(notifications));
            }

            if (notifications == null || notifications.Count == 0)
            {
                this.notificationAmountText.text = "";
                this.background.color = new Color32(0, 0, 0, 0);
                return;
            }

            if(this.background.color != this.baseColor)
            {
                this.background.color = this.baseColor;
            }

            this.background.color = this.baseColor;
            this.notificationAmountText.text = notifications.Count.ToString();
        }
    }
}

