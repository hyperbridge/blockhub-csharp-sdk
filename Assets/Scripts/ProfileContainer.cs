using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ProfileContainer : MonoBehaviour
{

    public Image myImage;
    public Text myText;
    int myIndex;
    Button myButton;
    ProfileData myData;
    private void Start()
    {
        
    }
    void InitializeContainer()
    {
        myButton = GetComponent<Button>();
        myButton.onClick.AddListener(() =>
        {

        AppManager.instance.profileManager._manageProfilesView.ShowEditProfileView(myData);

        });
    }

    void Update()
    {

    }

    public void SetupProfile(Sprite image, string name, int index, ProfileData data)
    {
        myData = data;
        if (image != null) myImage.sprite = image;

        myText.text = name;
        myIndex = index;
        InitializeContainer();
    }

}
