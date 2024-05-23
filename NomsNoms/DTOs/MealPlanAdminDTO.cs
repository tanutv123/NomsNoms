namespace NomsNoms.DTOs
{
    public class MealPlanAdminDTO
    {
        public int Id { get; set; }
        public int Duration { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public byte Status { get; set; }
    }
}
