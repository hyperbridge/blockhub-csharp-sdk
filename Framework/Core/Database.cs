using System.IO;
using Newtonsoft.Json;
using UnityEngine;
using System.Collections.Generic;
namespace Hyperbridge.Core
{
    public static class Database
    {
        public static void SaveJSONToExternal<T>(string externalPath, string saveName, T objectToSave)
        {
            if (!Directory.Exists(externalPath))
            {
                Directory.CreateDirectory(externalPath);
            }
            File.WriteAllText(externalPath + "/" + saveName + ".json", JsonConvert.SerializeObject(objectToSave));
            
            Debug.Log(saveName + " Saved at: " + externalPath);
        }

        public static void SaveJSON<T>(string internalPath, string saveName, T objectToSave)
        {
            if (!Directory.Exists(Application.dataPath + internalPath))
            {
                Directory.CreateDirectory(Application.dataPath + internalPath);
            }
            
            File.WriteAllText(Application.dataPath + internalPath + "/" + saveName + ".json", JsonConvert.SerializeObject(objectToSave));

            Debug.Log(saveName + " Saved at: " + internalPath);
        }



        public static T LoadJSONByName<T>(string path, string filename)
        {
            if (File.Exists(Application.dataPath + path + "/" + filename + ".json"))
            {
                var fileToLoad = File.ReadAllText(Application.dataPath + path + "/" + filename + ".json");


                if (fileToLoad == null)
                {

                    return default(T);
                }
                else
                {
                    Debug.Log(filename + " has been loaded successfully");

                    return JsonConvert.DeserializeObject<T>(fileToLoad);
                }
            }
            else { Debug.Log(Application.dataPath + path + "/" + filename + ".json Not found."); }

            return default(T);
        }

        public static List<T> LoadAllJSONFilesFromFolder<T>(string path)
        {
            DirectoryInfo dirInfo = new DirectoryInfo(Application.dataPath + path);
            DirectoryInfo[] subDirectories = dirInfo.GetDirectories();
            FileInfo[] info = dirInfo.GetFiles("*.json");
            List<T> returnList = new List<T>();

            if (info.Length > 0)
            {
                foreach (FileInfo file in info)
                {
                    var fileToLoad = File.ReadAllText(Application.dataPath + path + "/" + file.Name);

                    if (fileToLoad == null)
                    {
                        string message = string.Format("No files '{0}' not found at path '{1}'.", file.FullName, path);

                        Debug.Log(message);

                        return returnList;
                    }
                    else
                    {
                        returnList.Add(JsonConvert.DeserializeObject<T>(fileToLoad));
                    }
                }
            }

            return returnList;
        }

        public static T LoadJSONFile<T>(string path, string fileName)
        {
            FileInfo file = new FileInfo(Application.dataPath + "/" + path + "/" + fileName + ".json");
            string fileToLoad = File.ReadAllText(file.FullName);

            return JsonConvert.DeserializeObject<T>(fileToLoad);
        }

        public static IEnumerator<T> LoadAllJSONFilesFromSubFolders<T>(string path, System.Action<List<T>> callback)
        {
            DirectoryInfo dirInfo = new DirectoryInfo(Application.dataPath + path);
            DirectoryInfo[] subDirectories = dirInfo.GetDirectories();
            List<T> returnList = new List<T>();

            if (subDirectories.Length == 0) callback(returnList);

            foreach (DirectoryInfo subDir in subDirectories)
            {
                FileInfo[] info = subDir.GetFiles("*.json");

                if (info.Length == 0) { subDir.Delete(); }

                foreach (FileInfo file in info)
                {
                    string fileToLoad = File.ReadAllText(file.FullName);

                    if (fileToLoad == null)
                    {
                        string message = string.Format("No files '{0}' not found at path '{1}'.", file.FullName, path);

                        Debug.Log(message);

                        callback(returnList);
                    }
                    else
                    {
                        returnList.Add(JsonConvert.DeserializeObject<T>(fileToLoad));
                    }
                }
            }
            callback(returnList);
            yield return default(T);
        }

        public static IEnumerator<T> LoadAllJSONFilesFromExternalSubFolders<T>(string path, System.Action<List<T>> callback)
        {
            DirectoryInfo dirInfo = new DirectoryInfo(path);
            DirectoryInfo[] subDirectories = dirInfo.GetDirectories();
            List<T> returnList = new List<T>();

            if (subDirectories.Length == 0) callback(returnList);

            foreach (DirectoryInfo subDir in subDirectories)
            {
                FileInfo[] info = subDir.GetFiles("*.json");

                if (info.Length == 0) yield break;

                foreach (FileInfo file in info)
                {
                    string fileToLoad = File.ReadAllText(file.FullName);

                    if (fileToLoad == null)
                    {
                        string message = string.Format("No files '{0}' not found at path '{1}'.", file.FullName, path);

                        Debug.Log(message);

                        callback(returnList);
                    }
                    else
                    {
                        returnList.Add(JsonConvert.DeserializeObject<T>(fileToLoad));
                    }
                }
            }
            callback(returnList);
            yield return default(T);
        }

        public static void DeleteFolder(string path)
        {
            Directory.Delete(path);
        }

        public static bool CheckFileExistence(string path)
        {
            if (File.Exists(path))
            {
                return true;
            }
            return false;

        }
    }
}

