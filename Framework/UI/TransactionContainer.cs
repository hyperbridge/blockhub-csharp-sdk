using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TransactionContainer : MonoBehaviour
{

    public RectTransform header;
    public Image icon;
    bool compressed;
    Vector2 extendedSize = new Vector2(1500, 193); //This is just a sample number that looks good
    Vector2 compressedSize = new Vector2(1500, 83); //Same as above.
    public Text dateDisplayText, typeText, valueText, dateText, transactionIDText, toText, nowText, oldDate, oldDateText;
    Button toggleButton;
    private void Start()
    {
        compressed = true;
        header = GetComponent<RectTransform>();

    }

    public void SetupContainer(string date, string type, string value, string transactionID, string oldDate)
    {

    }

    public void ToggleButtonSize()
    {
        if (compressed)
        {
            header.sizeDelta = extendedSize;
            compressed = false;
        }
        else
        {
            header.sizeDelta = compressedSize;
            compressed = true;
        }
    }
}
