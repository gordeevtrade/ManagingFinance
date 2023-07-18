namespace FamilyBudjetAPI.Sevices.Interface
{
    public interface IFinanceReportService
    {
        //DailyReport GetDailyReport(DateTime date);

        PeriodReport GetPeriodReport(DateTime startDate, DateTime endDate);
    }
}