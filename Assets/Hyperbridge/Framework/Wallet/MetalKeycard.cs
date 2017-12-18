using UnityEngine;
using UnityEngine.UI;

namespace Hyperbridge.Wallet
{
    public class MetalKeycard : MonoBehaviour
    {

        public Image QRCode;
        public Text jsonText, accountNameText, dateText, userKeyText;

        public void SetupMetalKeycard (string jsonText, string accountName, string key)
        {
            this.jsonText.text = jsonText;
            this.accountNameText.text = accountName;
            this.userKeyText.text = key;
            var currentDateTime = System.DateTime.Now;
            dateText.text = "Created on " + currentDateTime.Day + "," + currentDateTime.Month + " " + currentDateTime.Day + " " + currentDateTime.Year + " for account";
        }
    }
}

