using UnityEngine;
using Zenject;
using Blockhub;
using Blockhub.Data;
using Blockhub.Nethereum;
using Blockhub.Ethereum;
using Blockhub.Transaction;
using Blockhub.EtherScan;
using Blockhub.Wallet;
using System.Net;
using System;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;

public class DependencyInstaller : MonoInstaller<DependencyInstaller>
{
    public DependencyInstaller() : base()
    {
        ServicePointManager.ServerCertificateValidationCallback = CertificationCallback;
    }

    private bool CertificationCallback(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
    {
        // NOTE: We need to figure out the best way to handle the certificate store.
        // Mono has its own store and doesn't utilize the system's natural store
        // reference: http://www.mono-project.com/docs/faq/security/
        return true;
    }

    public override void InstallBindings()
    {
        // These constants should be a dependency as well and loaded to easily
        // be extracted
        string PROFILE_DIRECTORY = System.IO.Path.GetFullPath("Profiles\\");
        const string ETHERSCAN_API_KEY = "VYY443VRI78CT32DFB1TJVMR1KZPZK5B92";
        const string ETHEREUM_CLIENT_URI = "https://ropsten.infura.io/ixskS1fXylG7pA5lOOAK";
        const int MAX_ACCOUNT_SEARCH_COUNT = 20;

        Container.Bind<IProfileContextAccess>()
            .To<ProfileContextAccess>()
            .AsSingle();

        Container.Bind<ISave<Profile>>()
            .To<FileSystemProfileSave>()
            .AsSingle()
            .WithArguments(PROFILE_DIRECTORY);

        Container.Bind<FileSystemJsonLoad<Profile>>()
            .AsSingle();
            
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
            .AsSingle()
            .WithArguments(NBitcoin.Wordlist.English, NBitcoin.WordCount.Twelve);

        // Token Sources
        Container.Bind<IBlockchainType>()
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
                return creator;

            })
            .AsSingle();

        Container.Bind<EthereumWalletCreate>()
            .AsSingle();

        Container.Bind<IAccountSearcher<Ethereum>>()
            .To<StandardAccountSearcher<Ethereum>>()
            .AsSingle();

        Container.Bind<IWalletCreate<Ethereum>>()
            .FromMethod(c =>
            {
                var creator = c.Container.Resolve<EthereumWalletCreate>();

                var pca = c.Container.Resolve<IProfileContextAccess>();
                var autoAdd = new AutoAddToProfileWalletCreate<Ethereum>(creator, pca);

                var saver = c.Container.Resolve<ISave<Profile>>();
                return new AutoSaveProfileWalletCreate<Ethereum>(autoAdd, pca, saver);
            })
            .AsSingle();

        Container.Bind<IBalanceRead<Ethereum>>()
            .To<NethereumBalanceRead>()
            .AsSingle()
            .WithArguments(ETHEREUM_CLIENT_URI);
    }
}