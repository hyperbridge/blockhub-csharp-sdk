using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public  class CopyToSystemClipboard
{
    public void CopyStringToSystemClipboard(string args)
    {
        EditorGUIUtility.systemCopyBuffer = args;
    }

    public void CopyImageToSystemClipboard(Sprite img)
    {

    }
}
