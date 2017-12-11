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
        ProfileData activeProfile;

        void Awake()
        {
            CodeControl.Message.AddListener<EditProfileEvent>(OnProfileEdited);
            CodeControl.Message.AddListener<ProfileInitializedEvent>(OnProfileInitialized);
        }

        void OnProfileEdited(EditProfileEvent e)
        {
            if (e == null)
            {
                throw new System.ArgumentNullException(nameof(e));
            }

            if (activeProfile == null) return;
            if (activeProfile.notifications == null)
            {
                notificationAmountText.text = "0";

                return;

            }

            UpdateNotificationsText(activeProfile.notifications.Count.ToString());
        }

        void OnProfileInitialized(ProfileInitializedEvent e)
        {

            if (e == null)
            {
                throw new System.ArgumentNullException(nameof(e));
            }

            if (e.activeProfile == null) return;
            if (e.activeProfile.notifications == null)
            {
                notificationAmountText.text = "0";

                return;

            }
            UpdateNotificationsText(e.activeProfile.notifications.Count.ToString());
            activeProfile = e.activeProfile;
        }

        void UpdateNotificationsText(string amount)
        {
            notificationAmountText.text = amount;

        }
    }
}

