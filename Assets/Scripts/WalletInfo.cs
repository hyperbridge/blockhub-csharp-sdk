using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalletInfo {

    public string name, address, publicKey,privateKey,secret;
    
    public void SetupWallet(string walletName, string walletAddress,string walletPublic, string walletPrivate, string walletSecret)
    {
        name = walletName;
        address = walletAddress;
        publicKey = walletPublic;
        privateKey = walletPrivate;
        secret = walletSecret;
    }

    public void SetupWallet(string walletName, string walletAddress, string walletPrivate)

    {

        name = walletName;
        address = walletAddress;
        privateKey = walletPrivate;


    }
}
