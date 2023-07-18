namespace Domain.BL.Models
{
    public class BudgetStatisticsData
    {
        public decimal? TotalBalance { get; set; }
        public int? TransactionCount { get; set; }
        public int? IncomeSubcategoriesCount { get; set; }
        public int? ExpenseSubcategoriesCount { get; set; }
    }
}