using Domain.DAL.Entity;
using Domain.RepositoriesInterfaces;
using FamilyBudjetAPI;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ManagingFinance.DAL.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly FinanceContext _context;

        public UserRepository(FinanceContext context)
        {
            _context = context;
        }

        public void Add(User entity)
        {
            _context.UserRegistration.Add(entity);
            _context.SaveChanges();
        }

        public async Task<User> FirstOrDefaultAsync(Expression<Func<User, bool>> predicate)
        {
            return await _context.UserRegistration.FirstOrDefaultAsync(predicate);
        }

        public async Task<bool> AnyAsync(Expression<Func<User, bool>> predicate)
        {
            return await _context.UserRegistration.AnyAsync(predicate);
        }
    }
}