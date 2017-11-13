using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreateProfileView : MonoBehaviour {

    public Button saveProfileButton;
    public InputField nameInput;
    public Toggle makeDefaultToggle;
    //We need an image selection system or image download or something similar here TODO TODO TODO

    void Start () {
        saveProfileButton.onClick.AddListener(() =>
        {
            AppManager.instance.profileManager.SaveNewProfileData(null, nameInput.text, makeDefaultToggle.isOn);
        });
	}


    void Update () {
		
	}
}
