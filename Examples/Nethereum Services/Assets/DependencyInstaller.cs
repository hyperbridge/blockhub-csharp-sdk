using UnityEngine;
using Zenject;
using Blockhub;
using Blockhub.Data;
using Blockhub.Nethereum;
using Blockhub.Ethereum;
using Blockhub.Transaction;
using Blockhub.EtherScan;
using Blockhub.Wallet;

public class DependencyInstaller : MonoInstaller<DependencyInstaller>
{
    public override void InstallBindings()
    {
        const string PROFILE_DIRECTORY = "";
        const string ETHERSCAN_API_KEY = "";
        const string ETHEREUM_CLIENT_URI = "";
        const int MAX_ACCOUNT_SEARCH_COUNT = 20;

        Container.Bind<IProfileContextAccess>()
            .To<ProfileContextAccess>()
            .AsSingle();

        Container.Bind<ISave<Profile>>()
            .To<FileSystemProfileSave>()
            .AsSingle();

        Container.Bind<FileSystemJsonLoad<Profile>>()
            .AsSingle()
            .WithArguments(PROFILE_DIRECTORY);
            
        Container.Bind<ILoad<Profile>>()
            .FromMethod(c =>
            {
                var fsLoad = c.Container.Resolve<FileSystemJsonLoad<Profile>>();
                var pca = c.Container.Resolve<IProfileContextAccess>();
                return new LoadProfileContextLoad(fsLoad, pca);
            })
            .AsSingle();

        Container.Bind<ISeedGenerate<string>>()
            .To<Bip39SeedGenerate>()
            .AsSingle();

        // Token Sources
        Container.Bind<ITokenSource>()
            .FromInstance(Ethereum.Instance)
            .AsSingle();

        // Ethereum
        Container.Bind<ILastTransactionRead<EthereumTransaction>>()
            .To<EtherScanLastTransactionRead>()
            .AsSingle()
            .WithArguments(ETHERSCAN_API_KEY);

        Container.Bind<NethereumTransactionWrite>()
            .AsSingle()
            .WithArguments(ETHEREUM_CLIENT_URI);

        Container.Bind<NethereumPrivateKeyGenerate>()
            .AsSingle();

        Container.Bind<ITransactionWrite<Ethereum>>()
            .FromMethod(c =>
            {
                var writer = c.Container.Resolve<NethereumTransactionWrite>();
                var generator = c.Container.Resolve<NethereumPrivateKeyGenerate>();
                return new LoadMissingPrivateKeyTransactionWrite<Ethereum>(writer, generator);
            })
            .AsSingle();

        Container.Bind<NethereumAccountCreate>()
            .FromMethod(c => new NethereumAccountCreate(MAX_ACCOUNT_SEARCH_COUNT))
            .AsSingle();

        Container.Bind<IAccountCreate<Ethereum>>()
            .FromMethod(c =>
            {
                var creator = c.Container.Resolve<NethereumAccountCreate>();
                var pca = c.Container.Resolve<IProfileContextAccess>();
                var saver = c.Container.Resolve<ISave<Profile>>();
                return new AutoSaveProfileAccountCreate<Ethereum>(creator, pca, saver);
            })
            .AsSingle();

        Container.Bind<EthereumWalletCreate>()
            .AsSingle();

        Container.Bind<IWalletCreate<Ethereum>>()
            .FromMethod(c =>
            {
                var creator = c.Container.Resolve<EthereumWalletCreate>();
                var accountCreator = c.Container.Resolve<IAccountCreate<Ethereum>>();
                var balanceReader = c.Container.Resolve<IBalanceRead<Ethereum>>();

                var search = new SearchForAccountsByBalanceWalletCreate<Ethereum>(creator, accountCreator, balanceReader);

                var pca = c.Container.Resolve<IProfileContextAccess>();
                var autoAdd = new AutoAddToProfileWalletCreate<Ethereum>(search, pca);

                var saver = c.Container.Resolve<ISave<Profile>>();
                return new AutoSaveProfileWalletCreate<Ethereum>(autoAdd, pca, saver);
            })
            .AsSingle();
    }
}