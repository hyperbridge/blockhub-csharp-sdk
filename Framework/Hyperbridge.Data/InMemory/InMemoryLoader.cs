using System;
using System.Collections.Generic;

namespace Hyperbridge.Data
{
    public class InMemoryLoader<T> : ILoader<T>
    {
        public T Load(string uri)
        {
            Uri parsedUri = new Uri(uri);
            if (!parsedUri.Scheme.Equals("mem", StringComparison.OrdinalIgnoreCase))
                throw new InvalidOperationException("Invalid uri scheme for this loader.");

            var foundObject = InMemoryDataStore.Store[parsedUri.Host];
            if (foundObject == null) throw new KeyNotFoundException("Object not found in memory.");

            // TODO: Add new checks to throw more appropriate exceptions if the type is incorrect.
            return (T)foundObject;
        }
    }
}
