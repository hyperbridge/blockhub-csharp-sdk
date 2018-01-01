# Hyperbridge.Core Library
The Hyperbridge Core library is the core of the Hyperbridge
ecosystem of Blockchain libraries. It is meant to have blockchain
agnostic classes and interfaces so that implementations can be
replaced as needed. It is also written as a library in order
to separate it from the needs of Unity. This library should maintain
separation from any UI framework so that it can be pushed out to
other UIs in the future (Unity, Web Application, Xamarin, etc).

# Library
## Wallet
The wallet namespace contains the primary interfaces and classes
needed to create wallets and generate accounts.

### `IAccount`
Interface providing the bare minimum information to transfer
information between accounts. This is required so that
cross-blockchain calls are not possible. Each blockchain
implementation must have its own `IAccount` implementation.

### `IWallet<T>`
Interface providing basic wallet functionality. The generic `T`
must be an implementation of type `IAccount`. We only want
the wallet to produce a single type of account for a given
blockchain implementation. This ensures the wallet doesn't cause
issues during cross-blockchain transactions.

