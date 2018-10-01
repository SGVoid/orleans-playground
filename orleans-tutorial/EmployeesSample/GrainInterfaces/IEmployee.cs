using System.Threading.Tasks;
using Orleans;

namespace GrainInterfaces
{
    public interface IEmployee : IGrainWithGuidKey
    {
        Task<int> GetLevel();
        Task Promote(int newLevel);

        Task<IManager> GetManager();
        Task SetManager(IManager manager);
        Task Greeting(IEmployee from, string message);
    }
}