using CoffeeCrazy.Interfaces;
using CoffeeCrazy.Models;
using CoffeeCrazy.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CoffeeCrazy.Pages.Users
{
    public class CreateModel : PageModel
    {
        private readonly IUserRepo _userRepo;
        private readonly IAccessService _accessService;
        private IImageService _imageService;

        public IFormFile PictureToBeUploaded { get; set; }

        [BindProperty]
        public User NewUser { get; set; } = new User();

        public CreateModel(IUserRepo userRepo, IImageService imageService, IAccessService accessService)
        {
            _userRepo = userRepo;
            _imageService = imageService;
            _accessService = accessService;
        }

        public IActionResult OnGet()
        {
            if (!_accessService.IsUserLoggedIn(HttpContext))
                return RedirectToPage("/Login/Login");
            return Page();
        }
        public async Task<IActionResult> OnPost()
        {


            //ModelState.Remove("PasswordSalt");
            //if (!ModelState.IsValid)
            //{
            //    return Page();
            //}

            NewUser.UserImage = _imageService.ConvertImageToByteArray(PictureToBeUploaded);
            
            await _userRepo.CreateAsync(NewUser);
            return RedirectToPage("Index");
        }
    }
}
