#pragma warning disable 0168 // variable declared but not used.
#pragma warning disable 0219 // variable assigned but not used.
#pragma warning disable 0414 // private field assigned but not used.
#pragma warning disable 0649 // default value null


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using Hyperbridge.Extension;

namespace Hyperbridge.Core
{
    public class SaveDataManager : MonoBehaviour
    {
        private Text saveText, loadText;


        public IEnumerator SaveCurrentExtensionData()
        {

            Database.SaveJSON<List<ExtensionInfo>>("/Resources/Extensions", "community-extensions", AppManager.instance.extensionManager.extensionList.communityExtensions);
            yield return new WaitForSeconds(0.5f);
            Database.SaveJSON<List<ExtensionInfo>>("/Resources/Extensions", "extensions", AppManager.instance.extensionManager.extensionList.installedExtensions);

        }

        public void SaveExtensionJSON(string ID, ExtensionInfo data)
        {
            Database.SaveJSON<ExtensionInfo>("/Resources/Extensions/", data.name, data);
        }

        public IEnumerator DeleteSpecificJSON(string saveName, string path)
        {
            FileInfo file = new FileInfo(Application.dataPath + path + "/" + saveName + ".json");
            //Debug.Log(IsFileLocked(file));
          /*  while (IsFileLocked(file)) For testing purposes
            {
                Debug.Log("Waiting");
                yield return new WaitForSeconds(0.5f);
            }
            */

            if (File.Exists(Application.dataPath + path + "/" + saveName + ".json"))
            {
                File.Delete(Application.dataPath + path + "/" + saveName + ".json");
                Debug.Log("File at: " + Application.dataPath + path + "/" + saveName + ".json" + " has been deleted");
                //deleted(true);
            }
            else
            {
                //  deleted(false);
                throw new FileNotFoundException();

            }
            yield return null;
        }

        protected virtual bool IsFileLocked(FileInfo file)
        {
            FileStream stream = null;
            try
            {
                stream = file.Open(FileMode.Open, FileAccess.Read, FileShare.None);
            }
            catch (IOException ex)
            {
                Debug.Log(ex.StackTrace);
                //the file is unavailable because it is:
                //still being written to
                //or being processed by another thread
                //or does not exist (has already been processed)
                return true;
            }
            finally
            {
                if (stream != null)
                    stream.Close();
            }

            //file is not locked
            return false;
        }
    }
}