using NBitcoin.BouncyCastle.Math;
using System;

namespace Blockhub.Ethereum
{
    public class Ether : ICurrency<Ethereum>
    {
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
            return ToWei(Amount);
        }

        public static BigInteger ToWei(decimal amount)
        {
            const string E18 = "1000000000000000000";

            var str = amount.ToString();
            if (str.Contains("."))
            {
                var parts = str.Split('.');
                if (parts.Length != 2) throw new Exception("Split was not valid.");

                var part1 = parts[0];
                var part2 = parts[1];

                if (part2.Length > 18)
                    part2 = part2.Substring(0, 18);
                else
                    part2 = part2.PadRight(18, '0');

                var part1Int = new BigInteger(part1).Multiply(new BigInteger(E18));
                var part2Int = new BigInteger(part2);

                return part1Int.Add(part2Int);
            } else
            {
                var left = new BigInteger(str);
                var right = new BigInteger(E18);

                var multiplied = left.Multiply(right);
                return multiplied;
            }
        }

        public static decimal FromWei(BigInteger amount)
        {
            var str = amount.ToString();
            if (str.Length > 18)
                str = str.Insert(str.Length - 18, ".");
            else
                str = "0." + str.PadLeft(18, '0');

            return decimal.Parse(str);
        }

        #region Operators
        public static implicit operator Wei(Ether ether)
        {
            return new Wei(ToWei(ether.Amount));
        }

        public static Ether operator +(Ether left, Ether right)
        {
            return new Ether(left.Amount + right.Amount);
        }

        public static implicit operator Ether(Wei wei)
        {
            return new Ether(FromWei(wei.ToTransactionAmount()));
        }

        public static Ether operator -(Ether left, Ether right)
        {
            return new Ether(left.Amount - right.Amount);
        }

        public static Ether operator +(Ether left, Wei right)
        {
            return new Ether(left.Amount + FromWei(right.ToTransactionAmount()));
        }

        public static Ether operator -(Ether left, Wei right)
        {
            return new Ether(left.Amount - FromWei(right.ToTransactionAmount()));
        }

        public static Wei operator +(Wei left, Ether right)
        {
            return new Wei(left.ToTransactionAmount().Add(ToWei(right.Amount)));
        }

        public static Wei operator -(Wei left, Ether right)
        {
            return new Wei(left.ToTransactionAmount().Subtract(ToWei(right.Amount)));
        }
        #endregion
    }
}
