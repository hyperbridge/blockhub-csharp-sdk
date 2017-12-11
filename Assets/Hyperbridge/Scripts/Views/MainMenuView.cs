using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Hyperbridge.UI;
using Hyperbridge.Profile;

public class MainMenuView : MonoBehaviour
{
    public Text[] activeProfileNameTexts;
    void Awake()
    {
        CodeControl.Message.AddListener<MenuEvent>(this.OnMenuEvent);
        CodeControl.Message.AddListener<ProfileInitializedEvent>(OnProfilesUpdated);
    }

    public void OnMenuEvent(MenuEvent e)
    {
        if (e.visible)
        {
            this.gameObject.GetComponent<Devdog.General.UI.UIWindow>().Show();
        }
        else
        {
            this.gameObject.GetComponent<Devdog.General.UI.UIWindow>().Hide();
        }
    }

    void OnProfilesUpdated(ProfileInitializedEvent e)
    {
        foreach (Text t in activeProfileNameTexts)
        {
            t.text = e.activeProfile.name;

        }
    }
}
