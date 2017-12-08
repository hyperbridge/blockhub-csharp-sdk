using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Hyperbridge.Core;

public class NotificationManager : MonoBehaviour
{
    public GameObject notificationPopup;
    public NotificationsOrganizerController notificationsOrganizerController;

    private void Awake()
    {
        CodeControl.Message.AddListener<NotificationReceivedEvent>(OnNotificationReceived);
    }

    void OnNotificationReceived(NotificationReceivedEvent e)
    {

        AppManager.instance.profileManager.activeProfile.notifications.Add(e.notification);
        notificationsOrganizerController.GenerateNotification(e.notification);
  
    }
}
