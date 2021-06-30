using System.ComponentModel.DataAnnotations;

namespace VemVinner.Domain
{
    public class GameDTO
    {
        public int? Id { get; set; }

        [Required(ErrorMessage = "Du måste ange ett namn")]
        [MinLength(3, ErrorMessage = "Namnet är för kort")]
        [MaxLength(20, ErrorMessage = "Namnet är för långt")]
        public string Name { get; set; }

        [MaxLength(500, ErrorMessage = "Beskrivningen är för långt")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Du måste välja en profilbild")]
        public string ProfilePictureURL { get; set; }
    }
}
