namespace NomsNoms.Entities
{
    public class Question
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public int Spiciness { get; set; }
        public int Saltiness { get; set; }
        public int Sweetness { get; set; }
        public int Sauce { get; set; }
    }
}
