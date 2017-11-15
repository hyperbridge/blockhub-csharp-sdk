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
        saveEditButton.onClick.AddListener(() => {
            StartCoroutine(AppManager.instance.profileManager.EditProfileData(null, nameInput.text, makeDefault.isOn));
        });
    }

    public void StartEditingProfile(ProfileData data)
    {
        AppManager.instance.profileManager.currentlyEditingProfile = data;

        nameInput.text = data.name;
        makeDefault.isOn = data.isDefault;
    }
}
