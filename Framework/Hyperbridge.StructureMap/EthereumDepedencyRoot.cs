using Hyperbridge.Services.Abstract;
using StructureMap;

namespace Hyperbridge.StructureMap
{
    public class EthereumDepedencyRoot : IDependencyRoot
    {
        private Container container { get; }
        public EthereumDepedencyRoot(string rpcClientUrl, string etherScanApiKey)
        {
            var registry = new EthereumRegistry(rpcClientUrl, etherScanApiKey);

            container = new Container(ce =>
            {
                ce.AddRegistry(registry);
            });
        }

        public T Resolve<T>()
        {
            return container.GetInstance<T>();
        }
    }
}
