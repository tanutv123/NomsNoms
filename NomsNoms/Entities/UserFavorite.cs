namespace NomsNoms.Entities
{
    public class UserFavorite
    {
        public int CategoryId { get; set; }
        public int AppUserId { get; set; }
        public Category Category { get; set; }
        public AppUser AppUser { get; set; }

    }
}
