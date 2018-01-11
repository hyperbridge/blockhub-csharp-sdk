using System.Threading.Tasks;

namespace Hyperbridge.Services.Abstract
{
    public interface ITransactionWrite
    {
        Task SendTransaction(SendTransaction transaction);
    }
}
