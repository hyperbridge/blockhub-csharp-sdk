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
    public Toggle makeDefault;

    // TODO: Again, we need a way to load images

    void Start()
    {
        this.saveEditButton.onClick.AddListener(() =>
        {
            DispatchUpdatedProfileEvent();
        });
    }

    public void StartEditingProfile(ProfileData data)
    {
        AppManager.instance.profileManager.currentlyEditingProfile = data;

        this.nameInput.text = data.name;
        this.makeDefault.isOn = data.isDefault;
    }

    void DispatchUpdatedProfileEvent()
    {
        var message = new EditProfileEvent();
        message.imageLocation = null;
        message.name = this.nameInput.text;
        message.makeDefault = this.makeDefault.isOn;
        CodeControl.Message.Send<EditProfileEvent>(message);
    }
}
