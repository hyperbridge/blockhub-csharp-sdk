using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonHighlightManager : MonoBehaviour {

    public Button[] buttons;
    Color32 semiTransparentGrayColor;
    void Start () {
        semiTransparentGrayColor = new Color32(1, 1, 1, 136 / 255);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void AppearSelectedDeselectOthers(Button button)
    {
        foreach (Button b in buttons)
        {
            if (b != button)
            {
                b.GetComponentInChildren<Text>().color = semiTransparentGrayColor;
            }
        }

       
    }
}
