using Budget.DAL.Repositories.Interfaces;
using Domain.RepositoriesInterfaces;
using FamilyBudjetAPI;
using ManagingFinance.DAL.Repositories;

namespace Budget.DAL.Repositories
{
    public class UnitOfWork : IUnitOfWOrk
    {
        private FinanceContext _financeContext;
        private ICategoryTypeRepository _categoryTypeRepository;
        private ICategoryRepository _categoryRepository;
        private IFinanceTransactionRepository _financeTransactionRepository;
        private IUserRepository _userRepository;

        public UnitOfWork(FinanceContext financeContext)
        {
            _financeContext = financeContext;
        }

        public ICategoryTypeRepository CategoryTypeRepository
        {
            get
            {
                if (_categoryTypeRepository == null)

                    _categoryTypeRepository = new CategoryTypeRepository(_financeContext);
                return _categoryTypeRepository;
            }
        }

        public ICategoryRepository CategoryRepository
        {
            get
            {
                if (_categoryRepository == null)
                    _categoryRepository = new CategoryRepository(_financeContext);
                return _categoryRepository;
            }
        }

        public IFinanceTransactionRepository FinanceTransactionRepository
        {
            get
            {
                if (_financeTransactionRepository == null)
                    _financeTransactionRepository = new FinanceTransactionRepository(_financeContext);
                return _financeTransactionRepository;
            }
        }

        public IUserRepository UserRepository
        {
            get
            {
                if (_userRepository == null)
                    _userRepository = new UserRepository(_financeContext);
                return _userRepository;
            }
        }
    }
}