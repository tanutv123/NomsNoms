using System.ComponentModel.DataAnnotations;

namespace NomsNoms.DTOs
{
    public class UserProfileDTO
    {
        [Required]
        public string KnownAs { get; set; }
        public string Introduction { get; set; }
        [Required]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Phone number must be 10 digits")]
        public string PhoneNumber { get; set; }

    }
}
