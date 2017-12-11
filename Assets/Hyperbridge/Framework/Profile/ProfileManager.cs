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
        bool firstLoad;

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
            firstLoad = true;
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
                  Debug.Log(profiles.Count);
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
            message.notifications = new List<Notification>();
            message.deleteProfile = false;
            message.name = activeProfile.name;

            if (activeProfile.notifications != null)
            {
                foreach (Notification n in activeProfile.notifications)
                {
                    message.notifications.Add(n);
                }
            }
            Notification newNotif = new Notification { index = message.notifications.Count, text = "Sim Notification #" + message.notifications.Count, date = System.DateTime.Now.ToString(), type = "Sim", hasPopupBeenDismissed = false };
            message.notifications.Add(newNotif);
            NotificationReceivedEvent notificationReceivedEvent = new NotificationReceivedEvent { notification = newNotif };
            CodeControl.Message.Send<NotificationReceivedEvent>(notificationReceivedEvent);
            CodeControl.Message.Send<EditProfileEvent>(message);
        }
        public IEnumerator EditProfileData(EditProfileEvent message)
        {
            if (currentlyEditingProfile == null) currentlyEditingProfile = activeProfile;
            var editedData = new ProfileData
            {
                name = message.name,
                imageLocation = message.imageLocation,
                uuid = this.currentlyEditingProfile.uuid,
                notifications = message.notifications
            };
            yield return new WaitForSeconds(1);

            Database.SaveJSON<ProfileData>("/Resources/Profiles/" + editedData.uuid, editedData.name, editedData);

            yield return new WaitForEndOfFrame();


            this.profiles.Add(editedData);

            if (currentlyEditingProfile == activeProfile)
            {
                SetGlobalDefaultProfile(editedData.name);
            }
            if (message.deleteProfile) DeleteProfileData(currentlyEditingProfile);

            this.UpdateActiveProfile();

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

        // TODO: How does this happen? Seems like it should just use above <- This is the only way to use a button, buttons can't pass abstract info

        public void DeleteEditingProfileData()
        {
            this.DeleteProfileData(currentlyEditingProfile);
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
            if (firstLoad)
            {
                this.DispatchProfileInitializedEvent();
                firstLoad = false;
            }
            else
            {
                this.DispatchUpdateEvent();
            }

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
            ProfileData requiredData = new ProfileData();
            foreach (ProfileData data in this.profiles)
            {
                if (name == data.name)
                {
                    profileFound = true;
                    requiredData = data;
                }

            }
            if (profileFound)
            {
                return requiredData;
            }
            else
            {
                return null;
            }

        }
    }
}