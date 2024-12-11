using CoffeeCrazy.Interfaces;
using CoffeeCrazy.Models;

namespace CoffeeCrazy.Services
{
    public class ScheduledJobService : BackgroundService
    {
        private readonly ICRUDRepo<Job> _jobRepo;
        private readonly ICRUDRepo<Machine> _machineRepo;

        public ScheduledJobService(ICRUDRepo<Job> jobRepo, ICRUDRepo<Machine> machineRepo)
        {
            _jobRepo = jobRepo;
            _machineRepo = machineRepo;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var now = DateTime.Now;

                var machines = await _machineRepo.GetAllAsync();

                var jobs = await _jobRepo.GetAllAsync();

                foreach (var machine in machines)
                {
                    foreach (var job in jobs)
                    {
                        if (job.MachineId == machine.MachineId)
                        {
                            DateTime deadline;

                            if (job.FrequencyId == 3)
                            {
                                deadline = now.AddMonths(1); // Månedlig
                            }
                            else if (job.FrequencyId == 2)
                            {
                                deadline = now.AddDays(7); // Ugenligt
                            }
                            else
                            {
                                deadline = now.AddDays(1); // Daglig
                            }
                            if (now.Date > deadline)
                            {
                                var dailyJob = new Job
                                {
                                    Title = job.Title,
                                    Description = job.Description,
                                    DateCreated = now,
                                    Deadline = deadline,
                                    FrequencyId = job.FrequencyId,
                                    MachineId = machine.MachineId
                                };

                                await _jobRepo.CreateAsync(dailyJob);
                            }
                        }
                    }
                }
                await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);
            }
        }

    }
}
