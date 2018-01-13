using Hyperbridge.Wallet;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hyperbridge.Ethereum
{
    public class Ether : ITokenSource
    {
        protected Ether() { }

        public string Code => "ETH";
        public string Name => "Ethereum";
        public string Description => "General-purpose Contract-based blockchain based on Ether.";

        private static Ether _Instance;
        public static Ether Instance => _Instance ?? (_Instance = new Ether());
    }
}
