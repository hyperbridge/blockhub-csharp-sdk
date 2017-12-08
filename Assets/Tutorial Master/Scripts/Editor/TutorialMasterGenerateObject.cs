#if UNITY_EDITOR

using UnityEditor;
using UnityEngine;

public class TutorialMasterGenerateObject : Editor
{
    [MenuItem("Window/Tutorial Master/Generate a Tutorial Master GameObject")]
    private static void GenerateTMObject()
    {
        if (GameObject.FindObjectOfType<TutorialMasterScript>() == null)            //check whether or not there is an object that has TutorialMasterScript attached to it
        {
            GameObject tm_obj = new GameObject("Tutorial Master");                  //create a new gameobject and name it "Tutorial Master"
            tm_obj.AddComponent<TutorialMasterScript>();                            //attach TutorialMasterScript component
            tm_obj.AddComponent<TutorialMasterEventsSystem>();                      //attach TutorialMasterEventsSystem component
        }
        else
        {
            Debug.Log("There is already another Tutorial Master. Space Time Paradox has been prevented...");
        }
    }
}

#endif