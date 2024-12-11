using CoffeeCrazy.Models;

namespace CoffeeCrazy.Interfaces
{
    public interface IJobService
    {
        Dictionary<string, List<Job>> GroupJobsByTitle(List<Job> jobs);
    }
}
