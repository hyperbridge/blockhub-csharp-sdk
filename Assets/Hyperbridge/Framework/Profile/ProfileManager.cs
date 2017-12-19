using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Hyperbridge.Core;
using Hyperbridge.Wallet;


namespace Hyperbridge.Profile
{
    public class ProfileManager : MonoBehaviour
    {
        public List<ProfileData> profiles = new List<ProfileData>();
        public ManageProfilesView _manageProfilesView;
        public Text profileNameDisplay, profileNameDisplayBase;
        public ProfileData activeProfile, defaultProfile;

        private void Awake()
        {
            CodeControl.Message.AddListener<AppInitializedEvent>(this.OnAppInitialized);
            CodeControl.Message.AddListener<ApplicationSettingsUpdatedEvent>(this.OnApplicationSettingsUpdated);
            CodeControl.Message.AddListener<EditProfileEvent>(this.OnProfileEdited);
        }


        public void OnAppInitialized(AppInitializedEvent e)
        {
            this.LoadProfiles();
        }

        public void OnApplicationSettingsUpdated(ApplicationSettingsUpdatedEvent e)
        {
            if (this.LoadProfiles() != null)
            {
                foreach (ProfileData profile in this.LoadProfiles())
                {
                    if (e.applicationSettings.activeProfile != null)
                    {
                        if (profile.uuid == e.applicationSettings.activeProfile.uuid)
                        {
                            activeProfile = profile;
                        }
                    }
                    if (e.applicationSettings.defaultProfile != null)
                    {
                        if (profile.uuid == e.applicationSettings.defaultProfile.uuid)
                        {
                            defaultProfile = profile;
                        }
                    }

                }

            }

            UpdateDefaultProfile(defaultProfile);

            if (e.firstLoad || activeProfile == null)
            {
                UpdateActiveProfile(defaultProfile);
            }
            else
            {
                UpdateActiveProfile(activeProfile);

            }
        }
        public void OnProfileEdited(EditProfileEvent e)
        {
            StartCoroutine(EditProfileData(e));
        }

        public List<ProfileData> LoadProfiles()
        {
            //    Debug.Log("Loading Profiles");
            StartCoroutine(Database.LoadAllJSONFilesFromSubFolders<ProfileData>("/Resources/Profiles/", (profiles) =>
              {
                  this.profiles = profiles;
              }));

            return this.profiles;
        }

        public void SaveNewProfileData(string imageLocation, string profileName, bool makeDefault)
        {
            var newData = new ProfileData
            {
                name = profileName,
                imageLocation = imageLocation,
                uuid = System.Guid.NewGuid().ToString()
            };

            this.profiles.Add(newData);

            Database.SaveJSON<ProfileData>("/Resources/Profiles/" + newData.uuid, newData.name, newData);

            if (defaultProfile == null)
            {
                UpdateDefaultProfile(newData);
                UpdateActiveProfile(newData);
            }

#if UNITY_EDITOR
            UnityEditor.AssetDatabase.Refresh();
#endif

            this.DispatchUpdateEvent();
        }

        public void AddNotification()
        {

            EditProfileEvent message = new EditProfileEvent();
            message.profileToEdit = activeProfile;
            message.deleteProfile = false;

            Notification newNotif = new Notification { index = message.profileToEdit.notifications.Count, subject = "Sim Notification #" + message.profileToEdit.notifications.Count, text = "Dummy text", date = System.DateTime.Now.ToString(), type = "Sim", hasPopupBeenDismissed = false };
            message.profileToEdit.notifications.Add(newNotif);

            NotificationReceivedEvent notificationReceivedEvent = new NotificationReceivedEvent { notification = newNotif };

            CodeControl.Message.Send<NotificationReceivedEvent>(notificationReceivedEvent);
            CodeControl.Message.Send<EditProfileEvent>(message);
        }

        public IEnumerator EditProfileData(EditProfileEvent message)
        {
            ProfileData profileData = FindProfileByName(message.originalProfileName);

            if (message.deleteProfile)
            {
                DeleteProfileData(profileData);
                message.profileToEdit.name = message.newProfileName;
                this.profiles.Add(message.profileToEdit);
            }

            Database.SaveJSON<ProfileData>("/Resources/Profiles/" + message.profileToEdit.uuid, message.profileToEdit.name, message.profileToEdit);
            if (defaultProfile.name == message.originalProfileName)
            {
                defaultProfile = message.profileToEdit;
                UpdateActiveProfile(defaultProfile);
            }
            else
            {
                DispatchUpdateEvent();
            }

            yield return new WaitForEndOfFrame();
        }

        public void DeleteProfileData(ProfileData dataToDelete)
        {
            StartCoroutine(AppManager.instance.saveDataManager.DeleteSpecificJSON(dataToDelete.name, "/Resources/Profiles/" + dataToDelete.uuid));
            if (dataToDelete.uuid == activeProfile.uuid)
            {
                UpdateActiveProfile(null);
            }
            if (dataToDelete.uuid == defaultProfile.uuid)
            {
                defaultProfile = null;
            }
            this.profiles.Remove(dataToDelete);


#if UNITY_EDITOR
            UnityEditor.AssetDatabase.Refresh();
#endif

            this.DispatchUpdateEvent();
        }
        public void UpdateDefaultProfile(ProfileData data)
        {
            if (data == null)
            {

                return;

            }
            if (defaultProfile == null)
            {
                SetAsDefaultProfile(data);
                return;
            }
            else if (defaultProfile.uuid != data.uuid)
            {

                SetAsDefaultProfile(data);
                return;

            }

        }
        public void UpdateActiveProfile(ProfileData data)
        {
            if (defaultProfile != null && activeProfile == null)
            {
                this.activeProfile = defaultProfile;
            }
            if (data == null)
            {
                this.activeProfile = null;
                this.profileNameDisplay.text = "Create a Profile";
                this.profileNameDisplayBase.text = "Create a Profile";
            }
            else
            {
                this.activeProfile = data;
                this.profileNameDisplay.text = activeProfile.name;
                this.profileNameDisplayBase.text = activeProfile.name;

            }

            this.DispatchProfileInitializedEvent();
        }

        public void DispatchUpdateEvent()
        {
            var message = new UpdateProfilesEvent();
            message.profiles = this.LoadProfiles();
            message.activeProfile = this.activeProfile;
            CodeControl.Message.Send<UpdateProfilesEvent>(message);
        }

        public void DispatchProfileInitializedEvent()
        {
            var message = new ProfileInitializedEvent();
            message.activeProfile = this.activeProfile;
            message.profiles = this.LoadProfiles();
            CodeControl.Message.Send<ProfileInitializedEvent>(message);
        }
        public void SetAsDefaultProfile(ProfileData data)
        {
            if (data == null) return;
            ApplicationSettingsUpdatedEvent message = new ApplicationSettingsUpdatedEvent();
            message.applicationSettings.defaultProfile = data;
            message.applicationSettings.activeProfile = this.activeProfile;
            message.firstLoad = false;
            CodeControl.Message.Send<ApplicationSettingsUpdatedEvent>(message);
        }

        public ProfileData FindProfileByName(string name)
        {
            foreach (ProfileData data in this.profiles)
            {
                if (name == data.name)
                {
                    return data;
                }
            }

            return null;
        }
    }
}