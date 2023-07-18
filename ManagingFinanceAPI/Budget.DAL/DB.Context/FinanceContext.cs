using Domain.DAL.Entity;
using Microsoft.EntityFrameworkCore;

namespace FamilyBudjetAPI
{
    public class FinanceContext : DbContext
    {
        public DbSet<CategoryType> CategoryTypes { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<FinanceTransaction> FinacneTransactions { get; set; }
        public DbSet<User> UserRegistration { get; set; }

        public FinanceContext(DbContextOptions<FinanceContext> options)
   : base(options)
        {
        }

        public FinanceContext()
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            var incomeCategoryType = new CategoryType { Id = 1, Name = "Income" };
            var expenseCategoryType = new CategoryType { Id = 2, Name = "Expense" };

            var salaryCategory = new Category { Id = 1, Name = "Salary", CategoryTypeId = incomeCategoryType.Id };
            var Stock = new Category { Id = 2, Name = "Stock", CategoryTypeId = incomeCategoryType.Id };
            var Houses = new Category { Id = 3, Name = "Houses", CategoryTypeId = incomeCategoryType.Id };
            var groceriesCategory = new Category { Id = 4, Name = "Groceries", CategoryTypeId = expenseCategoryType.Id };
            var Animals = new Category { Id = 5, Name = "Собаки", CategoryTypeId = expenseCategoryType.Id };

            var SalaryTransaction = new FinanceTransaction { Id = 1, Amount = 1000M, Date = DateTime.Now, Note = "Salary for May", CategoryId = salaryCategory.Id };
            var StockTransaction = new FinanceTransaction { Id = 2, Amount = 950M, Date = DateTime.Now, Note = "Microsoft", CategoryId = Stock.Id };
            var HouseTransaction = new FinanceTransaction { Id = 3, Amount = 1850M, Date = DateTime.Now, Note = "Avalon", CategoryId = Houses.Id };
            var GroceriesTransaction = new FinanceTransaction { Id = 4, Amount = -200M, Date = DateTime.Now, Note = "Grocery shopping", CategoryId = groceriesCategory.Id };
            var AnimalsTransaction = new FinanceTransaction { Id = 5, Amount = -8700M, Date = DateTime.Now, Note = "Собаки ", CategoryId = Animals.Id };

            modelBuilder.Entity<CategoryType>().HasData(incomeCategoryType);
            modelBuilder.Entity<CategoryType>().HasData(expenseCategoryType);

            modelBuilder.Entity<Category>().HasData(salaryCategory);
            modelBuilder.Entity<Category>().HasData(Stock);
            modelBuilder.Entity<Category>().HasData(Houses);
            modelBuilder.Entity<Category>().HasData(groceriesCategory);
            modelBuilder.Entity<Category>().HasData(Animals);

            modelBuilder.Entity<FinanceTransaction>().HasData(SalaryTransaction);
            modelBuilder.Entity<FinanceTransaction>().HasData(StockTransaction);
            modelBuilder.Entity<FinanceTransaction>().HasData(HouseTransaction);
            modelBuilder.Entity<FinanceTransaction>().HasData(GroceriesTransaction);
            modelBuilder.Entity<FinanceTransaction>().HasData(AnimalsTransaction);
        }
    }
}