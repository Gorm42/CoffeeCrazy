using CoffeeCrazy.Interfaces;
using CoffeeCrazy.Models;
using CoffeeCrazy.Models.Enums;
using CoffeeCrazy.Repos;
using CoffeeCrazy.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CoffeeCrazy.Pages.Jobs
{
    public class IndexModel : PageModel
    {
        private readonly ICRUDRepo<Job> _jobRepo;
        private readonly IJobService _jobService;
        private readonly IUserRepo _userRepo; //hvis jeg ikke får brugt den her skal den slettes.
        IAccessService _accessService;

        public Dictionary<string, List<Job>> GroupedJobs { get; set; } //property til at samle alle vores jobs under de titler de skal fremvises som under html-siden.

        public IndexModel(IJobService jobService)
        {
            _jobService = jobService;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            if (!_accessService.IsUserLoggedIn(HttpContext))
                       return RedirectToPage("/Login/Login");

            try
            {
                List<Job> allJobs = await _jobRepo.GetAllAsync();

                GroupedJobs = _jobService.GroupJobsByTitle(allJobs);
                
                return Page();
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Unhandled error: {ex.Message}");
                return RedirectToPage("/Error", new { message = "Noget gik galt under hentning af brugere. Kontakt administrator." });
            }

         
        }


    }
}

