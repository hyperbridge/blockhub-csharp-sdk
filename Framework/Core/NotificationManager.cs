using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Hyperbridge.Core;
using Hyperbridge.Profile;

public class NotificationManager : MonoBehaviour
{
    public GameObject notificationPopup, uiParent;
    public NotificationsOrganizerController notificationsOrganizerController;

    private void Awake()
    {
        CodeControl.Message.AddListener<NotificationReceivedEvent>(OnNotificationReceived);
        CodeControl.Message.AddListener<ProfileInitializedEvent>(OnProfileInitialized);
    }
    public void OnProfileInitialized(ProfileInitializedEvent e)
    {
        notificationsOrganizerController.ClearNotifications();
        foreach(Notification n in e.activeProfile.notifications)
        {
            notificationsOrganizerController.GenerateNotification(n, GeneratePopup);
        }
    }
    public void OnNotificationReceived(NotificationReceivedEvent e)
    {

        AddNotification(e.notification);

    }
    void GeneratePopup(NotificationContainer notificationContainer)
    {
        if (notificationContainer.hasPopupBeenDismissed) return;

        GameObject popup = Instantiate(notificationPopup, uiParent.transform);
        popup.GetComponent<NotificationPopup>().SetupPopup(notificationContainer.descriptionText.text, notificationContainer.dateText.text, notificationContainer);

    }

    void AddNotification(Notification notification)
    {
        notificationsOrganizerController.GenerateNotification(notification, GeneratePopup);

    }
}
