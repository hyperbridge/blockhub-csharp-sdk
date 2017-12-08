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
    public bool hasPopupBeenDismissed;
    public int index;
    bool alreadyPressed;
    public GameObject popup;
    void Start()
    {

    }

    //TODO: Types
    public void SetupContainer(string descriptionText, string type, string date, bool hasPopupBeenDismissed,int index)
    {
        this.descriptionText.text = descriptionText;
        dateText.text = date;
        this.hasPopupBeenDismissed = hasPopupBeenDismissed;
        this.index = index;
      
    }
   
    public void PopupDisabled()
    {
        hasPopupBeenDismissed = true;
        DispatchEditProfileEvent();
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
        message.hasPopupBeenDismissed = this.hasPopupBeenDismissed;
        message.imageLocation = profile.imageLocation;
        message.deleteProfile = false;
        profile.notifications.RemoveAt(this.index);
        message.notifications = profile.notifications;

        CodeControl.Message.Send<EditProfileEvent>(message);


    }

}
