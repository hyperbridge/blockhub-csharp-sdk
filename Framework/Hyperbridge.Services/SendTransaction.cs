using Hyperbridge.Wallet;
using Newtonsoft.Json;
using System;

namespace Hyperbridge.Services
{
    public class SendTransaction
    {
        [JsonConverter(typeof(ICoinCurrencyConverter))]
        public ICoinCurrency Currency { get; set; }

        public DateTime TimeStamp { get; set; }
        public Account FromAddress { get; set; }
        public string ToAddress { get; set; }

        public decimal Amount { get; set; }
        public string Unit { get; set; }

        public Transaction ToTransaction()
        {
            return new Transaction
            {
                Amount = Amount,
                Currency = Currency,
                FromAddress = FromAddress.Address,
                ToAddress = ToAddress,
                TimeStamp = TimeStamp,
                Unit = Unit
            };
        }
    }
}
