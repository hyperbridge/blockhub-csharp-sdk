using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EditProfileView : MonoBehaviour
{
    public Button saveEditButton;
    public InputField nameInput;
    public Toggle makeDefault;

    // TODO: Again, we need a way to load images

    void Start()
    {
        this.saveEditButton.onClick.AddListener(() => {
            StartCoroutine(AppManager.instance.profileManager.EditProfileData(null, this.nameInput.text, this.makeDefault.isOn));
        });
    }

    public void StartEditingProfile(ProfileData data)
    {
        AppManager.instance.profileManager.currentlyEditingProfile = data;

        this.nameInput.text = data.name;
        this.makeDefault.isOn = data.isDefault;
    }
}
