# Hyperbridge.Services Library
The Hyperbridge Services library is the primary external library. It was created to simplify
interfaces for use in limited framework environments. Removal of generics here allows a UI to
work by simply providing a selected `ICoinCurrency` as needed and resolving the references 
that those implementations provide.

# Examples
There are examples of using the various functions within the `Hyperbridge.Tests` library 
under the `Examples` folder.

# Library

## Abstract
Abstractions that should be utilized by the UI.

### `IAccountBalanceReader`
Interface that retrieves the balance of an account

### `IAccountCreator`
Interface that creates an account for a given wallet and name for the account.

### `IDependencyRoot`
Interface that can be used to resolve depedencies on the fly
(similar to the service locator pattern).

### `IResolver`
Interface that allows the consumer to resolve dependencies with an instance of `ICoinCurrency`.

### `ITransactionRead`
Interface that allows reading of the last x transactions for an account. Can retrieve specifically
sent/received transasctions.

### `ITransactionWrite`
Interface that allows sending a basic coin transaction from one account to another.

### `IWalletCreator`
Interface that allows creating a wallet with / without a provided phrase (or secret seed).

## Application
Application-level implementations that are indepedenct of blockchain

### `DependencyRootResolver : IResolver`
Returns the proper depedency root for a given currency by searching a dictionary provided by the 
consumer.

```csharp
var resolver = new DependencyRootResolver();
resolver.AddRoot(Ether.Instance, new EthereumDependencyRoot);
resolver.AddRoot(Bitcoin.Instance, new BitcoinDependencyRoot);

// Return a Ether-specific IWalletCreator
var instance = resolver.Resolve<IWalletCreator>(Ether.Instance);
```

### `FileSystemProfileSaver : ISaver<Profile>`
Implementation of ISaver that saves a profile in a special directory and names it based
on the profile's id.

```csharp
var saver = new FileSystemProfileSaver("... directory ...");
var profileUri = saver.Save(profileModel);
```

## Ethereum
Ethereum-specific implementation of the services
> TODO: If possible, these classes should be moved to the `Hyperbridge.Ethereum` library.

### `EthereumAccountBalanceReader : IAccountBalanceReader`
An Ethereum-based Balance Reader.

### `EthereumAccountCreator : IAccountCreator`
An Ethereum-based Account Creator.

### `EthereumTransactionRead : ITransactionRead`
An Ethereum-based transaction reader.

### `EthereumTransactionWrite : ITransactionWrite`
An Ethereum-based transaction writer.

### `EthereumWalletCreator : IWalletCreator`
An Ethereum-based wallet creator.

## Models
Models of the system for the frontend specifically. These do not use generics
in order to make them easier to handle.

### `Account`
An account model that holds key information for an individual account.

### `AccountBalance`
The response to the retrieving an account balance.

### `Notification`
A notification that occurs in the UI. Saved with the Profile

### `Profile`
A user's profile model

### `ReceiveTransaction`
A response to retrieving Receive Transactions only

### `SendTransaction`
A response to retrieving Send Transactions only

### `Transaction`
A response to retrieving transactions.

### `Wallet`
A wallet model that holds key information for an individual wallet. Passwords are NOT stored here.