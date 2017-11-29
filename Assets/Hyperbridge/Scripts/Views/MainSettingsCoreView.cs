using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using Hyperbridge.Wallet;


public class MainSettingsCoreView : MonoBehaviour
{

    public InputField walletPathDisplay;

    void Start()
    {

        CodeControl.Message.AddListener<WalletPathChangedEvent>(OnWalletPathChangedEvent);

    }


    void OnWalletPathChangedEvent(WalletPathChangedEvent e)
    {
        walletPathDisplay.text = e.path;
    }



}
