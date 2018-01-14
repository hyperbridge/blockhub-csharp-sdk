# Blockhub.Ethereum Library
The Hyperbridge Ethereum library contains core Ethereum classes that
are true regardless of the implementation of the Ethereum work. This includes
coins and transaction information. This allows for cross-implementation
workloads when one implementation isn't enough and their may be needs
for load balancing between various clients or implementations.

# Library

### `Ether : ICoinCurrency`
The Ethereum implementation of ICoinCurrency. This is a critical class that should be used to create implementations
for Ethereum classes. There should only ever be one instance of this class. There is a static `Instance` property
that will provide this to the consumer.

### `EtherCoin : ICoin<Ether>`
The implementation of the Ether coin in the system. It has built in capabilities to be implicitly converted to
`WeiCoin`.

### `EthereumTransaction : ITransaction`
Implements the basic `ITransaction` that is needed for writing to the Ethereum blockchain. It also contains
all the other data that is specific to Ethereum.

### `ToEthereumAccount : IAccount<Ether>`
Since we will not typically know the private keys of accounts we are sending coins to, this class is used to 
avoid this need. However, if `PrivateKey` is attempted to be accessed, an `InvalidOperationException` is thrown.

### `WeiCoin : ICoin<Ether>`
The implementation of the base Ethereum unit of coin, Wei. It has built in capabilities to be implicitly converted to
`EtherCoin`.

```csharp
WeiCoin wei = new WeiCoin(5000000);
EtherCoin ether = (EtherCoin)wei;
// Ether = 0.000000000005 ETH
```
