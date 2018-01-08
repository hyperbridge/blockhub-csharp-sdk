using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hyperbridge.Wallet
{
    /// <summary>
    /// A general blockchain account with Address and Private Key funds can be sent to and from.
    /// </summary>
    public interface IAccount<out T> where T : ICoinCurrency
    {
        /// <summary>
        /// Public address of the blockchain account.
        /// </summary>
        string Address { get; }

        /// <summary>
        /// Private Key of the blockchain account.
        /// </summary>
        string PrivateKey { get; }
    }
}