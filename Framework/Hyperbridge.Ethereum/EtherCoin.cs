using Hyperbridge.Wallet;

namespace Hyperbridge.Ethereum
{
    public class EtherCoin : ICoin<Ether>
    {
        private const decimal FROM_WEI_MULTIPLIER = 1E-18M;
        private const decimal TO_WEI_MULTIPLIER = 1E18M;

        public EtherCoin(decimal amount)
        {
            Amount = amount;
        }

        public decimal Amount { get; }
        public Ether BaseCurrency => Ether.Instance;

        public decimal ToTransactionAmount()
        {
            return Amount;
        }

        public static implicit operator WeiCoin(EtherCoin ether)
        {
            return new WeiCoin(ether.Amount * TO_WEI_MULTIPLIER);
        }

        public static implicit operator EtherCoin(WeiCoin wei)
        {
            return new EtherCoin(wei.Amount * FROM_WEI_MULTIPLIER);
        }

        public static EtherCoin operator +(EtherCoin left, EtherCoin right)
        {
            return new EtherCoin(left.Amount + right.Amount);
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
            return new WeiCoin(left.Amount + (right.Amount * TO_WEI_MULTIPLIER));
        }

        public static WeiCoin operator -(WeiCoin left, EtherCoin right)
        {
            return new WeiCoin(left.Amount - (right.Amount * TO_WEI_MULTIPLIER));
        }
    }
}
