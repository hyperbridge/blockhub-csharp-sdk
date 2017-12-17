using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Hyperbridge.Profile;

public class NotificationsOrganizerController : MonoBehaviour
{

    public VerticalLayoutGroup[] columns;
    public GameObject notificationContainerPrefab, popupPrefab;
    public GameObject parent;

    private void Awake()
    {
        this.CleanDisplay();
    }

    public void GenerateNotification(Notification n)
    {
        GameObject newNotification = Instantiate(notificationContainerPrefab, columns[0].transform);
        newNotification.GetComponent<NotificationContainer>().SetupContainer(n.subject, n.text, n.type, n.date, n.hasPopupBeenDismissed, n.index);
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

    private void CleanDisplay()
    {
        foreach (Transform child in this.gameObject.transform)
        {
            Destroy(child.gameObject);
        }
    }
}
