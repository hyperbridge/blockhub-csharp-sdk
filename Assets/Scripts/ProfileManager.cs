using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProfileManager : MonoBehaviour
{

    public List<ProfileData> existingProfiles = new List<ProfileData>();
    public ManageProfilesView _manageProfilesView;
    public Text profileNameDisplay, profileNameDisplayBase;
    public ProfileData currentlyEditingProfile;

    private SaveData saver;
    private LoadData loader;

    void Awake()
    {

        saver = SaveData.SaveAtPath("Profiles");
        loader = LoadData.LoadFromPath("Profiles");

        LoadExistingProfiles();

        UpdateProfilesEvent message = new UpdateProfilesEvent();
        message.profiles = existingProfiles;
        CodeControl.Message.Send<UpdateProfilesEvent>(message);
    }

    public List<ProfileData> LoadExistingProfiles()
    {
        existingProfiles = loader.LoadAllFromFolder<ProfileData>();
        UpdateProfileNameDisplay();

        return existingProfiles;
    }

    public void SaveNewProfileData(string imageLocation, string profileName, bool makeDefault)
    {
        ProfileData newData = new ProfileData();

        newData.SetupProfileData(profileName, makeDefault, imageLocation, existingProfiles.Count.ToString());
        existingProfiles.Add(newData);
        saver.Save<ProfileData>(newData.name, newData);

        UnityEditor.AssetDatabase.Refresh();

        UpdateProfilesEvent message = new UpdateProfilesEvent();
        message.profiles = existingProfiles;
        CodeControl.Message.Send<UpdateProfilesEvent>(message);
    }

    public IEnumerator EditProfileData(string imageLocation, string profileName, bool makeDefault)
    {
        DeleteProfileData(currentlyEditingProfile);

        ProfileData editedData = new ProfileData();
        editedData.SetupProfileData(profileName, makeDefault, imageLocation, currentlyEditingProfile.uuid);
        saver.Save<ProfileData>(editedData.name, editedData);

        yield return new WaitForSeconds(0.25f);

        existingProfiles.Add(editedData);
        UpdateProfileNameDisplay(editedData);

        UpdateProfilesEvent message = new UpdateProfilesEvent();
        message.profiles = existingProfiles;
        CodeControl.Message.Send<UpdateProfilesEvent>(message);
    }

    public void DeleteProfileData(ProfileData dataToDelete)
    {
        AppManager.instance.saveDataManager.DeleteSpecificSave(dataToDelete.name, "Profiles");

        existingProfiles.Remove(dataToDelete);

        UnityEditor.AssetDatabase.Refresh();

        UpdateProfilesEvent message = new UpdateProfilesEvent();
        message.profiles = existingProfiles;
        CodeControl.Message.Send<UpdateProfilesEvent>(message);
    }

    // TODO: How does this happen? Seems like it should just use above
    public void DeleteEditingProfileData()
    {
        this.DeleteProfileData(currentlyEditingProfile);
    }

    public void UpdateProfileNameDisplay(ProfileData newDefault = null)
    {
        foreach (ProfileData data in existingProfiles) {
            if (newDefault != null && newDefault == data) {
                profileNameDisplay.text = data.name;
                profileNameDisplayBase.text = data.name;
            }
            else if (newDefault == null && data.isDefault) {
                profileNameDisplay.text = data.name;
                profileNameDisplayBase.text = data.name;
            }
            else {
                data.isDefault = false;
            }
        }
    }

    public void UpdateProfileNameDisplay()
    {
    }
}
