using System;
using System.Numerics;
using Hyperbridge.Ethereum;
using Hyperbridge.Transaction;
using Hyperbridge.Wallet;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Hyperbridge.Nethereum
{
    [TestClass]
    public class CoinTests
    {
        [TestMethod]
        public void SingleEtherToWei()
        {
            var etherCoin = new EtherCoin(1M);
            var weiCoin = (WeiCoin)etherCoin;

            Assert.AreEqual(1E18M, weiCoin.Amount);
        }

        [TestMethod]
        public void SingleWeiToEther()
        {
            var weiCoin = new WeiCoin((BigInteger)1M);
            var etherCoin = (EtherCoin)weiCoin;

            Assert.AreEqual(1E-18M, etherCoin.Amount);
        }
    }
}
