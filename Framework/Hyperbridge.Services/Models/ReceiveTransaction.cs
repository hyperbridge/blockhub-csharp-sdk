using Hyperbridge.Wallet;
using Newtonsoft.Json;
using System;

namespace Hyperbridge.Services
{
    public class ReceiveTransaction
    {
        [JsonConverter(typeof(ICoinCurrencyConverter))]
        public ICoinCurrency Currency { get; set; }

        public DateTime TimeStamp { get; set; }
        public string FromAddress { get; set; }
        public Account ToAddress { get; set; }

        public decimal Amount { get; set; }
        public string Unit { get; set; }

        public Transaction ToTransaction()
        {
            return new Transaction
            {
                Amount = Amount,
                Currency = Currency,
                FromAddress = FromAddress,
                ToAddress = ToAddress.Address,
                TimeStamp = TimeStamp,
                Unit = Unit
            };
        }
    }
}
