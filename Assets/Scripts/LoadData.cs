using UnityEngine;
using System.Collections;
using Newtonsoft.Json;
using System.IO;

public class LoadData  {

    private string _path;
	public static LoadData LoadFromPath(string path)
    {
        return new LoadData
        {
            _path = path
        };
    }
	
	public T LoadThisData<T>(string data) 
    {
        if(File.Exists(Application.dataPath + "/Resources/" + _path + "/" + data + ".json") )
        {
            var fileToLoad = File.ReadAllText(Application.dataPath + "/Resources/" + _path + "/" + data + ".json");

            // fileToLoad = Resources.Load<TextAsset>(_path +"/"+  data +".json");


            if (fileToLoad == null)
            {
                Debug.Log(_path + data);
                //string message = string.Format("File '{0}' not found at path '{1}'.", data + ".json", _path);

                return default(T);
                //throw new FileNotFoundException(message);
            }
            else
            {
                Debug.Log(data + " has been loaded successfully");
                return JsonConvert.DeserializeObject<T>(fileToLoad);

            }
        }
        else
        {
            return default(T);

        }


    }





}
