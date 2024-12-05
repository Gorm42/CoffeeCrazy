using CoffeeCrazy.Interfaces;
using CoffeeCrazy.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CoffeeCrazy.Pages.Machines
{
    public class DeleteModel : PageModel
    {
        private readonly ICRUDRepo<Machine> _machineRepo;

        public List<Machine> Machines { get; set; } = new();

        [BindProperty]
        public int? SelectedMachineId { get; set; } // Holds the ID of the selected machine.
        public DeleteModel(ICRUDRepo<Machine> machineRepo)
        {
            _machineRepo = machineRepo;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            Machines = await _machineRepo.GetAllAsync();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!SelectedMachineId.HasValue)
            {
                ModelState.AddModelError(nameof(SelectedMachineId), "Please select a machine to delete.");
                Machines = await _machineRepo.GetAllAsync(); // Reload machines for redisplay.
                return Page();
            }

            var machine = await _machineRepo.GetByIdAsync(SelectedMachineId.Value);

            if (machine == null)
            {
                return NotFound();
            }

            await _machineRepo.DeleteAsync(machine);

            return RedirectToPage("./Index"); // Redirect to a list or main page after deletion.
        }
    }
}