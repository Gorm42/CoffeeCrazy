using CoffeeCrazy.Interfaces;
using CoffeeCrazy.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CoffeeCrazy.Pages.Machines
{
    public class EditModel : PageModel
    {
        private readonly ICRUDRepo<Machine> _machineRepo;
        private readonly IImageService _imageService;

        public EditModel(ICRUDRepo<Machine> machineRepo, IImageService imageService)
        {
            _machineRepo = machineRepo;
            _imageService = imageService;
        }

        [BindProperty]
        public Machine MachinetoUpdate { get; set; }
        public IFormFile PictureToUpload { get; set; }

    public async Task<IActionResult> OnGetAsync(int id)
        {
            // Henter maskinen baseret på ID
            MachinetoUpdate = await _machineRepo.GetByIdAsync(id);
            if (MachinetoUpdate == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            if (PictureToUpload != null)
            {
                MachinetoUpdate.MachineImage = _imageService.ConvertImageToByteArray(PictureToUpload);

            }
            ModelState.Remove("PictureToUpload");
            if (!ModelState.IsValid)
            {
                return Page();
            }
           
            MachinetoUpdate.MachineId = id;
            // Opdaterer maskinen i databasen
            await _machineRepo.UpdateAsync(MachinetoUpdate);

            // Redirecter til Index (oversigten)
            return RedirectToPage("./Index");
        }
    }
}
