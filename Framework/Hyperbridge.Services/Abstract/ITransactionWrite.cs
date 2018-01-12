using System.Threading.Tasks;

namespace Hyperbridge.Services.Abstract
{
    public interface ITransactionWrite
    {
        Task<string> SendTransaction(SendTransaction transaction);
    }
}
