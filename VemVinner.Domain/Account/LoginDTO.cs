using System.ComponentModel.DataAnnotations;

namespace VemVinner.Domain.Account
{
    public class LoginDTO
    {
        [Required(ErrorMessage = "Du måste ange användarnamn")]
        [MaxLength(20, ErrorMessage = "Användarnamnet är för långt")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Du måste ange lösenord")]
        public string Password { get; set; }
    }
}
