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
    private void Start()
    {
        
    }
    void InitializeContainer()
    {
        myButton = GetComponent<Button>();
        myButton.onClick.AddListener(() =>
        {

           AppManager.instance.profileManager._manageProfilesView.ShowEditProfileView(myIndex);

        });
    }

    void Update()
    {

    }

    public void SetupProfile(Sprite image, string name, int index)
    {
        if (image != null) myImage.sprite = image;

        myText.text = name;
        myIndex = index;
        InitializeContainer();
    }

}
