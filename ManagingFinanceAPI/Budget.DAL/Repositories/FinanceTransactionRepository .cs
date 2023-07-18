using Budget.DAL.Repositories.Interfaces;
using FamilyBudjetAPI;

namespace Budget.DAL.Repositories
{
    public class FinanceTransactionRepository : IFinanceTransactionRepository
    {
        protected readonly FinanceContext _context;

        public FinanceTransactionRepository(FinanceContext context)
        {
            _context = context;
        }

        public IEnumerable<FinanceTransaction> GetAll()
        {
            return _context.FinacneTransactions.ToList();
        }

        public FinanceTransaction Get(int id)
        {
            return _context.FinacneTransactions.Find(id);
        }

        public FinanceTransaction Create(FinanceTransaction transaction)
        {
            _context.FinacneTransactions.Add(transaction);
            _context.SaveChanges();
            return transaction;
        }

        public IEnumerable<FinanceTransaction> GetByCategory(int categoryId)
        {
            var transactions = _context.FinacneTransactions
                    .Where(t => t.CategoryId == categoryId)
                    .ToList();

            return transactions;
        }

        public List<FinanceTransaction> GetTransactions(DateTime startDate, DateTime endDate)
        {
            return _context.FinacneTransactions
                .Where(t => t.Date.Date >= startDate.Date && t.Date.Date <= endDate.Date)
                .ToList();
        }

        public void Update(FinanceTransaction transaction)
        {
            _context.FinacneTransactions.Update(transaction);
            _context.SaveChanges();
        }

        public void Delete(FinanceTransaction financeTransaction)
        {
            _context.FinacneTransactions.Remove(financeTransaction);
            _context.SaveChanges();
        }
    }
}