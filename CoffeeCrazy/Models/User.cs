using System.ComponentModel.DataAnnotations;
using CoffeeCrazy.Models.Enums;

namespace CoffeeCrazy.Models
{
    public class User
    {        
        public int UserId { get; set; }

        //[Required(ErrorMessage = "Brugerens fornavn skal indtastes.")]
        public string FirstName { get; set; }

        //[Required(ErrorMessage = "Brugerens efternavn skal indtastes.")]
        public string LastName { get; set; }

        //[Required(ErrorMessage = "Brugerens email indtastes.")]
        public string Email { get; set; }

        //[Required(ErrorMessage = "Brugeren skal have et kodeord.")]
        //[StringLength(100, MinimumLength = 6; ErrorMessage = "Kodeordet skal være minimum seks bogstaver/tal)]
        public string Password { get; set; }

        public string PasswordSalt { get; set; }

        public Role Role { get; set; }

        public Campus Campus { get; set; }

        //[Required(ErrorMessage = "Du skal oploade et profilbillede.")]
        public IFormFile? UserImageFile { get; set; }
    }
}
    