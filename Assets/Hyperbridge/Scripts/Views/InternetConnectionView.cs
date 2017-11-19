using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InternetConnectionView : MonoBehaviour {
    public GameObject info;

    private void Awake() {
        CodeControl.Message.AddListener<InternetConnectionEvent>(this.OnInternetConnectionEvent);
    }

    public void OnInternetConnectionEvent(InternetConnectionEvent e)
    {
        if (e.connected)
        {
            this.info.SetActive(false);
        }
        else {
            this.info.SetActive(true);
        }
    }
}
