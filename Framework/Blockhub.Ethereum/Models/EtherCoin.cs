using NBitcoin.BouncyCastle.Math;

namespace Blockhub.Ethereum
{
    public class EtherCoin : IToken<Ethereum>
    {
        private const decimal FROM_WEI_MULTIPLIER = 1E-18M;
        private const decimal TO_WEI_MULTIPLIER = 1E18M;

        public EtherCoin(decimal amount)
        {
            Amount = amount;
        }

        public decimal Amount { get; }
        public string Unit => "ETH";
        public string Name => "Ether";

        public Ethereum TokenType => Ethereum.Instance;

        public string ToDisplayAmount()
        {
            return ToString();
        }

        public override string ToString()
        {
            return $"{Amount} {Unit}";
        }

        public BigInteger ToTransactionAmount()
        {
            var converted = Amount * TO_WEI_MULTIPLIER;
            return new BigInteger(converted.ToString("0"));
        }

        #region Operators
        public static implicit operator WeiCoin(EtherCoin ether)
        {
            return new WeiCoin(new BigInteger((ether.Amount * TO_WEI_MULTIPLIER).ToString()));
        }

        public static EtherCoin operator +(EtherCoin left, EtherCoin right)
        {
            return new EtherCoin(left.Amount + right.Amount);
        }

        public static implicit operator EtherCoin(WeiCoin wei)
        {
            return new EtherCoin(wei.Amount * FROM_WEI_MULTIPLIER);
        }

        public static EtherCoin operator -(EtherCoin left, EtherCoin right)
        {
            return new EtherCoin(left.Amount - right.Amount);
        }

        public static EtherCoin operator +(EtherCoin left, WeiCoin right)
        {
            return new EtherCoin(left.Amount + (right.Amount * FROM_WEI_MULTIPLIER));
        }

        public static EtherCoin operator -(EtherCoin left, WeiCoin right)
        {
            return new EtherCoin(left.Amount - (right.Amount * FROM_WEI_MULTIPLIER));
        }

        public static WeiCoin operator +(WeiCoin left, EtherCoin right)
        {
            return new WeiCoin(new BigInteger((left.Amount + (right.Amount * TO_WEI_MULTIPLIER)).ToString()));
        }

        public static WeiCoin operator -(WeiCoin left, EtherCoin right)
        {
            return new WeiCoin(new BigInteger((left.Amount - (right.Amount * TO_WEI_MULTIPLIER)).ToString()));
        }
        #endregion
    }
}
