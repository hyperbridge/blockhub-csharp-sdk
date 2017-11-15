using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SuperScrollView;
using Devdog.General.UI;

public class ManageProfilesView : MonoBehaviour
{
    public UIWindowPage editProfileView, deleteProfileConfirmationView;
    public GameObject profileDisplayPrefab, listView;

    private ProfileManager _profileManager;

    private void Awake()
    {
        CodeControl.Message.AddListener<UpdateProfilesEvent>(UpdateProfiles);
    }

    private void Start()
    {
        _profileManager = AppManager.instance.profileManager;
        AppManager.instance.profileManager._manageProfilesView = this; // TODO: remove this
    }

    public void UpdateProfiles(UpdateProfilesEvent e) {
        var profiles = e.profiles;

        foreach (Transform child in listView.transform) {
            Destroy(child.gameObject);
        }

        foreach (ProfileData profileData in profiles) {
            Debug.Log(profileData.name);

            GameObject go = Instantiate(profileDisplayPrefab);

            ProfileContainer profileContainerController = go.GetComponent<ProfileContainer>();
            profileContainerController.SetupProfile(null, profileData.name, profileData.uuid, profileData);

            go.transform.SetParent(listView.transform);
            go.transform.localScale = new Vector3(1, 1, 1);
            go.SetActive(true);
        }
    }

    void OnEnable()
    {


    }

    public void ShowEditProfileView(ProfileData dataToEdit)
    {
        editProfileView.Show();
        editProfileView.GetComponent<EditProfileView>().StartEditingProfile(dataToEdit);
    }

    public void ShowDeleteProfileConfirmationView()
    {
        deleteProfileConfirmationView.Show();
    }
}
