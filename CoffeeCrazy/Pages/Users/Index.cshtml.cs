using CoffeeCrazy.Interfaces;
using CoffeeCrazy.Models;
using CoffeeCrazy.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CoffeeCrazy.Pages.Users
{
    public class IndexModel : PageModel
    {
        public List<User> Users { get; private set; }

        private readonly IUserRepo _UserRepo;
        private readonly IAccessService _accessService;
        
        public IndexModel(IUserRepo userCrudRepository, IAccessService accessService)
        {
            _UserRepo = userCrudRepository;
            _accessService = accessService;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            if (!_accessService.IsUserLoggedIn(HttpContext))
                return RedirectToPage("/Login/Login");
            try
            {
                Users = await _UserRepo.GetAllAsync();

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
