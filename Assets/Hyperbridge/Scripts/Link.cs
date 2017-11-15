using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Link : MonoBehaviour
{
    public GameObject view;
    public string path;


    private void Awake()
    {
        CodeControl.Message.AddListener<AppStateChangeEvent>(this.OnAppStateChange);

        var button = this.view.GetComponent<Button>();

        button.onClick.AddListener(this.OnButtonClick);
    }

    public void OnButtonClick() {
        var message = new NavigateEvent();
        message.path = this.path;
        CodeControl.Message.Send<NavigateEvent>(message);
    }

    public void OnAppStateChange(AppStateChangeEvent e)
    {
        var state = e.state;
        var animator = this.view.GetComponent<Animator>();
        var button = this.view.GetComponent<Button>();

        if (state.uri.StartsWith(this.path))
        {
            animator.SetTrigger(button.animationTriggers.highlightedTrigger);
        }
        else
        {
            animator.SetTrigger(button.animationTriggers.normalTrigger);
        }
    }
}
