﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SuperScrollView;
using Devdog.General.UI;

public class ManageProfilesView : MonoBehaviour
{
    public UIWindowPage editView, deleteConfirmationView;
    public GameObject displayPrefab, listView;

    private ProfileManager profileManager;

    private void Awake()
    {
        CodeControl.Message.AddListener<UpdateProfilesEvent>(this.OnUpdateProfiles);
    }

    private void Start()
    {
        this.profileManager = AppManager.instance.profileManager;
        AppManager.instance.profileManager._manageProfilesView = this; // TODO: remove this
    }

    public void OnUpdateProfiles(UpdateProfilesEvent e)
    {
        var profiles = e.profiles;

        foreach (Transform child in this.listView.transform) {
            GameObject.Destroy(child.gameObject);
        }

        foreach (ProfileData info in profiles) {
            Debug.Log(info.name);

            GameObject go = GameObject.Instantiate(displayPrefab);

            var container = go.GetComponent<ProfileContainer>();
            container.SetupContainer(null, info.name, info.uuid, info);

            go.transform.SetParent(this.listView.transform);
            go.transform.localScale = new Vector3(1, 1, 1);
            go.SetActive(true);
        }
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
}