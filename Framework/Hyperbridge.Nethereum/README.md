# Hyperbridge.Nethereum Library
This library contains the Nethereum implementation details.

## Library

### `Bip39SeedGenerator : ISeedGenerator<string>`
Generates a mnemonic phrase for use with Nethereum. As of now, the implementation is
utilizing the Nethereum libraries so it is sitting with this library.

```csharp
var generator = new Bip39SeedGenerator(NBitcoin.Wordlist.English, WordCount.Twelve);
var phrase = generator.Generate();
```

### `NethereumAccount : IAccount<Ether>`
The Nethereum account implementation. Requires an `Nethereum.Web3.Accounts.Account`
to be passed in. It is better to get the account back from the wallet:

> TODO: Talk more about why an account is needed and how it is used in
> a blockchain implementation.

```csharp
var wallet = new NetherumWallet("...seed word list...");
var account = wallet.GetAccount("address"); 
// Alternative: wallet.GetAccount(index);
```

### `NethereumBalanceRead : IBalanceRead<Ether>`
The Nethereum Balance Reader implementation that will reach out and retrieve an 
accounts balance from a given client. The URL to the client is required. This implementation
utilizes `Web3`.

```csharp
var reader = new NethereumBalanceRead("...URL...");
ICoin<Ether> balance = await reader.GetBalance(new Ethereum.ToEthereumAccount("...Address..."));
```

### `NethereumHdWallet : IWallet<Ether>`
The Nethereum wallet implementation. Requires the word list to be
passed in as the seed key.

> TODO: Talk more about what a seed key is and how to generate one.

```csharp
var wallet = new NetherumWallet("...seed word list...");
```

### `NethereumTransactionWrite : ITransactionWrite<Ether>`
The Nethereum transaction writer implementation. Allows a consumer to send ether from one account
to another.

```csharp
var writer = new NethereumTransactionWrite("...URL...");
var wallet = new NethereumHdWallet("...Phrase...");
var fromAccount = wallet.GetAccount("...From Address...");
var toAccount = new Ethereum.ToEthereumAccount("...To Address...");

// Send 0.0001 ETH
TransactionSentResponse<Ether> response = await writer.SendTransactionAsync(fromAccount, toAccount, new EtherCoin(0.0001M));
```

## Adding Nethereum to a Project
install the NuGet package `Nethereum.Web3` by calling
`Install-Package Nethereum.Web3`