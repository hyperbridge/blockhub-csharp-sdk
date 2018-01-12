# Hyperbridge.StructureMap Library
The Hyperbridge StructureMap library is an Inversion of Control (IOC) implementation
to ease the adoption in Unity and other UI systems. Since Unity 3D does not have
easy ways to inject dependencies into classes where resolution is simple, this
allows for only a few classes to have control over resolving depedencies in the system.

It has been separated out in order to allow another implementation in the future, if necessary.

# Implementing a new Blockchain
In order to implement a new blockchain, a number of things should happen:

  1. Implement all transaction and coin interfaces.
  2. Decide what implementations are required by the UI and create a StructureMap registry.
  3. Implement `IDependencyRoot` that will resolve the depedencies from 1 or more registries for the new blockchain.
  4. Add the newly implemented `ICoinCurrency` to the `ApplicationRegistry`

After doing this, the new coin currency should be available to the UI application.

# Assemblies
All assemblies needed for Unity are found in the output of this project. This means that this
project is considered the most volatile of all the libraries and will change the most.

# Library

### `ApplicationDependencyRoot : IDependencyRoot`
Provides an instance of IDependencyRoot this is blockchain indepedent. This is used
for application-level depedencies (i.e. Profile Saver/Loader, etc.). Configuration values
are required as an input to the constructor.

This root should also contain the available coins. By calling `Resolve<IEnumerable<ICoinCurrency>>` 
the consumer can retrieve "dynamically" what blockchains have been implemented.

### `EthereumDepedencyRoot : IDependencyRoot`
Provides an instance of the Ethereum blockchain classes to the consumer.
