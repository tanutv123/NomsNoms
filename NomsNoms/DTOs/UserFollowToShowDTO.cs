namespace NomsNoms.DTOs
{
    public class UserFollowToShowDTO
    {
        public int AppUserId { get; set; }
        public int FollowerCount { get; set; }
        public string Name { get; set; }
        public string KnownAs { get; set; }
        //public string ImageUrl { get; set; }
    }
}
