using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuView : MonoBehaviour
{
    void Awake() {
        CodeControl.Message.AddListener<MenuEvent>(this.OnMenuEvent);
	}

    public void OnMenuEvent(MenuEvent e) {
        if (e.visible) {
            this.gameObject.GetComponent<Devdog.General.UI.UIWindow>().Show();
        }
        else {
            this.gameObject.GetComponent<Devdog.General.UI.UIWindow>().Hide();
        }
    }
}
