using NomsNoms.Entities;

namespace NomsNoms.DTOs
{
    public class TransactionDTO
    {
        public int Id { get; set; }
        public string? ReportName { get; set; }
        public DateTime CreateDate { get; set; }
        public decimal TotalPrice { get; set; }
        public int SenderId { get; set; }
    }
}
