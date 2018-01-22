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

        [TestMethod]
        public void TenEtherToWei()
        {
            var etherCoin = new Ether(10M);
            var weiCoin = (Wei)etherCoin;

            Assert.AreEqual("10000000000000000000", weiCoin.ToTransactionAmount().ToString());
        }

        [TestMethod]
        public void SmallDecimalEtherToWei()
        {
            var etherCoin = new Ether(0.00512M);
            var weiCoin = (Wei)etherCoin;

            Assert.AreEqual("5120000000000000", weiCoin.ToTransactionAmount().ToString());
        }

        [TestMethod]
        public void LargeDecimalEtherToWei()
        {
            var etherCoin = new Ether(36.339918223663372027M);
            var weiCoin = (Wei)etherCoin;

            Assert.AreEqual("36339918223663372027", weiCoin.ToTransactionAmount().ToString());
        }

        [TestMethod]
        public void LargeDecimalEtherToWeiTruncate()
        {
            var etherCoin = new Ether(36.339918223663372027445M);
            var weiCoin = (Wei)etherCoin;

            Assert.AreEqual("36339918223663372027", weiCoin.ToTransactionAmount().ToString());
        }

        [TestMethod]
        public void LargeWeiToEther()
        {
            var weiCoin = new Wei(new NBitcoin.BouncyCastle.Math.BigInteger("36339918223663372027"));
            var etherCoin = (Ether)weiCoin;

            Assert.AreEqual(36.339918223663372027M, etherCoin.Amount);
        }

        [TestMethod]
        public void SmallWeiToEther()
        {
            var weiCoin = new Wei(new NBitcoin.BouncyCastle.Math.BigInteger("435000000000000"));
            var etherCoin = (Ether)weiCoin;

            Assert.AreEqual(0.000435M, etherCoin.Amount);
        }
    }
}
