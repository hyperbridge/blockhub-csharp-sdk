using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public  class CopyToSystemClipboard
{
    public void CopyStringToSystemClipboard(string args)
    {
        #if UNITY_EDITOR
        EditorGUIUtility.systemCopyBuffer = args;
        #endif
    }

    public void CopyImageToSystemClipboard(Sprite img)
    {

    }
}
