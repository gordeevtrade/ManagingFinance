using Domain.BL.Models;

namespace Domain.ServicesInterface
{
    public interface IStatisticstService
    {
        BudgetStatisticsData GetStatisticsData();

        //public CombinedData GetCombinedData();

        //     decimal GetTotalBalance();
    }
}