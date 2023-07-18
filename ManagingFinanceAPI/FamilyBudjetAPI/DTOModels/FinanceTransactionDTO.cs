namespace FamilyBudjetAPI.DTOModels
{
    public class FinanceTransactionDTO
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public string Note { get; set; }
        public int CategoryId { get; set; }
    }
}