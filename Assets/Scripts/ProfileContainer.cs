using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ProfileContainer : MonoBehaviour
{
    public Image image;
    public Text text;

    private Button button;
    private ProfileData data;
    private string uuid;

    private void Start()
    {
        
    }

    private void Update()
    {

    }

    private void InitializeContainer()
    {
        this.button = GetComponent<Button>();
        this.button.onClick.AddListener(() => {
            AppManager.instance.profileManager._manageProfilesView.ShowEditProfileView(this.data); // TODO: please no
        });
    }

    public void SetupProfile(Sprite sprite, string name, string uuid, ProfileData data)
    {
        this.data = data;
        this.uuid = uuid;
        this.text.text = name;

        if (sprite != null)
            this.image.sprite = sprite;
        
        InitializeContainer();
    }
}
