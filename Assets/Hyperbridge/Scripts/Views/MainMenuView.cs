using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Hyperbridge.UI;
using Hyperbridge.Profile;
using System;

public class MainMenuView : MonoBehaviour
{
    public Text[] activeProfileNameTexts;
    void Awake()
    {
        CodeControl.Message.AddListener<MenuEvent>(this.OnMenuEvent);
        CodeControl.Message.AddListener<ProfileInitializedEvent>(OnProfileInitialized);
        CodeControl.Message.AddListener<UpdateProfilesEvent>(OnProfilesUpdated);
    }

    private void OnProfilesUpdated(UpdateProfilesEvent e)
    {
        if(e.activeProfile == null)
        {
            ChangeProfileNameTexts(null);
            return;
        }
        ChangeProfileNameTexts(e.activeProfile);

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

    void OnProfileInitialized(ProfileInitializedEvent e)
    {
        ChangeProfileNameTexts(e.activeProfile);
    }

    void ChangeProfileNameTexts(ProfileData activeProfile)
    {
        Debug.Log(activeProfile);
        foreach (Text t in activeProfileNameTexts)
        {
            if (activeProfile != null)
            {
                t.text = activeProfile.name;

            }
            else
            {
                t.text = "Create a Profile";
            }

        }
    }
}
