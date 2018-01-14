using Blockhub.Wallet;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace Blockhub.Wallet
{
    public abstract class Wallet : ProfileObject
    {
        [JsonProperty]
        public string Secret { get; set; }
    }

    public class Wallet<T> : Wallet 
        where T : ITokenSource
    {
        [JsonProperty]
        public ICollection<Account<T>> Accounts { get; } = new Collection<Account<T>>();
    }
}
