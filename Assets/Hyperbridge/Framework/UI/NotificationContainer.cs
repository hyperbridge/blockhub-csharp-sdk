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
    public bool hasPopupBeenDismissed, removeNotification;
    public int index;
    bool alreadyPressed;
    public GameObject popup;
    Button button;
    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(() =>
        {
            DispatchShowNotificationPopupRequest();
            RemoveNotification();
        });
    }

    //TODO: Types
    public void SetupContainer(string descriptionText, string type, string date, bool hasPopupBeenDismissed, int index)
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
        removeNotification = true;
        DispatchEditProfileEvent();
        alreadyPressed = true;
        Destroy(gameObject, 0.25f);
    }

    void DispatchEditProfileEvent()
    {
        ProfileData profile = AppManager.instance.profileManager.activeProfile;
        EditProfileEvent message = new EditProfileEvent();
        message.originalProfileName = profile.name;
        message.profileToEdit = profile;

        message.deleteProfile = false;
        Notification target = new Notification();

        foreach (Notification n in profile.notifications)
        {
            if (n.index == this.index)
            {
                target = n;

            }
        }
        if (target != null)
        {
            if (removeNotification) profile.notifications.Remove(target);
            target.hasPopupBeenDismissed = this.hasPopupBeenDismissed;

        }


        CodeControl.Message.Send<EditProfileEvent>(message);


    }

    void DispatchShowNotificationPopupRequest()
    {
        hasPopupBeenDismissed = false;
        ShowNotificationPopupRequest message = new ShowNotificationPopupRequest { notificationContainer = this };

        CodeControl.Message.Send<ShowNotificationPopupRequest>(message);
    }

}
