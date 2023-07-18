using Budget.DAL.Repositories.Interfaces;
using FamilyBudjetAPI.Sevices.Interface;

namespace FamilyBudjetAPI.Sevices
{
    public class CategoryService : ICategoryService
    {
        private IUnitOfWOrk _unitOfWork;

        public CategoryService(IUnitOfWOrk unitOfWOrk)
        {
            _unitOfWork = unitOfWOrk;
        }

        public IEnumerable<Category> GetCategories()
        {
            return _unitOfWork.CategoryRepository.GetAll();
        }

        public Category CreateCategory(Category category)
        {
            return _unitOfWork.CategoryRepository.Create(category);
        }

        public Category GetCategory(int id)
        {
            var category = _unitOfWork.CategoryRepository.Get(id);
            if (category == null)
            {
                throw new Exception($"Category with id {id} not found");
            }
            return category;
        }

        public IEnumerable<Category> GetCategoriesByCategoryType(int categoryTypeId)
        {
            var categorys = _unitOfWork.CategoryRepository.GetByCategoryType(categoryTypeId);
            if (!categorys.Any())
            {
                throw new Exception("Category not found for the provided category");
            }
            return categorys;
        }

        public void UpdateCategory(Category updatedCategory)
        {
            var category = _unitOfWork.CategoryRepository.Get(updatedCategory.Id);
            if (category == null)
            {
                throw new Exception($"Category with id {updatedCategory.Id} not found");
            }
            category.Name = updatedCategory.Name;
            category.CategoryTypeId = updatedCategory.CategoryTypeId;
            _unitOfWork.CategoryRepository.Update(category);
        }

        public void DeleteCategory(int id)
        {
            var category = _unitOfWork.CategoryRepository.Get(id);
            if (category == null)
            {
                throw new Exception($"Category with id {id} not found");
            }
            _unitOfWork.CategoryRepository.Delete(category);
        }
    }
}