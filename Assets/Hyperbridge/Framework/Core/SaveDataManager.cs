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
            
            Database.SaveJSON<List<ExtensionInfo>>("/Resources/Extensions","community-extensions", AppManager.instance.extensionManager.extensionList.communityExtensions);
            yield return new WaitForSeconds(0.5f);
            Database.SaveJSON<List<ExtensionInfo>>("/Resources/Extensions","extensions", AppManager.instance.extensionManager.extensionList.installedExtensions);

        }

        public void SaveExtensionJSON(string ID, ExtensionInfo data)
        {
            Database.SaveJSON<ExtensionInfo>("/Resources/Extensions/",data.name, data);
        }

        public void DeleteSpecificSave(string saveName, string path)
        {
            Debug.Log(Application.dataPath + path + "/" + saveName + ".json");
            if (File.Exists(Application.dataPath + path + "/" + saveName + ".json"))
            {
                File.Delete(Application.dataPath + path + "/" + saveName + ".json");
            }
            else
            {
                throw new FileNotFoundException();
            }
        }

       
    }
}