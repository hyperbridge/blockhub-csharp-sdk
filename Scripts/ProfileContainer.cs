using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Hyperbridge.Profile;
using Hyperbridge.Core;

public class ProfileContainer : MonoBehaviour
{
    public Image image;
    public Text text;
    public Button makeDefaultButton;

    private Button button;
    private ProfileData data;
    private string uuid;

    public void SetupContainer(Sprite sprite, string name, string uuid, ProfileData data)
    {
        this.data = data;
        this.uuid = uuid;
        this.text.text = name;

        if (sprite != null)
            this.image.sprite = sprite;

        this.button = GetComponent<Button>();
        this.button.onClick.AddListener(() =>
        {
            AppManager.instance.profileManager._manageProfilesView.ShowEditProfileView(this.data); // TODO: please no
        });

        this.makeDefaultButton.onClick.AddListener(() =>
        {
            AppManager.instance.profileManager.SetGlobalDefaultProfile(name);
        });
    }
}
