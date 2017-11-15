using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonHighlightManager : MonoBehaviour {

    public Button[] buttons;

    private Color32 semiTransparentGrayColor;

    private void Start () {
        //this.semiTransparentGrayColor = new Color32(1, 1, 1, 136 / 255);
	}
	
    public void AppearSelectedDeselectOthers(Button button)
    {
        //foreach (Button b in buttons) {
        //    if (b != button) {
        //        b.GetComponentInChildren<Text>().color = this.semiTransparentGrayColor;
        //    }
        //}
    }
}
