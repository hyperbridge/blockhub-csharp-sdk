using System.Threading.Tasks;

namespace Blockhub.Data
{
    public interface ISaver<T>
    {
        /// <summary>
        /// Persists the model to storage
        /// </summary>
        /// <param name="model">The model to persist.</param>
        /// <returns>The uri to the resource</returns>
        Task<string> Save(T model);
    }
}
