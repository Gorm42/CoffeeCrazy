using CoffeeCrazy.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Dynamic;

namespace CoffeeCrazy.Pages.Login.Password
{
    public class ForgotPasswordModel : PageModel
    {
        private readonly IEmailService _emailService;

        public ForgotPasswordModel( IEmailService emailService)
        {
            _emailService = emailService;
        }

        [BindProperty]
        public string Email { get; set; }

        public string Message { get; set; }

        public IActionResult OnGet()
        {
            var user = HttpContext.Session.GetInt32("UserId");
            if (user == null)
            {
                return RedirectToPage("/Machines/Index"); // Skal sende folk til hvad vi synes er main siden n�r man er logget ind :)
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                ModelState.AddModelError("","Venligst intast email.");
                return Page(); 
            }   
                
               bool emailSent = await _emailService.GenerateTokenAndSendResetEmail(email);

            if (emailSent)
            {
                Message = "En mail med nyt password er sendt til dig.";
                Thread.Sleep(5000);
                return RedirectToPage("/Login/Password/ResetPassword");
            }
            else
            {
                Message = "Der er sket en fejl Token bliv ikke sendt pr�v igen";
                Thread.Sleep(5000);

                return Page();
            }                  
        }
    }
}
