using FamilyBudjetAPI;

namespace Budget.BuisnessLogic.Models
{
    public class Report
    {
        public decimal TotalIncome { get; set; }
        public decimal TotalExpenses { get; set; }
        public List<FinanceTransaction> Transactions { get; set; }
    }
}