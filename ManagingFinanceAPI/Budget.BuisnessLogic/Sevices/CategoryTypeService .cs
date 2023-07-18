using Budget.DAL.Repositories.Interfaces;
using Domain.BL.Models;
using FamilyBudjetAPI.Sevices.Interface;

namespace FamilyBudjetAPI.Sevices
{
    public class CategoryTypeService : ICategoryTypeService
    {
        private IUnitOfWOrk _unitOfWork;
        private const int IncomeCategoryId = 1;
        private const int ExpenseCategoryId = 2;

        public CategoryTypeService(IUnitOfWOrk unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<CategoryType> GetCategoryTypes()
        {
            return _unitOfWork.CategoryTypeRepository.GetAll();
        }

        public CategoryType CreateCategoryType(CategoryType categoryType)
        {
            return _unitOfWork.CategoryTypeRepository.Create(categoryType);
        }

        public CategoryType GetCategoryType(int id)
        {
            var categoryType = _unitOfWork.CategoryTypeRepository.Get(id);
            if (categoryType == null)
            {
                throw new Exception($"CategoryType with id {id} not found.");
            }
            return categoryType;
        }

        public void UpdateCategoryType(int id, CategoryType updatedCategoryType)
        {
            var categoryType = _unitOfWork.CategoryTypeRepository.Get(id);
            if (categoryType == null)
            {
                throw new Exception($"CategoryType with id {id} not found.");
            }
            categoryType.Name = updatedCategoryType.Name;
            _unitOfWork.CategoryTypeRepository.Update(categoryType);
        }

        public void DeleteCategoryType(int id)
        {
            var categoryType = _unitOfWork.CategoryTypeRepository.Get(id);
            if (categoryType == null)
            {
                throw new Exception($"CategoryType with id {id} not found.");
            }
            _unitOfWork.CategoryTypeRepository.Delete(categoryType);
        }

        public int GetIncomeSubcategoriesCount()
        {
            return _unitOfWork.CategoryRepository.CountByCategory(IncomeCategoryId);
        }

        public int GetExpenseSubcategoriesCount()
        {
            return _unitOfWork.CategoryRepository.CountByCategory(ExpenseCategoryId);
        }

        public CategoryTypeCollection ReturnCategoryTypeCollection()
        {
            var categoryTypes = GetCategoryTypes();
            var transactions = _unitOfWork.FinanceTransactionRepository.GetAll();

            var transactionsGroupedByCategory = transactions.GroupBy(t => t.CategoryId)
                                                            .ToDictionary(g => g.Key, g => g.Count());

            foreach (var categoryType in categoryTypes)
            {
                var subCategories = _unitOfWork.CategoryRepository.GetByCategoryType(categoryType.Id);

                foreach (var category in subCategories)
                {
                    transactionsGroupedByCategory.TryGetValue(category.Id, out int transactionsCount);
                    category.TransactionsCount = transactionsCount;
                }

                categoryType.subcategories = subCategories;
            }

            var combinedData = new CategoryTypeCollection
            {
                Categories = categoryTypes,
            };

            return combinedData;
        }
    }
}