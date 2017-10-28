using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveDataManager : MonoBehaviour
{

    public static SaveDataManager instance;
    Text saveText, loadText;
    LoadData loader;
    SaveData saver;
    private void Awake()
    {

        if (instance == null)
        {
            instance = this;

        }
        else Debug.LogWarning("More than one object list manager");
    }

    private void Start()
    {
      
    }
    public void CacheValues()
    {
    }
    public void SaveCurrentData(int slot)
    {
        saver = SaveData.SaveAtPath("Extensions");
        StartCoroutine(SavePlacement(slot));
      //  saver.Save(slot.ToString() + "fow", FogOfWarValues());

    }

    public void SaveExtensionJSON(ExtensionInfo data)
    {
        saver = SaveData.SaveAtPath("Extensions");
        saver.Save<ExtensionInfo>(data.name, data);

    }
    IEnumerator SavePlacement(int slot)
    {


        yield return new WaitForSeconds(1);
        saveText.text = "Saved successfully in slot: " +slot.ToString();
        yield return new WaitForSeconds(2);


    }
  
    public void LoadSavedData()
    {


        loader = LoadData.LoadFromPath("Extensions");
      

        
    }
  
    IEnumerator NoSaveData(int slot)
    {
        loadText.text = "No save data in slot " + slot.ToString();
        yield return new WaitForSeconds(1);
    }
    IEnumerator LoadExtensions(int slot)
    {
        yield return new WaitForSeconds(1);
        
        loadText.text = "Loaded successfully from slot: "+slot.ToString();
        yield return new WaitForSeconds(2);
    }

   
}
