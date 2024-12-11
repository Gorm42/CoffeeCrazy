using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CoffeeCrazy.Pages.Jobs
{
    public class DeadDarlingModel : PageModel
    {
        //public class DeadDarlingIndex
        //{
        //
        //    private readonly ICRUDRepo<Job> _jobRepo;
        //    private readonly IUserRepo _userRepo;
        //    private readonly ICRUDRepo<Machine> _machineRepo;
        //    private readonly IAccessService _accessService;
        //
        //    public List<Frequency> Frequencies = new List<Frequency>(); //hvad bruges denne til?
        //
        //    public List<Job> Jobs { get; set; }
        //    public List<User> Users { get; set; }
        //    public List<Machine> Machines { get; set; }
        //
        //    private List<Job> OlderThan6MonthsJobs { get; set; }
        //
        //
        //
        //    public IndexModel(ICRUDRepo<Job> jobRepo, IUserRepo userRepo, ICRUDRepo<Machine> machineRepo, IAccessService accessService /*, IJobService jobservice*/)
        //    {
        //        _jobRepo = jobRepo;
        //        _userRepo = userRepo;
        //        _machineRepo = machineRepo;
        //        _accessService = accessService;
        //        //_jobService = jobservice;
        //    }
        //
        //
        //    public async Task<IActionResult> OnGetAsync()
        //    {
        //        if (!_accessService.IsUserLoggedIn(HttpContext))
        //            return RedirectToPage("/Login/Login");
        //
        //        //Finder jobs som er ældre end 6 måneder
        //        OlderThan6MonthsJobs = _jobRepo.GetAllAsync().Result.FindAll(parameter => parameter.Deadline < DateTime.UtcNow.AddMonths(-6));
        //
        //        //Sletter jobs som er ældre end 6 måneder
        //        foreach (Job oldJob in OlderThan6MonthsJobs)
        //        {
        //            await _jobRepo.DeleteAsync(oldJob);
        //        }
        //
        //        Jobs = await _jobRepo.GetAllAsync();
        //        Users = await _userRepo.GetAllAsync();  //Hvorfor har vi users her?
        //        Machines = await _machineRepo.GetAllAsync();
        //
        //        foreach (var item in Jobs) //hvad gør den her?
        //        {
        //            Frequencies.Add((Frequency)item.FrequencyId);
        //        }
        //        return Page();
        //
        //    }
        //}
        public void OnGet()
        {
        }
    }
}
