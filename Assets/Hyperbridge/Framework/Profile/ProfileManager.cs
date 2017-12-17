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
        public ProfileData activeProfile;
        private string defaultProfileName;

        private void Awake()
        {
            defaultProfileName = GetGlobalDefaultProfile();
            CodeControl.Message.AddListener<AppInitializedEvent>(this.OnAppInitialized);
            CodeControl.Message.AddListener<EditProfileEvent>(this.OnProfileEdited);
        }

        public void DispatchUpdateEvent()
        {
            var message = new UpdateProfilesEvent();
            message.profiles = this.profiles;
            message.activeProfile = this.activeProfile;
            CodeControl.Message.Send<UpdateProfilesEvent>(message);
        }

        public void DispatchProfileInitializedEvent()
        {
            var message = new ProfileInitializedEvent();
            message.activeProfile = this.activeProfile;
            message.profiles = this.profiles;
            CodeControl.Message.Send<ProfileInitializedEvent>(message);
        }

        public void OnAppInitialized(AppInitializedEvent e)
        {
            this.LoadProfiles();
        }

        public void OnProfileEdited(EditProfileEvent e)
        {
            StartCoroutine(EditProfileData(e));
        }

        public List<ProfileData> LoadProfiles()
        {
            Debug.Log("Loading Profiles");
            StartCoroutine(Database.LoadAllJSONFilesFromSubFolders<ProfileData>("/Resources/Profiles/", (profiles) =>
              {
                  this.profiles = profiles;
                  this.UpdateActiveProfile();
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

            if (GetGlobalDefaultProfile() == "")
            {
                SetGlobalDefaultProfile(newData.name);
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

            if (GetGlobalDefaultProfile() == message.originalProfileName)
            {
                SetGlobalDefaultProfile(message.profileToEdit.name);
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

            this.profiles.Remove(dataToDelete);

#if UNITY_EDITOR
            UnityEditor.AssetDatabase.Refresh();
#endif

            this.DispatchUpdateEvent();
        }

        public void UpdateActiveProfile()
        {
            ProfileData updatedActiveProfile = this.FindProfileByName(defaultProfileName);

            if (this.profiles.Count == 0)
            {
                this.activeProfile = new ProfileData();
                this.activeProfile.name = "Create a Profile";

                if (this.GetGlobalDefaultProfile() != "") this.SetGlobalDefaultProfile("");
            }
            else
            {
                if (updatedActiveProfile == null)
                {
                    this.activeProfile = this.profiles[0];
                    this.SetGlobalDefaultProfile(this.profiles[0].name);
                }
                else
                {
                    this.activeProfile = this.FindProfileByName(this.GetGlobalDefaultProfile());
                }
            }

            this.profileNameDisplay.text = activeProfile.name;
            this.profileNameDisplayBase.text = activeProfile.name;

            this.DispatchProfileInitializedEvent();
        }

        string GetGlobalDefaultProfile()
        {
            if (PlayerPrefs.HasKey("DefaultProfile"))
            {
                return PlayerPrefs.GetString("DefaultProfile");
            }

            // TODO: shouldnt this be an error?
            return "";
        }

        public void SetGlobalDefaultProfile(string name)
        {
            PlayerPrefs.SetString("DefaultProfile", name);
            PlayerPrefs.Save();

            this.defaultProfileName = name;
            this.UpdateActiveProfile();
        }

        ProfileData FindProfileByName(string name)
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