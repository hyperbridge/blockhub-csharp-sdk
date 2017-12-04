using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Hyperbridge.Profile;
using Hyperbridge.Core; 

public class NotificationContainer : MonoBehaviour
{

    public Text descriptionText, dateText;
    public Image type;

    public int index;
    bool alreadyPressed;
    void Start()
    {

    }

    //TODO: Types
    public void SetupContainer(string text, string type, string date, int index)
    {
        descriptionText.text = text;
        dateText.text = date;
        this.index = index;
    }

    public void RemoveNotification()
    {
        if (alreadyPressed) return;
        DispatchEditProfileEvent();
        alreadyPressed = true;
        Destroy(gameObject, 0.25f);
    }

    void DispatchEditProfileEvent()
    {
        ProfileData profile = AppManager.instance.profileManager.activeProfile;
        EditProfileEvent message = new EditProfileEvent();
        message.name = profile.name;
        message.makeDefault = profile.isDefault;
        message.imageLocation = profile.imageLocation;

        profile.notifications.RemoveAt(this.index);
        message.notifications = profile.notifications;

        CodeControl.Message.Send<EditProfileEvent>(message);


    }

}
