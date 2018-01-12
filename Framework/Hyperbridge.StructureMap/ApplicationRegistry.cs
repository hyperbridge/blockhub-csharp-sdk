using Hyperbridge.Data;
using Hyperbridge.Data.FileSystem;
using Hyperbridge.Ethereum;
using Hyperbridge.Services;
using Hyperbridge.Wallet;
using StructureMap;

namespace Hyperbridge.StructureMap
{
    class ApplicationRegistry : Registry
    {
        public ApplicationRegistry(string profileDirectory)
        {
            For<ISaver<Profile>>().Use<FileSystemProfileSaver>()
                .Ctor<string>("rootDirectory").Is(profileDirectory)
                .Singleton();

            For(typeof(ILoader<>)).Use(typeof(FileSystemLoader<>)).Singleton();
            For<ICoinCurrency>().Use(Ether.Instance).Singleton();
        }
    }
}
