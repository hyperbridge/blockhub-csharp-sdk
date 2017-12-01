using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NotificationContainer : MonoBehaviour {

    public Text descriptionText, dateText;
    public Image type;
	void Start () {
		
	}
	
	void Update () {
		
	}

    public void SetupContainer(string text, string type, string date)
    {
        descriptionText.text = text;
        dateText.text = date;
    }
}
