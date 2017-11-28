using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Hyperbridge.UI;

public class MainHomeView : MonoBehaviour {
    public GameObject extensionLauncherAccessButton;
    public RectTransform testObjectLocation;
    void Start () {
        CodeControl.Message.AddListener<ModLoadedEvent>(OnModLoaded);
	}
	
	

    void OnModLoaded(ModLoadedEvent e)
    {
        GameObject extensionButton = Instantiate(extensionLauncherAccessButton,transform);
        extensionButton.transform.position = testObjectLocation.transform.position;
        extensionButton.GetComponent<Button>().onClick.AddListener(()=> 
        {
            NavigateEvent message = new NavigateEvent();
            message.path = "/dev/extensionTest";
            CodeControl.Message.Send<NavigateEvent>(message);


        });
    }




}
