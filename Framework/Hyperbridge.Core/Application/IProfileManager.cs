using System.Threading.Tasks;

namespace Blockhub.Services
{
    public interface IProfileManager
    {
        Task<Profile> CreateProfile(string name);
    }
}
