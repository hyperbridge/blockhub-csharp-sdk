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
            var etherCoin = new Ether(1M);
            var weiCoin = (Wei)etherCoin;

            Assert.AreEqual(1E18M, weiCoin.Amount);
        }

        [TestMethod]
        public void SingleWeiToEther()
        {
            var weiCoin = new Wei(new NBitcoin.BouncyCastle.Math.BigInteger("1"));
            var etherCoin = (Ether)weiCoin;

            Assert.AreEqual(1E-18M, etherCoin.Amount);
        }
    }
}
