namespace NomsNoms.Helpers
{
    public class UserParams : PaginationParams
    {
        public string OrderByCompletionTime { get; set; }
        public string OrderByComplexity { get; set; }
        public int[] CategoryIds { get; set; }
        public string Search { get; set; }
    }
}
