using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SuperScrollView;
using Devdog.General.UI;
using Hyperbridge.Profile;
using Hyperbridge.Core;

public class ManageProfilesView : MonoBehaviour
{
    public UIWindowPage editView, deleteConfirmationView;
    public GameObject displayPrefab, listView;

    private ProfileManager profileManager;

    private void Awake()
    {
        foreach (Transform child in this.listView.transform) {
            GameObject.Destroy(child.gameObject);
        }

        CodeControl.Message.AddListener<UpdateProfilesEvent>(this.OnUpdateProfiles);
        CodeControl.Message.AddListener<ProfileInitializedEvent>(this.OnProfileInitialized);
    }

    private void Start()
    {
        this.profileManager = AppManager.instance.profileManager;
        AppManager.instance.profileManager._manageProfilesView = this; // TODO: remove this
    }

    public void OnUpdateProfiles(UpdateProfilesEvent e)
    {
        RefreshProfilesListDisplay(e.profiles);
    }
    public void OnProfileInitialized(ProfileInitializedEvent e)
    {
        RefreshProfilesListDisplay(e.profiles);
    }

    public void ShowEditProfileView(ProfileData dataToEdit)
    {
        this.editView.Show();
        this.editView.GetComponent<EditProfileView>().StartEditingProfile(dataToEdit);
    }

    public void ShowDeleteProfileConfirmationView()
    {
        this.deleteConfirmationView.Show();
    }
    void RefreshProfilesListDisplay(List<ProfileData> profiles)
    {
        Debug.Log(profiles.Count);

        foreach (Transform child in this.listView.transform)
        {
            GameObject.Destroy(child.gameObject);
        }

        foreach (ProfileData info in profiles)
        {
            GameObject go = GameObject.Instantiate(displayPrefab);

            var container = go.GetComponent<ProfileContainer>();
            container.SetupContainer(null, info.name, info.uuid, info);

            go.transform.SetParent(this.listView.transform);
            go.transform.localScale = new Vector3(1, 1, 1);
            go.SetActive(true);
        }
    }
}
