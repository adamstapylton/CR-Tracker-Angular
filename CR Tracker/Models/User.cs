using System.ComponentModel.DataAnnotations;

namespace CR_Tracker.Models
{
    public class User
    {
        public int UserId { get; set; }
        [Required]
        public string Initials { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string Surname { get; set; }

        public string Email { get; set; }

    }
}
