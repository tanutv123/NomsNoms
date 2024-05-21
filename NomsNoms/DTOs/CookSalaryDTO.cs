namespace NomsNoms.DTOs
{
    public class CookSalaryDTO
    {
        public string CookName { get; set; }
        public string CookEmail { get; set; }
        public string CookPhoneNumber { get; set; }
        public string SalaryReportName { get; set; }
        public decimal TotalMoneyReceived { get; set; }
        public decimal MoneyFromSubscriptions { get; set; }
    }
}
