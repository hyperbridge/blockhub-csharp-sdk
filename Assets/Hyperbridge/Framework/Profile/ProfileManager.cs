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
        public ProfileData currentlyEditingProfile, activeProfile;
        string defaultProfileName;

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

            Notification newNotif = new Notification { index = message.profileToEdit.notifications.Count, text = "Sim Notification #" + message.profileToEdit.notifications.Count, date = System.DateTime.Now.ToString(), type = "Sim", hasPopupBeenDismissed = false };
            message.profileToEdit.notifications.Add(newNotif);
            NotificationReceivedEvent notificationReceivedEvent = new NotificationReceivedEvent { notification = newNotif };
            CodeControl.Message.Send<NotificationReceivedEvent>(notificationReceivedEvent);
            CodeControl.Message.Send<EditProfileEvent>(message);
        }
        public IEnumerator EditProfileData(EditProfileEvent message)
        {
            if (message.deleteProfile)
            {
                DeleteProfileData(FindProfileByName(message.profileToEdit.name));
                yield break;
            }
            Database.SaveJSON<ProfileData>("/Resources/Profiles/" + message.profileToEdit.uuid, message.profileToEdit.name, message.profileToEdit);

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
            ProfileData updatedActiveProfile = FindProfileByName(defaultProfileName);
            if (this.profiles.Count <= 0)
            {
                activeProfile = new ProfileData();
                activeProfile.name = "Create a Profile";
                if (GetGlobalDefaultProfile() != "") SetGlobalDefaultProfile("");

            }
            else
            {
                if (updatedActiveProfile == null)
                {
                    activeProfile = this.profiles[0];
                    SetGlobalDefaultProfile(this.profiles[0].name);
                }
                else
                {
                    activeProfile = FindProfileByName(GetGlobalDefaultProfile());

                }
            }

            profileNameDisplay.text = activeProfile.name;
            profileNameDisplayBase.text = activeProfile.name;

            this.DispatchProfileInitializedEvent();


        }


        string GetGlobalDefaultProfile()
        {
            if (PlayerPrefs.HasKey("DefaultProfile"))
            {
                return PlayerPrefs.GetString("DefaultProfile");
            }
            else
            {
                return "";
            }
        }

        public void SetGlobalDefaultProfile(string name)
        {
            PlayerPrefs.SetString("DefaultProfile", name);
            PlayerPrefs.Save();
            defaultProfileName = name;
            UpdateActiveProfile();
        }

        ProfileData FindProfileByName(string name)
        {
            bool profileFound = false;
            ProfileData profileData = new ProfileData();
            foreach (ProfileData data in this.profiles)
            {
                if (name == data.name)
                {
                    profileFound = true;
                    profileData = data;
                }

            }
            if (profileFound)
            {
                return profileData;
            }
            else
            {
                return null;
            }

        }
    }
}