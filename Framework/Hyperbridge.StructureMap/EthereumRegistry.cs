using Hyperbridge.Ethereum;
using Hyperbridge.Services.Abstract;
using Hyperbridge.Services.Ethereum;
using Hyperbridge.Transaction;
using Hyperbridge.Wallet;
using StructureMap;

namespace Hyperbridge.StructureMap
{
    class EthereumRegistry : Registry
    {
        public EthereumRegistry(string rpcClientUrl, string etherScanApiKey)
        {
            For<IAccountBalanceReader>().Use<EthereumAccountBalanceReader>().Singleton();
            For<IBalanceRead<Ether>>().Use<Nethereum.NethereumBalanceRead>()
                .Ctor<string>("url").Is(rpcClientUrl)
                .Singleton();

            For<IAccountCreator>().Use<EthereumAccountCreator>().Singleton();
            For<ITransactionRead>().Use<EthereumTransactionRead>().Singleton();
            For<ILastTransactionRead<EthereumTransaction>>().Use<EtherScan.EtherScanLastTransactionRead>()
                .Ctor<string>("apiKey").Is(etherScanApiKey)
                .Singleton();

            For<ITransactionWrite>().Use<EthereumTransactionWrite>().Singleton();
            For<ITransactionWrite<Ether>>().Use<Nethereum.NethereumTransactionWrite>()
                .Ctor<string>("url").Is(rpcClientUrl)
                .Singleton();

            For<IWalletCreator>().Use<EthereumWalletCreator>().Singleton();
        }
    }
}
