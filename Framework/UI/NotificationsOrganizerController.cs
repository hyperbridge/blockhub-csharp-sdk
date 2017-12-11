using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Hyperbridge.Profile;
public class NotificationsOrganizerController : MonoBehaviour
{

    public VerticalLayoutGroup[] columns;
    public GridLayoutGroup grid;
    public GameObject notificationContainerPrefab, popupPrefab;
    public GameObject parent;

    public void GenerateNotification(Notification n, System.Action<NotificationContainer> callback)
    {
        GameObject newNotification = Instantiate(notificationContainerPrefab, columns[0].transform);
        newNotification.GetComponent<NotificationContainer>().SetupContainer(n.text, n.type, n.date, n.hasPopupBeenDismissed, n.index);

        callback(newNotification.GetComponent<NotificationContainer>());
    }

    public void ClearNotifications()
    {
        foreach (VerticalLayoutGroup vlg in columns)
        {
            foreach (Transform child in vlg.transform)
            {
                Destroy(child.gameObject);
            }
        }

    }
}
