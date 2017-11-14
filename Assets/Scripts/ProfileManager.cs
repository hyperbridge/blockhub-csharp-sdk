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
    public ProfileData currentlyEditingProfile;
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



    public IEnumerator EditProfileData(string imageLocation, string profileName, bool makeDefault)
    {
        DeleteProfileData(currentlyEditingProfile);
        ProfileData editedData = new ProfileData();
        editedData.SetupProfileData(profileName, makeDefault, imageLocation, currentlyEditingProfile.ID);
        saver.Save<ProfileData>(editedData.profileName, editedData);
        yield return new WaitForSeconds(0.25f);
        existingProfiles.Add(editedData);
        UpdateProfileNameDisplay(editedData);

    }

    public void DeleteProfileData(ProfileData dataToDelete)
    {
        AppManager.instance.saveDataManager.DeleteSpecificSave(dataToDelete.profileName, "Profiles");


        existingProfiles.Remove(dataToDelete);
        _manageProfilesView.myLoopList.SetListItemCount(existingProfiles.Count, true);


    }
    public void DeleteEditingProfileData()
    {
        AppManager.instance.saveDataManager.DeleteSpecificSave(currentlyEditingProfile.profileName, "Profiles");


        existingProfiles.Remove(currentlyEditingProfile);
        _manageProfilesView.myLoopList.SetListItemCount(existingProfiles.Count, true);

    }
    public void UpdateProfileNameDisplay(ProfileData newDefault)
    {
        profileNameDisplay.text = newDefault.profileName;
        profileNameDisplayBase.text = newDefault.profileName;
        foreach (ProfileData data in existingProfiles)
        {
            if (data.defaultProfile && data != newDefault)
            {
                data.defaultProfile = false;
            }
        }
    }
    public void UpdateProfileNameDisplay()
    {
        foreach (ProfileData data in existingProfiles)
        {
            if (data.defaultProfile)
            {
                profileNameDisplay.text = data.profileName;
                profileNameDisplayBase.text = data.profileName;
            }
        }
    }
}
