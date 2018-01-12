using Hyperbridge.Wallet;

namespace Hyperbridge.Services.Abstract
{
    public interface IDependencyRoot
    {
        T Resolve<T>();
    }
}
