namespace FamilyBudjetAPI.DTOModels
{
    public class BudgetStatisticsDataDTO
    {
        public decimal? TotalBalance { get; set; }
        public int? TransactionCount { get; set; }
        public int? IncomeSubcategoriesCount { get; set; }
        public int? ExpenseSubcategoriesCount { get; set; }
    }
}