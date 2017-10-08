namespace WorkMate.DTOs
{
    public class PaycheckDTO
    {
        public int ID { get; set; }
        public string PayDate { get; set; }
        public string JobName { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public decimal NetPay { get; set; }
    }
}