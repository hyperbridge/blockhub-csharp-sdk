using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Hyperbridge.Profile
{
    public class SecondaryNotificationOrganizerController : MonoBehaviour
    {
        public GameObject notificationContainerPrefab;
        public VerticalLayoutGroup showcase;

        private void Awake()
        {
            CodeControl.Message.AddListener<NotificationReceivedEvent>(OnNotificationReceived);
        }

        void OnNotificationReceived(NotificationReceivedEvent e)
        {
            StartCoroutine(GenerateSecondaryNotification(e.notification));
        }

        IEnumerator GenerateSecondaryNotification(Notification n)
        {
            GameObject newNotification = Instantiate(notificationContainerPrefab, showcase.transform);
            newNotification.GetComponent<NotificationContainer>().SetupContainer(n.text, n.type, n.date, n.hasPopupBeenDismissed, n.index);
            yield return new WaitForSeconds(2);

            Destroy(newNotification);

            yield return null;

        }
    }



}
