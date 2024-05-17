using NomsNoms.Entities;

namespace NomsNoms.DTOs
{
    public class UserPhotoDTO
    {
        public string Url { get; set; }
        public bool IsMain { get; set; } = false;
        public string PublicId { get; set; }
        public int AppUserId { get; set; }
    }
}
