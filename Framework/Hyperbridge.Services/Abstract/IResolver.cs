using Hyperbridge.Wallet;

namespace Hyperbridge.Services.Abstract
{
    public interface IResolver
    {
        T Resolve<T>(ICoinCurrency currency);
    }
}
