using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public  class CopyToSystemClipboard
{
    public void CopyStringToSystemClipboard(string text)
    {
        TextEditor te = new TextEditor();
        te.text = text;
        te.SelectAll();
        te.Copy();
    }
}
