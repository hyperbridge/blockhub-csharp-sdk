using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UpdateWalletsEvent : CodeControl.Message
{
    public List<WalletInfo> wallets;
}
