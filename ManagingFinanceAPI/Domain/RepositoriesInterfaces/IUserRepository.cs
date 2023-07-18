using Domain.DAL.Entity;
using System.Linq.Expressions;

namespace Domain.RepositoriesInterfaces
{
    public interface IUserRepository
    {
        void Add(User entity);

        Task<User> FirstOrDefaultAsync(Expression<Func<User, bool>> predicate);

        Task<bool> AnyAsync(Expression<Func<User, bool>> predicate);
    }
}