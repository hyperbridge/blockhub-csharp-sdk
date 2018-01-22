using Blockhub.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blockhub.Tests
{
    class EmulateSave<T> : ISave<T>
    {
        public bool SaveCalled { get; private set; } = false;

        public Task<string> Save(T model)
        {
            SaveCalled = true;
            return Task.FromResult(model?.ToString());
        }
    }
}
