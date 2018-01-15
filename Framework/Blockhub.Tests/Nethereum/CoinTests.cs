using System;
using System.Numerics;
using Blockhub.Ethereum;
using Blockhub.Transaction;
using Blockhub.Wallet;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Blockhub.Nethereum
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
            var weiCoin = new WeiCoin(new NBitcoin.BouncyCastle.Math.BigInteger("1"));
            var etherCoin = (EtherCoin)weiCoin;

            Assert.AreEqual(1E-18M, etherCoin.Amount);
        }
    }
}
