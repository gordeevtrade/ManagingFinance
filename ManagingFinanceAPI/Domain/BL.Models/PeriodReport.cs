using Budget.BuisnessLogic.Models;
using Domain.BL.Models;

namespace FamilyBudjetAPI
{
    public class PeriodReport : Report
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public List<TransactionWithCategoryName> Transactions { get; set; }
    }
}