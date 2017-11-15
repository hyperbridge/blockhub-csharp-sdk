using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Screen : MonoBehaviour
{
    public GameObject view;
    public string path;

    private void Awake()
    {
        CodeControl.Message.AddListener<AppStateChangeEvent>(this.OnAppStateChange);
    }

    public void OnAppStateChange(AppStateChangeEvent e)
    {
        var state = e.state;
        var window = this.view.GetComponent<Devdog.General.UI.UIWindow>();

        if (state.uri.StartsWith(this.path))
        {
            window.Show();
        }
        else
        {
            window.Hide();
        }
    }
}
