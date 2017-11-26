using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Hyperbridge.Core;

public class CreateProfileView : MonoBehaviour
{
    public Button saveProfileButton;
    public InputField nameInput;
    public Toggle makeDefaultToggle;
    //We need an image selection system or image download or something similar here TODO TODO TODO

    void Start () {
        this.saveProfileButton.onClick.AddListener(() =>
        {
            AppManager.instance.profileManager.SaveNewProfileData(null, this.nameInput.text, this.makeDefaultToggle.isOn);
        });
	}
}
