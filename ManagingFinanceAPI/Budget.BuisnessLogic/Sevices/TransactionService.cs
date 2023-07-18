using Budget.DAL.Repositories.Interfaces;
using Domain.BL.Models;
using FamilyBudjetAPI.Sevices.Interface;

namespace FamilyBudjetAPI.Sevices
{
    public class TransactionService : ITransactionService
    {
        private IUnitOfWOrk _unitOfWOrk;

        public TransactionService(IUnitOfWOrk unitOfWOrk)
        {
            _unitOfWOrk = unitOfWOrk;
        }

        public IEnumerable<FinanceTransaction> GetTransactions()
        {
            return _unitOfWOrk.FinanceTransactionRepository.GetAll();
        }

        public IEnumerable<TransactionWithCategoryName> GetTransactionsWithCategoryNames()
        {
            var transactions = _unitOfWOrk.FinanceTransactionRepository.GetAll();
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
                    CategoryName = category.Name
                });

            return transactionsWithCategoryNames;
        }

        public int GetTransactionsCount()
        {
            return _unitOfWOrk.FinanceTransactionRepository.GetAll().Count();
        }

        public FinanceTransaction GetTransaction(int id)
        {
            var transaction = _unitOfWOrk.FinanceTransactionRepository.Get(id);

            if (transaction == null)
            {
                throw new Exception("Transaction not found");
            }

            return transaction;
        }

        public FinanceTransaction CreateTransaction(FinanceTransaction transaction)
        {
            _unitOfWOrk.FinanceTransactionRepository.Create(transaction);
            return transaction;
        }

        public void UpdateTransaction(FinanceTransaction updatedTransaction)
        {
            var transaction = _unitOfWOrk.FinanceTransactionRepository.Get(updatedTransaction.Id);

            if (transaction == null)
            {
                throw new Exception($"Transaction with id {updatedTransaction.Id} not found");
            }

            transaction.Amount = updatedTransaction.Amount;
            transaction.Date = updatedTransaction.Date;
            transaction.Note = updatedTransaction.Note;
            transaction.CategoryId = updatedTransaction.CategoryId;
            _unitOfWOrk.FinanceTransactionRepository.Update(transaction);
        }

        public void DeleteTransaction(int id)
        {
            var transaction = _unitOfWOrk.FinanceTransactionRepository.Get(id);

            if (transaction == null)
            {
                throw new Exception($"Transaction with id {id} not found");
            }
            _unitOfWOrk.FinanceTransactionRepository.Delete(transaction);
        }

        public IEnumerable<FinanceTransaction> GetTransactionsByCategory(int categoryId)
        {
            var transactions = _unitOfWOrk.FinanceTransactionRepository.GetByCategory(categoryId);

            if (!transactions.Any())
            {
                throw new Exception("Transactions not found for the provided category");
            }

            return transactions;
        }
    }
}