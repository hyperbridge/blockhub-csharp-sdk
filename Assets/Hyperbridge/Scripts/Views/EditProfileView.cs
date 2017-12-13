using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Hyperbridge.Profile;
using Hyperbridge.Core;

public class EditProfileView : MonoBehaviour
{
    public Button saveEditButton;
    public InputField nameInput;
    ProfileData editingProfile;
    // TODO: We need a way to load images

    void Start()
    {
        this.saveEditButton.onClick.AddListener(() =>
        {
            DispatchEditedProfileEvent();
        });
    }

    public void StartEditingProfile(ProfileData data)
    {
        editingProfile = data;
        this.nameInput.text = data.name;
    }

    void DispatchEditedProfileEvent()
    {
        var message = new EditProfileEvent();
        message.originalProfileName = editingProfile.name;
        message.profileToEdit = editingProfile;
        message.newProfileName = this.nameInput.text; 
        message.deleteProfile = true;
        CodeControl.Message.Send<EditProfileEvent>(message);
    }
}
