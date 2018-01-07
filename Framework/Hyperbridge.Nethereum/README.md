# Hyperbridge.Nethereum Library
This library contains the Nethereum implementation details.

## Library

### `NethereumAccount`
The Nethereum account implementation. Requires an `Nethereum.Web3.Accounts.Account`
to be passed in. It is better to get the account back from the wallet:

> TODO: Talk more about why an account is needed and how it is used in
> a blockchain implementation.

```csharp
var wallet = new NetherumWallet("...seed word list...");
var account = wallet.GetAccount("address"); 
// Alternative: wallet.GetAccount(index);
```

### `NethereumWallet`
The Nethereum wallet implementation. Requires the word list to be
passed in as the seed key.

> TODO: Talk more about what a seed key is and how to generate one.

```csharp
var wallet = new NetherumWallet("...seed word list...");
```

## Adding Nethereum to a Project
install the NuGet package `Nethereum.Web3` by calling
`Install-Package Nethereum.Web3`