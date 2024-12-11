using CoffeeCrazy.Interfaces;
using CoffeeCrazy.Models;

namespace CoffeeCrazy.Services
{
    public class JobService : IJobService
    {
        public Dictionary<string, List<Job>> GroupJobsByTitle(List<Job> jobs)
        {
            if( jobs == null )
            {
                Console.WriteLine($"{jobs}-listen indeholder ikke noget");
                return null;
            }
            else
            {
                return jobs //benytter sig at LINQ (Language Integrated Query), til at sortere listen.
                .GroupBy(job => job.Title)
                .ToDictionary(group => group.Key, group => group.ToList());
            }
            
        }

    }
}
