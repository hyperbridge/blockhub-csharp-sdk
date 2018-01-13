# Hyperbridge.Core Library
The Hyperbridge Core library is the core of the Hyperbridge
ecosystem of Blockchain libraries. It is meant to have blockchain
agnostic classes and interfaces so that implementations can be
replaced as needed. It is also written as a library in order
to separate it from the needs of Unity. This library should maintain
separation from any UI framework so that it can be pushed out to
other UIs in the future (Unity, Web Application, Xamarin, etc).

# Library

## Data Interfaces

### `ISaver<T>`
Interface that saves any model to persistent storage. No filename or path is provided
due to the varying nature of different platforms. On some platforms it may be necessary
to store data in a database or a file. The implementation should specify where the model
is to be saved. The result of the call is a uri to locate the resource at a later point.
This should be used when attempting to load it back into memory.

### `ILoader<T>`
Interface that loads a model from persistent storage. A uri must be provided in order
to properly retrieve the resource. It should not be an absolute file path or data. Due
to the nature of cross-platform development, it is best to generalize this to a common
format like the URI schema. This schema can be retrieved from the output of the 
`ISaver<T>` implementation.

## Data Classes

### `FileSystemJsonLoader<T> : ILoader<T>` 
Loads a file from the local disk and assumes it is a json text format. It will
deserialize this back into an object and return.

### `InMemoryLoader<T> : ILoader<T>`
Loads a model from memory that was previously stored. This class should mainly
be used for testing various new UI features.

### `InMemorySaver<T> : ISaver<T>`
Saves a model to memory that can be later retrieved. This class should mainly
be used for testing various new UI features.

## Service Interfaces

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

## Transaction Interfaces

### `ILastTransactionRead<T>`
Interface that allows for the last x transactions to be returned from a specific
blockchain. The generic `T` must be of type `ITransaction`. Since `ITransation` is 
not limited by blockchain type and can be anything, any type of transactions
can be returned. This isn't limited to a single blockchain, which potentially allows
for a single implementation that can return transactions from many different blockchains.

### `ITransaction`
Interface representing the most basic form of transaction model in the system. This
is utilized so that we can enforce type checking in the code. Typically, only 1 will
be created per blockchain type.

### `ITransactionWrite`
Interface that sends coins from one account to another. The generic `T` must be
an implementation of type `ICoinCurrency`. We only want accounts from the same
blockchain to send coins to each other.

## Transaction Classes

### `TransactionSentResponse<T>`
A response object model providing information about the results of writing a transaction
on the blockchain. Will provide a transaction hash to be able to track the transaction.

## Wallet Interfaces

### `IAccount<T>`
Interface providing the bare minimum information to transfer
information between accounts. This is required so that
cross-blockchain calls are not possible. Each blockchain
implementation must have its own `IAccount` implementation.
The generic `T` must be an implementation of type `ICoinCurrency`.

### `IBalanceRead<T>`
Interface to retrieve the balance for a give account. The generic `T`
must be an implementation of type `ICoinCurrency`. This ensures only
the proper accounts retrieve balances from the proper blockchains.

### `ICoin<T>`
Interface that defines units of coins in a given blockchain. In the
case of Ethereum this could be EtherCoin or WeiCoin. The generic `T`
must be an implementation of type `ICoinCurrency` which ensures
we know what blockchain it belongs to.

### `ICoinCurrency`
Interface that defines a blockchain by its currency. Each blockchain
implementation will only have a single `ICoinCurrency`. This interface
is the cornerstone of all classes within the system.

NOTE: This interface could be re-defined to IBlockChainType and have
the same effect.

### `ISeedGenerator<T>`
Interface used to define a seed generator to be used when a mnemonic
word list is required (or other implementation). The generic `T` is 
used to define the return type. In most impementations this will be a 
string, but byte arrays could also be utilized in special cases.

### `IWallet<T>`
Interface providing basic wallet functionality. The generic `T`
must be an implementation of type `ICoinCurrency`. We only want
the wallet to produce a single type of account for a given
blockchain implementation. This ensures the wallet doesn't cause
issues during cross-blockchain transactions.

For most implementations, this will not reach out to any service
or external source.

## Wallet Classes

### `StaticInMemorySeedGenerator : ISeedGenerator<string>`
In instances where the user already knows their mnemonic phrase, this
class can be used to provide this static value to other classes that require
`ISeedGenerator<string>`

### `WalletAddressNotFoundException : Exception`
Exception thrown when an address is not found in the wallet.


