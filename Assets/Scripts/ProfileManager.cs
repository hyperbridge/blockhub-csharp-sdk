using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProfileManager : MonoBehaviour
{

    public List<ProfileData> existingProfiles = new List<ProfileData>();
    SaveData saver;
    LoadData loader;
    public ManageProfilesView _manageProfilesView;
    public Text profileNameDisplay, profileNameDisplayBase;

    void Awake()
    {

        saver = SaveData.SaveAtPath("Profiles");
        loader = LoadData.LoadFromPath("Profiles");

        LoadExistingProfiles();
    }

    public List<ProfileData> LoadExistingProfiles()
    {
        existingProfiles = loader.LoadAllFromFolder<ProfileData>();
        UpdateProfileNameDisplay();
        return existingProfiles;
    }
    public ProfileData GetProfileDataByIndex(int index)
    {
        UpdateProfileNameDisplay();

        return existingProfiles[index];
      
        /*foreach (ProfileData data in existingProfiles)
        {
            if (data.ID == index)
            {
                return data;

            }
        }*/
    }
    /// <summary>
    /// Creates a new profile data
    /// </summary>
    /// <param name="imageLocation"></param>
    /// <param name="profileName"></param>
    /// <param name="makeDefault"></param>
    public void SaveNewProfileData(string imageLocation, string profileName, bool makeDefault)
    {
        ProfileData newData = new ProfileData();

        newData.SetupProfileData(profileName, makeDefault, imageLocation, existingProfiles.Count);
        existingProfiles.Add(newData);
        saver.Save<ProfileData>(newData.profileName, newData);
    }



    public void EditProfileData(string imageLocation, string profileName, bool makeDefault, int ID)
    {
        DeleteProfileData(ID);
        ProfileData editedData = new ProfileData();
        editedData.SetupProfileData(profileName, makeDefault, imageLocation, ID);
        saver.Save<ProfileData>(editedData.profileName, editedData);
        UpdateProfileNameDisplay();

    }

    public void DeleteProfileData(int ID)
    {
        foreach (ProfileData data in existingProfiles)
        {
            if (data.ID == ID)
            {

                existingProfiles.Remove(data);
               _manageProfilesView.myLoopList.SetListItemCount(existingProfiles.Count, true);

                AppManager.instance.saveDataManager.DeleteSpecificSave(data.profileName, "Profiles");

            }
        }



    }

    public void UpdateProfileNameDisplay()
    {
        foreach(ProfileData data in existingProfiles)
        {
            if (data.defaultProfile)
            {
                profileNameDisplay.text = data.profileName;
                profileNameDisplayBase.text = data.profileName;
            }
        }
    }
}
