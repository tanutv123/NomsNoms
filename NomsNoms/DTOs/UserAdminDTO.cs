using NomsNoms.Entities;
using System.ComponentModel.DataAnnotations;

namespace NomsNoms.DTOs
{
    public class UserAdminDTO
    {
        public int Id { get; set; }
        [Required]
        [RegularExpression(@"^[a-zA-Z\u00C0-\u1EF9 ]*$", ErrorMessage = "Fullname can only contain letters, spaces, and Vietnamese characters.")]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 5)]
        public string KnownAs { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Introduction { get; set; }
        [Required]
        public string Role { get; set; }
        [Required]
        public byte Status { get; set; }
    }
}
