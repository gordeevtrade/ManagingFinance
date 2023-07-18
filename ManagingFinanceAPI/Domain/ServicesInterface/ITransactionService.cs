using Domain.BL.Models;

namespace FamilyBudjetAPI.Sevices.Interface
{
    public interface ITransactionService
    {
        IEnumerable<FinanceTransaction> GetTransactions();

        FinanceTransaction GetTransaction(int id);

        FinanceTransaction CreateTransaction(FinanceTransaction transaction);

        void UpdateTransaction(FinanceTransaction updatedTransaction);

        void DeleteTransaction(int id);

        IEnumerable<FinanceTransaction> GetTransactionsByCategory(int categoryId);

        int GetTransactionsCount();

        IEnumerable<TransactionWithCategoryName> GetTransactionsWithCategoryNames();
    }
}