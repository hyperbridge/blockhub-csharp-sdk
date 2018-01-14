using System.Threading.Tasks;

namespace Blockhub
{
    public interface IProfileManager
    {
        Task<Profile> CreateProfile(string name);
    }
}
