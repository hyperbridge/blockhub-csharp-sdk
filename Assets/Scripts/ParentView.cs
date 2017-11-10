using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Devdog.General.UI;

public class ParentView : MonoBehaviour
{

    List<UIWindowPage> allPages = new List<UIWindowPage>();
    // Use this for initialization
    void Start()
    {
        foreach (UIWindowPage uiWindow in GetComponentsInChildren<UIWindowPage>())
        {
            allPages.Add(uiWindow);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void DeactivateAllPagesButOne(UIWindowPage stayActive)
    {
        foreach(UIWindowPage page in allPages)
        {
            if(page != stayActive)
            {
                page.Hide();
            }
        }
    }
}
