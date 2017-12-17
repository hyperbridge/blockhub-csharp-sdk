using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Hyperbridge.Profile;
using Hyperbridge.Core;

public class NotificationContainer : MonoBehaviour
{

    public Text subjectText, descriptionText, dateText;
    public Image type;
    public bool hasPopupBeenDismissed, removeNotification;
    public int index;
    public GameObject popup;

    private bool alreadyPressed;
    private Button button;

    private void Start()
    {
        this.button = GetComponent<Button>();
        this.button.onClick.AddListener(() =>
        {
            this.DispatchShowNotificationPopupRequest();
            this.RemoveNotification();
        });
    }

    //TODO: Types
    public void SetupContainer(string subjectText, string descriptionText, string type, string date, bool hasPopupBeenDismissed, int index)
    {
        this.subjectText.text = subjectText;
        this.descriptionText.text = descriptionText;
        this.dateText.text = date;
        this.hasPopupBeenDismissed = hasPopupBeenDismissed;
        this.index = index;
    }

    public void PopupDisabled()
    {
        this.hasPopupBeenDismissed = true;
        this.DispatchEditProfileEvent();
    }

    public void RemoveNotification()
    {
        if (alreadyPressed) return;

        this.removeNotification = true;

        this.DispatchEditProfileEvent();

        this.alreadyPressed = true;

        Destroy(gameObject, 0.25f);
    }

    private void DispatchEditProfileEvent()
    {
        ProfileData profile = AppManager.instance.profileManager.activeProfile;
        Notification target = new Notification();

        EditProfileEvent message = new EditProfileEvent {
            originalProfileName = profile.name,
            profileToEdit = profile,
            deleteProfile = false
        };

        foreach (Notification n in profile.notifications)
        {
            if (n.index == this.index)
            {
                target = n;
            }
        }

        if (target != null)
        {
            if (this.removeNotification) profile.notifications.Remove(target);
            target.hasPopupBeenDismissed = this.hasPopupBeenDismissed;
        }

        CodeControl.Message.Send<EditProfileEvent>(message);
    }

    private void DispatchShowNotificationPopupRequest()
    {
        this.hasPopupBeenDismissed = false;

        CodeControl.Message.Send<ShowNotificationPopupRequest>(new ShowNotificationPopupRequest { notificationContainer = this });
    }

}
