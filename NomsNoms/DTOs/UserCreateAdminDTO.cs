using System.ComponentModel.DataAnnotations;

namespace NomsNoms.DTOs
{
    public class UserCreateAdminDTO
    {
        [Required]
        [RegularExpression(@"^[a-zA-Z\u00C0-\u1EF9 ]*$", ErrorMessage = "Fullname can only contain letters, spaces, and Vietnamese characters.")]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        public string KnownAs { get; set; }
        [Required]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Phone number must be 10 digits")]
        public string PhoneNumber { get; set; }
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [EmailAddress]
        public string Email { get; set; }
        //[Required]
        //[StringLength(100, ErrorMessage = "Introduction must be at least 1 character long and max is 100 characters", MinimumLength = 1)]
        public string Introduction { get; set; }
    }
}
