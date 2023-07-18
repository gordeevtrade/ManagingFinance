using Budget.DAL.Repositories.Interfaces;
using Domain.BL.Models;
using Domain.ServicesInterface;
using FamilyBudjetAPI.Sevices.Interface;

namespace ManagingFinance.BuisnessLogic.Sevices
{
    public class StatisticsService : IStatisticstService
    {
        private IUnitOfWOrk _unitOfWOrk;
        private ICategoryTypeService _categoryTypeService;

        public StatisticsService(IUnitOfWOrk unitOfWOrk, ICategoryTypeService categoryTypeService)
        {
            _unitOfWOrk = unitOfWOrk;
            _categoryTypeService = categoryTypeService;
        }

        public BudgetStatisticsData GetStatisticsData()
        {
            var transactionData = new BudgetStatisticsData();
            transactionData.TotalBalance = GetTotalBalance();
            transactionData.TransactionCount = _unitOfWOrk.FinanceTransactionRepository.GetAll().Count();
            transactionData.IncomeSubcategoriesCount = _categoryTypeService.GetIncomeSubcategoriesCount();
            transactionData.ExpenseSubcategoriesCount = _categoryTypeService.GetExpenseSubcategoriesCount();
            return transactionData;
        }

        private decimal GetTotalBalance()
        {
            var transactions = _unitOfWOrk.FinanceTransactionRepository.GetAll();
            if (!transactions.Any())
            {
                throw new Exception("No transactions found");
            }

            decimal totalIncome = transactions
                .Where(t => t.Amount > 0)
                .Sum(t => t.Amount);

            decimal totalExpenses = transactions
                .Where(t => t.Amount < 0)
                .Sum(t => t.Amount);

            return totalIncome + totalExpenses;
        }

        //public CombinedData GetCombinedData()
        //{
        //    var categoryTypes = _unitOfWOrk.CategoryTypeRepository.GetAll();
        //    var transactions = _unitOfWOrk.FinanceTransactionRepository.GetAll();

        //    var transactionsGroupedByCategory = transactions.GroupBy(t => t.CategoryId)
        //                                                    .ToDictionary(g => g.Key, g => g.Count());

        //    foreach (var categoryType in categoryTypes)
        //    {
        //        var subCategories = _unitOfWOrk.CategoryRepository.GetByCategoryType(categoryType.Id);

        //        foreach (var category in subCategories)
        //        {
        //            transactionsGroupedByCategory.TryGetValue(category.Id, out int transactionsCount);
        //            category.TransactionsCount = transactionsCount;
        //        }

        //        categoryType.subcategories = subCategories;
        //    }

        //    var combinedData = new CombinedData
        //    {
        //        Categories = categoryTypes,
        //    };

        //    return combinedData;
        //}

        //public CombinedData GetCombinedData()
        //{
        //    var categoryTypes = _unitOfWOrk.CategoryTypeRepository.GetAll();
        //    var transactions = _unitOfWOrk.FinanceTransactionRepository.GetAll();

        //    foreach (var categoryType in categoryTypes)
        //    {
        //        var subCategories = _unitOfWOrk.CategoryRepository.GetByCategoryType(categoryType.Id);

        //        foreach (var category in subCategories)
        //        {
        //            var transactionsCount = transactions.Count(t => t.CategoryId == category.Id);
        //            category.TransactionsCount = transactionsCount;
        //        }
        //        categoryType.subcategories = subCategories;
        //    }

        //    var combinedData = new CombinedData
        //    {
        //        Categories = categoryTypes,
        //    };
        //    return combinedData;
        //}
    }
}