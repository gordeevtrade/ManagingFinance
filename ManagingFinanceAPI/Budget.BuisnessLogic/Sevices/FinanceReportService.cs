using Budget.DAL.Repositories.Interfaces;
using Domain.BL.Models;
using FamilyBudjetAPI.Sevices.Interface;

namespace FamilyBudjetAPI.Sevices
{
    public class FinanceReportService : IFinanceReportService
    {
        private IUnitOfWOrk _unitOfWOrk;

        public FinanceReportService(IUnitOfWOrk unitOfWOrk)
        {
            _unitOfWOrk = unitOfWOrk;
        }

        public PeriodReport GetPeriodReport(DateTime startDate, DateTime endDate)
        {
            var transactions = GetTransactionsWithCategoryNames(startDate, endDate).ToList();
            if (transactions.Count == 0)
            {
                throw new Exception("Date Period not found");
            }

            decimal totalIncome = transactions
                .Where(t => t.Amount > 0)
                .Sum(t => t.Amount);

            decimal totalExpenses = transactions
                .Where(t => t.Amount < 0)
                .Sum(t => t.Amount);

            var periodReport = new PeriodReport
            {
                StartDate = startDate,
                EndDate = endDate,
                TotalIncome = totalIncome,
                TotalExpenses = totalExpenses,
                Transactions = transactions
            };

            return periodReport;
        }

        private IEnumerable<TransactionWithCategoryName> GetTransactionsWithCategoryNames(DateTime startDate, DateTime endDate)
        {
            var transactions = _unitOfWOrk.FinanceTransactionRepository.GetAll().Where(t => t.Date >= startDate && t.Date <= endDate);
            var categories = _unitOfWOrk.CategoryRepository.GetAll();

            var transactionsWithCategoryNames = transactions.Join(
                categories,
                transaction => transaction.CategoryId,
                category => category.Id,
                (transaction, category) => new TransactionWithCategoryName
                {
                    Id = transaction.Id,
                    Amount = transaction.Amount,
                    Date = transaction.Date,
                    Note = transaction.Note,
                    CategoryName = category.Name,
                    CategoryId = category.Id
                });

            return transactionsWithCategoryNames;
        }

        //public DailyReport GetDailyReport(DateTime date)
        //{
        //    var transactions = GetTransactions(date, date);
        //    if (transactions.Count == 0)
        //    {
        //        throw new Exception("Date Report Not Found");
        //    }

        //    decimal totalIncome = transactions
        //        .Where(t => t.Amount > 0)
        //        .Sum(t => t.Amount);

        //    decimal totalExpenses = transactions
        //        .Where(t => t.Amount < 0)
        //        .Sum(t => t.Amount);

        //    var dailyReport = new DailyReport
        //    {
        //        Date = date,
        //        TotalIncome = totalIncome,
        //        TotalExpenses = totalExpenses,
        //        Transactions = transactions
        //    };

        //    return dailyReport;
        //}

        //public PeriodReport GetPeriodReport(DateTime startDate, DateTime endDate)
        //{
        //    var transactions = GetTransactions(startDate, endDate);
        //    if (transactions.Count == 0)
        //    {
        //        throw new Exception("Date Peridod not found");
        //    }

        //    decimal totalIncome = transactions
        //        .Where(t => t.Amount > 0)
        //        .Sum(t => t.Amount);

        //    decimal totalExpenses = transactions
        //        .Where(t => t.Amount < 0)
        //        .Sum(t => t.Amount);

        //    var periodReport = new PeriodReport
        //    {
        //        StartDate = startDate,
        //        EndDate = endDate,
        //        TotalIncome = totalIncome,
        //        TotalExpenses = totalExpenses,
        //        Transactions = transactions
        //    };

        //    return periodReport;
        //}

        //private List<FinanceTransaction> GetTransactions(DateTime startDate, DateTime endDate)
        //{
        //    return _unitOfWOrk.FinanceTransactionRepository.GetTransactions(startDate, endDate);
        //}
    }
}