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
    void OnProfileInitialized(ProfileInitializedEvent e)
    {
        foreach(Notification n in e.activeProfile.notifications)
        {
            notificationsOrganizerController.GenerateNotification(n, GeneratePopup);
        }
    }
    void OnNotificationReceived(NotificationReceivedEvent e)
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
        AppManager.instance.profileManager.activeProfile.notifications.Add(notification);
        notificationsOrganizerController.GenerateNotification(notification, GeneratePopup);

    }
}
