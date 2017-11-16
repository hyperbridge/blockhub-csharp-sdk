using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.IO;

public class LoadData
{
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
        if (File.Exists(Application.dataPath + _path + "/" + data + ".json"))
        {
            var fileToLoad = File.ReadAllText(Application.dataPath + _path + "/" + data + ".json");

            // fileToLoad = Resources.Load<TextAsset>(_path +"/"+  data +".json");

            if (fileToLoad == null)
            {
                //  Debug.Log(_path + data);
                //string message = string.Format("File '{0}' not found at path '{1}'.", data + ".json", _path);

                //throw new FileNotFoundException(message);
                return default(T);
            }
            else
            {
                Debug.Log(data + " has been loaded successfully");

                return JsonConvert.DeserializeObject<T>(fileToLoad);
            }
        }
        else { Debug.Log(Application.dataPath + _path + "/" + data + ".json"); }

        return default(T);
    }

    public List<T> LoadAllFilesFromFolder<T>()
    {
        DirectoryInfo dirInfo = new DirectoryInfo(Application.dataPath + _path);
        DirectoryInfo[] subDirectories = dirInfo.GetDirectories();
        FileInfo[] info = dirInfo.GetFiles("*.json");
        List<T> returnList = new List<T>();

        if (info.Length > 0)
        {
            foreach (FileInfo file in info)
            {
                var fileToLoad = File.ReadAllText(Application.dataPath + _path + "/" + file.Name);

                // fileToLoad = Resources.Load<TextAsset>(_path +"/"+  data +".json");

                if (fileToLoad == null)
                {
                    string message = string.Format("No files '{0}' not found at path '{1}'.", file.FullName, _path);

                    Debug.Log(message);

                    return returnList;
                    //throw new FileNotFoundException(message);
                }
                else
                {
                    returnList.Add(JsonConvert.DeserializeObject<T>(fileToLoad));
                }
            }
        }

        return returnList;
    }


    public List<T> LoadAllFilesFromSubFolder<T>()
    {
        DirectoryInfo dirInfo = new DirectoryInfo(Application.dataPath + _path);
        DirectoryInfo[] subDirectories = dirInfo.GetDirectories();
        List<T> returnList = new List<T>();

        if (subDirectories.Length == 0) return returnList;
        foreach (DirectoryInfo subDir in subDirectories)
        {
            FileInfo[] info = subDir.GetFiles("*.json");

            if (info.Length > 0)
            {
                foreach (FileInfo file in info)
                {
                    string fileToLoad = file.OpenText().ReadToEnd();

                    if (fileToLoad == null)
                    {
                        string message = string.Format("No files '{0}' not found at path '{1}'.", file.FullName, _path);

                        Debug.Log(message);

                        return returnList;
                    }
                    else
                    {
                        returnList.Add(JsonConvert.DeserializeObject<T>(fileToLoad));
                    }
                }
            }
        }




        return returnList;
    }
}
