using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Hyperbridge.Core;

namespace Hyperbridge.Profile
{
    public class ProfileManager : MonoBehaviour
    {
        public List<ProfileData> profiles = new List<ProfileData>();
        public ManageProfilesView _manageProfilesView;
        public Text profileNameDisplay, profileNameDisplayBase;
        public ProfileData currentlyEditingProfile, activeProfile;


        private void Awake()
        {
          //  this.saver = SaveData.SaveAtPath("/Resources/Profiles");
           // this.loader = LoadData.LoadFromPath("/Resources/Profiles");

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
          StartCoroutine(  Database.LoadAllJSONFilesFromSubFolders<ProfileData>("/Resources/Profiles/", (profiles)=>
            {
                this.profiles = profiles;
                this.UpdateProfileNameDisplay();

                this.DispatchUpdateEvent();
            }));
            

            return this.profiles;
        }

        public void SaveNewProfileData(string imageLocation, string profileName, bool makeDefault)
        {
            var newData = new ProfileData
            {
                name = profileName,
                isDefault = makeDefault,
                imageLocation = imageLocation,
                uuid = System.Guid.NewGuid().ToString()
            };
            this.profiles.Add(newData);
            Database.SaveJSON<ProfileData>("/Resources/Profiles/" + newData.uuid,newData.name, newData);

#if UNITY_EDITOR
            UnityEditor.AssetDatabase.Refresh();
#endif

            this.DispatchUpdateEvent();
        }

        public IEnumerator EditProfileData(EditProfileEvent message)
        {
            DeleteProfileData(this.currentlyEditingProfile);

            var editedData = new ProfileData
            {
                name = message.profileName,
                isDefault = message.makeDefault,
                imageLocation = message.imageLocation,
                uuid = this.currentlyEditingProfile.uuid
            };
            Database.SaveJSON<ProfileData>("/Resources/Profiles/" + editedData.uuid,editedData.name, editedData);

            yield return new WaitForSeconds(0.25f);

            this.profiles.Add(editedData);
            this.UpdateProfileNameDisplay(editedData);

            this.DispatchUpdateEvent();
        }

        public void DeleteProfileData(ProfileData dataToDelete)
        {
            AppManager.instance.saveDataManager.DeleteSpecificSave(dataToDelete.name, "/Resources/Profiles/"+dataToDelete.uuid);

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

        public void UpdateProfileNameDisplay(ProfileData newDefault = null)
        {
            foreach (ProfileData data in this.profiles)
            {
                if (newDefault != null && newDefault == data)
                {
                    profileNameDisplay.text = data.name;
                    profileNameDisplayBase.text = data.name;
                    activeProfile = data;
                }
                else if (newDefault == null && data.isDefault)
                {
                    profileNameDisplay.text = data.name;
                    profileNameDisplayBase.text = data.name;
                    activeProfile = data;
                }
                else
                {
                    data.isDefault = false;
                }
            }
        }

      
    }
}