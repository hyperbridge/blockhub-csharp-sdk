using Hyperbridge.Wallet;
using Newtonsoft.Json;
using System;

namespace Hyperbridge.Services
{
    public class Transaction
    {
        [JsonConverter(typeof(ICoinCurrencyConverter))]
        public ICoinCurrency Currency { get; set; }

        public DateTime TimeStamp { get; set; }
        public string FromAddress { get; set; }
        public string ToAddress { get; set; }

        public decimal Amount { get; set; }
        public string Unit { get; set; }
    }
}
