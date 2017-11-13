using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EditProfileView : MonoBehaviour
{

    public int currentlyEditingProfile;
    public Button saveEditButton;
    public InputField nameInput;
    public Toggle makeDefault;

    //Again, we need a way to load images TODO TODO TODO to do TO DO

    void Start()
    {
        saveEditButton.onClick.AddListener(() =>
        {

            AppManager.instance.profileManager.EditProfileData(null, nameInput.text, makeDefault.isOn, currentlyEditingProfile);



        });

    }


    public void StartEditingProfile(int index)
    {

        currentlyEditingProfile = index;

        nameInput.text = AppManager.instance.profileManager.GetProfileDataByIndex(index).profileName;

        makeDefault.isOn = AppManager.instance.profileManager.GetProfileDataByIndex(index).defaultProfile;


    }
}
