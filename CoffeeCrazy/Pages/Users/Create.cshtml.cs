using CoffeeCrazy.Interfaces;
using CoffeeCrazy.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace CoffeeCrazy.Pages.Users
{
    public class CreateModel : PageModel
    {
        //Overvej at tilføje ILogger her til debugging.
        private readonly ICRUDRepo<User> _userRepo;
        private readonly IAccessService _accessService;
        private IImageService _imageService;
        private readonly IValidationService _validationService;

        [BindProperty]
        public User NewUser { get; set; } = new User();

        public string ResultMessage { get; set; }

        public CreateModel(ICRUDRepo<User> userRepo, IImageService imageService, IAccessService accessService, IValidationService validationService)
        {
            _userRepo = userRepo;
            _imageService = imageService;
            _accessService = accessService;
            _validationService = validationService;
        }

        public IActionResult OnGet()
        {
            return Page();
        }
        public async Task<IActionResult> OnPost()
        {
            if (!_accessService.IsUserLoggedIn(HttpContext))
            {
                return RedirectToPage("/Login/Login");
            }
            if (!_accessService.IsAdmin(HttpContext))
            {
                return RedirectToPage("/Errors/AccessDenied");
            }

            //Data validation and page redirect here:            
            //if (!ModelState.IsValid)
            //{
            //    return Page();
            //}



            bool emailExists = await _validationService.CompareEmailsAsync(NewUser.Email);
            if (emailExists)
            {
                TempData["ErrorMessage"] = "Email findes allerede i databasen. Slet brugeren eller bed dem om at nulstille deres password.";
                return RedirectToPage("Create");
                
            }

            await _userRepo.CreateAsync(NewUser);
            TempData["SuccessMessage"] = "Bruger oprettet!";
            return RedirectToPage("Index");


        }
    }
}
