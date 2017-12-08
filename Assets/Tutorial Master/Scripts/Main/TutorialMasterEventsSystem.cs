using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
[AddComponentMenu("Tutorial Master/ Tutorial Master Events System")]
[RequireComponent(typeof(TutorialMasterScript))]
public class TutorialMasterEventsSystem : MonoBehaviour
{
    public UnityEvent OnTutorialStart;
    public UnityEvent OnTutorialEnd;

    public UnityEvent OnFrameEnter;
}