namespace NomsNoms.Helpers
{
    public class UserParams : PaginationParams
    {
        public string OrderByCompletionTime { get; set; }
        public int CategoryId { get; set; }
    }
}
