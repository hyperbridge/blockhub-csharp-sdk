using Hyperbridge.Wallet;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hyperbridge.Ethereum
{
    public class Ethereum : ITokenSource
    {
        protected Ethereum() { }

        public string Code => "ETH";
        public string Name => "Ethereum";
        public string Description => "General-purpose Contract-based blockchain based on Ether.";

        private static Ethereum _Instance;
        public static Ethereum Instance => _Instance ?? (_Instance = new Ethereum());
    }
}
