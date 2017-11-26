using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Hyperbridge.Wallet
{
    public class UpdateWalletsEvent : CodeControl.Message
    {
        public List<WalletInfo> wallets;
    }
}
