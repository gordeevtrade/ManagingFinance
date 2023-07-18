namespace Domain.BL.Models
{
    public class TransactionWithCategoryName
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public string Note { get; set; }
        public string CategoryName { get; set; }
        public int CategoryId { get; set; }
    }
}