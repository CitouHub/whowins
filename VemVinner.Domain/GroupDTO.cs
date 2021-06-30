using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using VemVinner.Domain.Account;
using VemVinner.Domain.Validation;

namespace VemVinner.Domain
{
    public class GroupDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Du måste ange ett namn")]
        [MinLength(3, ErrorMessage = "Namnet är för kort")]
        [MaxLength(20, ErrorMessage = "Namnet är för långt")]
        public string Name { get; set; }

        [MaxLength(500, ErrorMessage = "Beskrivningen är för långt")]
        public string Description { get; set; }

        [ListValidator(MinimumCount = 1, ErrorMessage = "Du måste lägga till minst en användare till gruppen")]
        public List<UserDTO> Users { get; set; } = new List<UserDTO>();

        [ListValidator(MinimumCount = 1, ErrorMessage = "Du måste lägga till minst ett spel till gruppen")]
        public List<GameDTO> Games { get; set; } = new List<GameDTO>();

        public List<UserDTO> DeactivateUsers { get; set; } = new List<UserDTO>();

        public List<GameDTO> DeactivateGames { get; set; } = new List<GameDTO>();

        public bool InvitationAccepted { get; set; }

        public int UsersInGroup { get; set; }
    }
}
