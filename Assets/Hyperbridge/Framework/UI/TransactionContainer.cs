using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TransactionContainer : MonoBehaviour
{

    public RectTransform header;
    public Image icon;
    private bool compressed;
    private Vector2 extendedSize = new Vector2(1500, 193); //This is just a sample number that looks good
    private Vector2 compressedSize = new Vector2(1500, 83); //Same as above.
    public Text dateDisplayText, typeText, valueText, dateText, transactionIDText, toText, nowText, oldDate, oldDateText;
    private Button toggleButton;

    private void Start()
    {
        this.compressed = true;
        this.header = GetComponent<RectTransform>();
    }

    public void SetupContainer(string date, string type, string value, string transactionID, string oldDate)
    {

    }

    public void ToggleButtonSize()
    {
        if (this.compressed)
        {
            this.header.sizeDelta = this.extendedSize;
            this.compressed = false;
        }
        else
        {
            this.header.sizeDelta = this.compressedSize;
            this.compressed = true;
        }
    }
}
