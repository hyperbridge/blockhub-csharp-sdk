using System;
using System.Collections.Generic;

namespace Blockhub.Data
{
    /// <summary>
    /// Used strictly for in-memory storage of data
    /// </summary>
    internal static class InMemoryDataStore
    {
        public static Dictionary<string, object> Store { get; } = new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase);
    }
}
