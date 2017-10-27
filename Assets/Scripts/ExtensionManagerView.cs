using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;    
public class ExtensionManagerView : MonoBehaviour {

    List<ExtensionInfo> availableExtensions= new   List<ExtensionInfo>();
    public GameObject extensionInfoPrefab,extensionInfoOrganizer;
    LoadData loader;
    SaveData saver;
    Coroutine activeRoutine;
	void Start () {
      

        activeRoutine  = StartCoroutine(LoadExtensions());
	}
	
	public void UpdateExtensionList()
    {
        StopCoroutine(activeRoutine);
       activeRoutine =  StartCoroutine(LoadExtensions());
    }

    IEnumerator LoadExtensions()
    {/*
        string url = RoomSettings.AbsoluteFilenamePath;

        if (Application.isEditor)
        {
            url = "file:///" + url;
        }

        var www = new WWW(url);
        yield return www;
        // Do some code, when file loaded*/ //<- This is for JSON when the JSON is online
        availableExtensions = null;
        yield return new WaitForThreadedTask(() => {
            List<ExtensionInfo> json = loader.LoadThisData<List<ExtensionInfo>>("extensions");
           // availableExtensions = JsonUtility.FromJson<List<ExtensionInfo>>(json);
        });
     
        foreach(ExtensionInfo extension in availableExtensions)
        {
            GameObject container = Instantiate(extensionInfoPrefab, extensionInfoOrganizer.transform);
            container.GetComponent<ExtensionInfoContainer>().SetupExtension(extension);
        }


        Debug.Log(availableExtensions.Count );
        activeRoutine = null;   
    }
}
