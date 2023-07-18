using AutoMapper;
using FamilyBudjetAPI.DTOModels;
using FamilyBudjetAPI.Sevices.Interface;
using Microsoft.AspNetCore.Mvc;

namespace FamilyBudjetAPI.Controllers
{
    //  [Authorize(AuthenticationSchemes = "Google,User")]
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;

        public CategoryController(ICategoryService categoryService, IMapper mapper)
        {
            _categoryService = categoryService;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<CategoryDTO>> GetCategories()
        {
            var categories = _categoryService.GetCategories();
            var categoryDtos = _mapper.Map<IEnumerable<CategoryDTO>>(categories);
            return Ok(categoryDtos);
        }

        [HttpPost]
        public ActionResult<CategoryDTO> CreateCategory(CategoryDTO categoryDto)
        {
            var category = _mapper.Map<Category>(categoryDto);
            var createdCategory = _categoryService.CreateCategory(category);
            var createdCategoryDto = _mapper.Map<CategoryDTO>(createdCategory);
            return Ok(createdCategoryDto);
        }

        [HttpGet("{id}")]
        public ActionResult<CategoryDTO> GetCategory(int id)
        {
            try
            {
                var category = _categoryService.GetCategory(id);
                var categoryDto = _mapper.Map<CategoryDTO>(category);
                return Ok(categoryDto);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("categorytype/{categoryTypeId}")]
        public ActionResult<IEnumerable<CategoryDTO>> GetCategoriesByCategoryType(int categoryTypeId)
        {
            try
            {
                var categories = _categoryService.GetCategoriesByCategoryType(categoryTypeId);
                var categoryDtos = _mapper.Map<IEnumerable<CategoryDTO>>(categories);
                return Ok(categoryDtos);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPut]
        public IActionResult UpdateCategory(CategoryDTO updatedCategoryDto)
        {
            try
            {
                var updatedCategory = _mapper.Map<Category>(updatedCategoryDto);
                _categoryService.UpdateCategory(updatedCategory);
                return NoContent();
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteCategory(int id)
        {
            try
            {
                _categoryService.DeleteCategory(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}