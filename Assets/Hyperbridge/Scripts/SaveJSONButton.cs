using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class SaveJSONButton : MonoBehaviour
{
    public InputField[] inputs;

    public void SaveJSON()
    {
        ExtensionInfo dataToSave = new ExtensionInfo();
        foreach (InputField input in inputs)
        {
            switch (input.name)
            {
                case "NameInput":
                    dataToSave.name = input.text;
                    break;
                case "DescriptionInput":
                    dataToSave.descriptionText = input.text;
                    break;
                case "InstallsInput":
                    dataToSave.installs = int.Parse(input.text);

                    break;
                case "VersionInput":
                    dataToSave.version = input.text;

                    break;
                case "UpdateDateInput":
                    dataToSave.updateDate = input.text;

                    break;
                case "URLInput":
                    dataToSave.path = input.text;

                    break;
                case "ImageURLInput":
                    dataToSave.imagePath = input.text;
                    break;
                case "RatingInput":
                    dataToSave.rating = float.Parse(input.text);

                    break;
                case "TagsInput":
                    dataToSave.tags = input.text;

                    break;
            }
        }

        string ID = Guid.NewGuid().ToString();
        AppManager.instance.saveDataManager.SaveExtensionJSON(ID, dataToSave);
    }
}
