using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Hyperbridge.Core;

namespace Hyperbridge.Profile
{
    public class ProfileContainer : MonoBehaviour
    {
        public Image image;
        public Text text;
        public Button makeActiveProfileButton;

        private Button button;
        private ProfileData data;
        private string uuid;

        public void SetupContainer(Sprite sprite, string name, string uuid, ProfileData data)
        {
            this.data = data;
            this.uuid = uuid;
            this.text.text = name;
            if (sprite != null)
                this.image.sprite = sprite;

            this.button = GetComponent<Button>();
            this.button.onClick.AddListener(() =>
            {
                AppManager.instance.profileManager._manageProfilesView.ShowEditProfileView(this.data); // TODO: please no
            });

            this.makeActiveProfileButton.onClick.AddListener(() =>
            {
                SetAsActiveProfile(data);
            });
        }


        public void SetAsActiveProfile(ProfileData data)
        {
            ApplicationSettingsUpdatedEvent message = new ApplicationSettingsUpdatedEvent ();
            message.applicationSettings.activeProfile = data;
            message.applicationSettings.defaultProfile = null;
            message.firstLoad = false;
            CodeControl.Message.Send<ApplicationSettingsUpdatedEvent>(message);


        }
    }
}

