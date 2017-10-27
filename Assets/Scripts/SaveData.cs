using System.IO;
using Newtonsoft.Json;
using UnityEngine;

public class SaveData  {

    private string _path;
    public static SaveData SaveAtPath(string path)
    {
        return new SaveData
        {
            _path = path
        };
    }

    public void Save<T>(string saveName,T objectToSave)
    {
        if(!Directory.Exists(Application.dataPath + "/Resources/" + _path))
        {
            Directory.CreateDirectory(Application.dataPath + "/Resources/" + _path);
        }
       
        File.WriteAllText(Application.dataPath +"/Resources/"+ _path +"/"+ saveName +".json", JsonConvert.SerializeObject(objectToSave));

        Debug.Log(saveName + " Saved at: " + _path);
    }
	

}
