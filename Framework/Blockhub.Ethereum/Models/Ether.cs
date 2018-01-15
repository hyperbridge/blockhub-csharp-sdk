using NBitcoin.BouncyCastle.Math;

namespace Blockhub.Ethereum
{
    public class Ether : ICurrency<Ethereum>
    {
        private const decimal FROM_WEI_MULTIPLIER = 1E-18M;
        private const decimal TO_WEI_MULTIPLIER = 1E18M;

        public Ether(decimal amount)
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
        public static implicit operator Wei(Ether ether)
        {
            return new Wei(new BigInteger((ether.Amount * TO_WEI_MULTIPLIER).ToString()));
        }

        public static Ether operator +(Ether left, Ether right)
        {
            return new Ether(left.Amount + right.Amount);
        }

        public static implicit operator Ether(Wei wei)
        {
            return new Ether(wei.Amount * FROM_WEI_MULTIPLIER);
        }

        public static Ether operator -(Ether left, Ether right)
        {
            return new Ether(left.Amount - right.Amount);
        }

        public static Ether operator +(Ether left, Wei right)
        {
            return new Ether(left.Amount + (right.Amount * FROM_WEI_MULTIPLIER));
        }

        public static Ether operator -(Ether left, Wei right)
        {
            return new Ether(left.Amount - (right.Amount * FROM_WEI_MULTIPLIER));
        }

        public static Wei operator +(Wei left, Ether right)
        {
            return new Wei(new BigInteger((left.Amount + (right.Amount * TO_WEI_MULTIPLIER)).ToString()));
        }

        public static Wei operator -(Wei left, Ether right)
        {
            return new Wei(new BigInteger((left.Amount - (right.Amount * TO_WEI_MULTIPLIER)).ToString()));
        }
        #endregion
    }
}
