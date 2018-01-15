using Blockhub.Ethereum;
using Blockhub.Transaction;
using NBitcoin.BouncyCastle.Math;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Blockhub.EtherScan
{
    public class EtherScanLastTransactionRead : ILastTransactionRead<EthereumTransaction>, IDisposable
    {
        // FUTURE: There may be a need for internal transactions (or transactions created by contracts)
        // reference: https://ethereum.stackexchange.com/questions/3417/how-to-get-contract-internal-transactions

        // reference: https://etherscan.io/apis#accounts
        private const string API_URI_FORMAT = "http://api.etherscan.io/api?module=account&action=txlist&address={0}&sort=desc&page={1}&offset={2}&apikey={3}";
        private readonly DateTime EPOCH = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        private string ApiKey { get; }
        private HttpClient Client { get; }

        public EtherScanLastTransactionRead(string apiKey)
        {
            ApiKey = apiKey ?? throw new ArgumentNullException(nameof(apiKey));
            Client = new HttpClient();
        }

        // reference: https://msdn.microsoft.com/en-us/library/system.threading.cancellationtoken.none.aspx?f=255&MSPPError=-2147217396
        public async Task<IEnumerable<EthereumTransaction>> GetLastTransactions(string address, int startPage = 1, int limit = 100, CancellationToken cancelToken = default(CancellationToken))
        {
            if (string.IsNullOrWhiteSpace(address)) throw new ArgumentNullException(nameof(address));
            
            // This allows for a 40 character raw address to 
            // be sent in and it will properly convert it as necessary
            if (address.Length == 40) address = "0x" + address;

            if (address.Length != 42) throw new ArgumentOutOfRangeException(nameof(address), "Invalid Address.");

            if (startPage < 1) throw new ArgumentOutOfRangeException(nameof(startPage), "Start Page must be greater than 0.");
            if (limit < 1) throw new ArgumentOutOfRangeException(nameof(limit), "Limit must be greater than 0.");

            string uri = string.Format(API_URI_FORMAT, address, startPage, limit, ApiKey);
            HttpResponseMessage response = await Client.GetAsync(uri, cancelToken);

            // TODO: This may need to be taken a step further to allow for retries in the future.
            // TODO: Create a better exception class to handle this
            if (!response.IsSuccessStatusCode) throw new Exception("Failed to retrieve transactions from the service.");
            var jsonResult = await response.Content.ReadAsStringAsync();

            var rawTransactions = JsonConvert.DeserializeObject<EtherScanTransactionResponse>(jsonResult, new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            });

            return rawTransactions.Result.Select(x => new EthereumTransaction
            {
                BlockHash = x.BlockHash,
                BlockNumber = x.BlockNumber,
                Coin = new Wei(x.Value),
                ContractAddress = x.ContractAddress,
                CumulativeGasUsed = x.CumulativeGasUsed,
                FromAddress = x.From,
                Gas = x.Gas,
                GasPrice = x.GasPrice,
                GasUsed = x.GasUsed,
                Input = x.Input,
                NumberConfirmations = x.Confirmations,
                ToAddress = x.To,

                // ASSUMPTION: There is no documentation, but the assumption this Hash is the Transaction Hash
                TransactionHash = x.Hash,
                TransactionIndex = x.TransactionIndex,

                // Timestamp is POSIX Timestamp (# Seconds since Unix Epoch 1970-01-01 UTC)
                // ASSUMPTION: We are making an assumption that EtherScan returns it the same as Ethereum RPC JSON
                // reference: https://github.com/ethereum/wiki/wiki/JSON-RPC
                // reference: https://forum.ethereum.org/discussion/14634/need-help-understanding-block-timestamp-and-how-time-works-in-the-blockchain
                TransactionTime = EPOCH.AddSeconds(x.TimeStamp) 
            }).ToArray();
        }

        public void Dispose()
        {
            Client?.Dispose();
        }

        private class EtherScanTransactionResponse
        {
            public int Status { get; set; }
            public string Message { get; set; }
            public EtherScanTransaction[] Result { get; set; }
        }

        private class EtherScanTransaction
        {
            public ulong BlockNumber { get; set; }
            public ulong TimeStamp { get; set; }
            public string Hash { get; set; }
            public ulong Nonce { get; set; }
            public string BlockHash { get; set; }
            public ulong TransactionIndex { get; set; }
            public string From { get; set; }
            public string To { get; set; }
            public string Value { get; set; }
            public ulong Gas { get; set; }
            public ulong GasPrice { get; set; }
            public ulong IsError { get; set; }

            [JsonProperty("txreceipt_status")]
            public string TxReceiptStatus { get; set; }
            public string Input { get; set; }
            public string ContractAddress { get; set; }
            public ulong CumulativeGasUsed { get; set; }
            public ulong GasUsed { get; set; }
            public ulong Confirmations { get; set; }
        }
    }
}
