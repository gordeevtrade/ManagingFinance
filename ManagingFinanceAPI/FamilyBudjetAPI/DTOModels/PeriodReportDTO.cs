using ManagingFinanceAPI.DTOModels;

namespace FamilyBudjetAPI
{
    public class PeriodReportDTO
    {
        public decimal TotalIncome { get; set; }
        public decimal TotalExpenses { get; set; }
        public List<TransactionWithCategoryNameDTO> Transactions { get; set; }
    }
}