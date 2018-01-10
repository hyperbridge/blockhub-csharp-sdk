using System;
using Hyperbridge.Wallet;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Hyperbridge.Services
{
    public class ICoinCurrencyConverter : JsonConverter
    {
        public override bool CanRead => true;
        public override bool CanWrite => true;
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(ICoinCurrency);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            JObject jsonObject = null;
            try
            {
                jsonObject = JObject.Load(reader);
            } catch
            {
                return null;
            }

            var currency = default(ICoinCurrency);
            switch (jsonObject["BlockchainType"].Value<string>())
            {
                case "ETHER":
                    currency = Ethereum.Ether.Instance;
                    break;
                default:
                    throw new InvalidOperationException("Invalid Blockchain type provided.");
            }

            serializer.Populate(jsonObject.CreateReader(), currency);
            return currency;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            JToken token = JToken.FromObject(value);

            if (typeof(Ethereum.Ether) == value.GetType())
            {
                JObject o = (JObject)token;
                o.AddFirst(new JProperty("BlockchainType", "ETHER"));
                o.WriteTo(writer);
            } else
            {
                throw new InvalidOperationException("Invalid Currency provided.");
            }
        }
    }
}
