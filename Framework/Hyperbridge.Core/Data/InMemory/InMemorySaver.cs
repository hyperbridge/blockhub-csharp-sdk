using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blockhub.Data
{
    public class InMemorySaver<T> : ISaver<T>
    {
        public string Save(T model)
        {
            var uniqueId = Guid.NewGuid().ToString();
            InMemoryDataStore.Store.Add(uniqueId, model);
            return $"mem://{uniqueId}";
        }
    }
}
