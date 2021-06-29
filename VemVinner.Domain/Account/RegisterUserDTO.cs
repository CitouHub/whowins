using System.ComponentModel.DataAnnotations;
using VemVinner.Domain.Validation;

namespace VemVinner.Domain.Account
{
    public class RegisterUserDTO
    {
        [Required(ErrorMessage = "Du måste ange användarnamn")]
        [MaxLength(20, ErrorMessage = "Användarnamnet är för långt")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Du måste ange lösenord")]
        public string Password { get; set; }
    }
}
