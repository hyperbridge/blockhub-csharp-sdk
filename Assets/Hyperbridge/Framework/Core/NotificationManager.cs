using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Hyperbridge.Core;
using Hyperbridge.Profile;

public class NotificationManager : MonoBehaviour
{
    public GameObject notificationPopup, uiParent;
    public NotificationsOrganizerController notificationsOrganizerController;
    public GameObject notificationContainerPrefab;


    private void Awake()
    {
        CodeControl.Message.AddListener<ShowNotificationPopupRequest>(OnNotificationPopupShowRequested);
        CodeControl.Message.AddListener<NotificationReceivedEvent>(OnNotificationReceived);
        CodeControl.Message.AddListener<ProfileInitializedEvent>(OnProfileInitialized);
        CodeControl.Message.AddListener<ErrorEvent>(OnErrorRaised);
    }
    public void OnProfileInitialized(ProfileInitializedEvent e)
    {
        notificationsOrganizerController.ClearNotifications();
        foreach(Notification n in e.activeProfile.notifications)
        {
            notificationsOrganizerController.GenerateNotification(n);
        }
    }
    public void OnNotificationReceived(NotificationReceivedEvent e)
    {
        AddNotification(e.notification);

    }
    public void OnErrorRaised(ErrorEvent e)
    {
        GameObject popup = Instantiate(notificationPopup, uiParent.transform);
        popup.GetComponent<NotificationPopup>().SetupPopup(e.errorMessage, e.errorDate, null);

    }

    void GeneratePopup(NotificationContainer notificationContainer)
    {
        if (notificationContainer.hasPopupBeenDismissed) return;

        GameObject popup = Instantiate(notificationPopup, uiParent.transform);
        popup.GetComponent<NotificationPopup>().SetupPopup(notificationContainer.descriptionText.text, notificationContainer.dateText.text, notificationContainer);

    }


    void AddNotification(Notification notification)
    {
        notificationsOrganizerController.GenerateNotification(notification);

    }

    void OnNotificationPopupShowRequested(ShowNotificationPopupRequest request)
    {
        GeneratePopup(request.notificationContainer);
    }
}
