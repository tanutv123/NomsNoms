using NomsNoms.Entities;

namespace NomsNoms.DTOs
{
    public class UserFollowDTO
    {
        public int SourceUserId { get; set; }
        public int TargetUserId { get; set; }
    }
}
