using FamilyBudjetAPI;

namespace Budget.DAL.Repositories.Interfaces
{
    public interface IFinanceTransactionRepository
    {
        IEnumerable<FinanceTransaction> GetAll();

        FinanceTransaction Get(int id);

        FinanceTransaction Create(FinanceTransaction transaction);

        IEnumerable<FinanceTransaction> GetByCategory(int categoryId);

        void Update(FinanceTransaction transaction);

        void Delete(FinanceTransaction financeTransaction);

        List<FinanceTransaction> GetTransactions(DateTime startDate, DateTime endDate);
    }
}