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
    private Text saveText, loadText;
    private LoadData loader;
    private SaveData saver;

    public void CacheValues()
    {
    }

    public void SaveCurrentExtensionData()
    {
        this.saver = SaveData.SaveAtPath("Extensions");
    }

    public void SaveExtensionJSON(ExtensionInfo data)
    {
        this.saver = SaveData.SaveAtPath("Extensions");
        this.saver.Save<ExtensionInfo>(data.name, data);
    }

    public void DeleteSpecificSave(string saveName, string saveFolder)
    {
        Debug.Log(Application.dataPath + "/Resources/" + saveFolder + "/" + saveName + ".json");
        if (File.Exists(Application.dataPath + "/Resources/" + saveFolder + "/" + saveName + ".json")) {
            File.Delete(Application.dataPath + "/Resources/" + saveFolder + "/" + saveName + ".json");
            Debug.Log("wtf");
        }
        else {
            throw new FileNotFoundException();
        }
    }

    public void LoadSavedData()
    {
        this.loader = LoadData.LoadFromPath("Extensions");
    }
}
