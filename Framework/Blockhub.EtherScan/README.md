# Blockhub.EtherScan Library
The Hyperbridge EtherScan library is used to read recent transactions from
the Ethereum blockchain. Since this requires reading the whole blockchain
or storing it ourselves, we utilize EtherScan.io to do this for us
via their API. 

# Library
### `EtherScanLastTransactionRead : ILastTransactionRead<EthereumTransaction>`
EtherScan implementation used to read recent transactions from the Ethereum blockchain. Requires an API Key
to utilize. The URL to EtherScan is hardcoded (as of now) to simplify configuration for now, as it is unlikely
they will change this anytime soon.