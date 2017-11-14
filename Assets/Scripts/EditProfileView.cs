using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EditProfileView : MonoBehaviour
{

    public Button saveEditButton;
    public InputField nameInput;
    public Toggle makeDefault;

    //Again, we need a way to load images TODO TODO TODO to do TO DO

    void Start()
    {
        saveEditButton.onClick.AddListener(() =>
        {

            StartCoroutine(AppManager.instance.profileManager.EditProfileData(null, nameInput.text, makeDefault.isOn));



        });

    }


    public void StartEditingProfile(ProfileData data)
    {
        AppManager.instance.profileManager.currentlyEditingProfile = data;

        nameInput.text = data.profileName;

        makeDefault.isOn = data.defaultProfile;


    }
}
