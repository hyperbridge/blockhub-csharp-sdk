#if UNITY_EDITOR

using UnityEditor;

[CustomEditor(typeof(TutorialMasterEventsSystem))]
public class TutorialMasterEventsInspector : Editor
{
    private SerializedProperty property_OnTutorialStartEvent;
    private SerializedProperty property_OnTutorialEndEvent;
    private SerializedProperty property_OnFrameEnterEvent;

    private void OnEnable()
    {
        property_OnTutorialStartEvent = serializedObject.FindProperty("OnTutorialStart");
        property_OnTutorialEndEvent = serializedObject.FindProperty("OnTutorialEnd");
        property_OnFrameEnterEvent = serializedObject.FindProperty("OnFrameEnter");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.HelpBox("The following event would run when any Tutorial STARTS", MessageType.Info);
        EditorGUILayout.PropertyField(property_OnTutorialStartEvent);

        EditorGUILayout.HelpBox("The following event would run when any Tutorial ENDS", MessageType.Info);
        EditorGUILayout.PropertyField(property_OnTutorialEndEvent);

        EditorGUILayout.HelpBox("The following event would run when a Frame STARTS. It may occur when either next or previous frame is played", MessageType.Info);
        EditorGUILayout.PropertyField(property_OnFrameEnterEvent);

        serializedObject.ApplyModifiedProperties();
    }
}

#endif