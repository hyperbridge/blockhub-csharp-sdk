using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NotificationPopup : MonoBehaviour {

    public Text descriptionText, dateText;
    public Button[] backButtons;
    public void SetupPopup(string description, string date, NotificationContainer notificationContainer)
    {
        descriptionText.text = description;
        dateText.text = date;

        foreach(Button b in backButtons)
        {
            b.onClick.AddListener(() => 
            {
                if(notificationContainer!=null) notificationContainer.PopupDisabled();

                this.gameObject.SetActive(false);
            });
        }
        gameObject.SetActive(true);
    }
}
