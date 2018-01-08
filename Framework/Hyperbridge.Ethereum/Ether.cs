using Hyperbridge.Transaction;
using Hyperbridge.Wallet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hyperbridge.Ethereum
{
    public class Ether : ICoinCurrency
    {
        protected Ether() { }

        public virtual string Code => "ETH";

        private static Ether _Instance;
        public static Ether Instance => _Instance ?? (_Instance = new Ether());
    }
}
