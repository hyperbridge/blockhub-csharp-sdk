#pragma warning disable 0168 // variable declared but not used.
#pragma warning disable 0219 // variable assigned but not used.
#pragma warning disable 0414 // private field assigned but not used.
#pragma warning disable 0649 // default value null


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class SaveDataManager : MonoBehaviour
{

    Text saveText, loadText;
    LoadData loader;
    SaveData saver;

    private void Awake()
    {

      
    }

    private void Start()
    {
      
    }

    public void CacheValues()
    {
    }

    public void SaveCurrentExtensionData()
    {
        saver = SaveData.SaveAtPath("Extensions");

    }

    public void SaveExtensionJSON(ExtensionInfo data)
    {
        saver = SaveData.SaveAtPath("Extensions");
        saver.Save<ExtensionInfo>(data.name, data);

    }

    public void DeleteSpecificSave(string saveName, string saveFolder)
    {
        Debug.Log(Application.dataPath + "/Resources/" + saveFolder + "/" + saveName + ".json");
        if(File.Exists(Application.dataPath + "/Resources/" + saveFolder + "/" + saveName + ".json"))
        {
            File.Delete(Application.dataPath + "/Resources/" + saveFolder + "/" + saveName + ".json");
            Debug.Log("wtf");
        }
        else
        {
            throw new FileNotFoundException();
        }


    }
    public void LoadSavedData()
    {


        loader = LoadData.LoadFromPath("Extensions");
      

        
    }
  
 

}
