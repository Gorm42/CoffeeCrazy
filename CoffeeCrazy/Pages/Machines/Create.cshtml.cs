using CoffeeCrazy.Interfaces;
using CoffeeCrazy.Models;
using CoffeeCrazy.Models.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CoffeeCrazy.Pages.Machines
{
    public class CreateModel : PageModel
    {
        private readonly ICRUDRepo<Machine> _machineRepo;

        public IFormFile PictureToBeUploaded { get; set; }

        [BindProperty]
        public Machine Machine { get; set; }

        public CreateModel(ICRUDRepo<Machine> machineRepo)
        {
            _machineRepo = machineRepo;
        }

        public void Onget()
        {

        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            
            await _machineRepo.CreateAsync(Machine);
            return RedirectToPage("./Index"); // Redirect to a list page
        }
    }
}