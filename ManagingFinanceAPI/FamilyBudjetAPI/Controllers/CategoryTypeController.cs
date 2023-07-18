using AutoMapper;
using FamilyBudjetAPI.DTOModels;
using FamilyBudjetAPI.Sevices.Interface;
using ManagingFinanceAPI.DTOModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FamilyBudjetAPI.Controllers
{
    //   [Authorize(AuthenticationSchemes = "Google,User")]
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryTypeController : ControllerBase
    {
        private readonly ICategoryTypeService _categoryTypeService;
        private readonly IMapper _mapper;

        public CategoryTypeController(ICategoryTypeService categoryTypeService, IMapper mapper)
        {
            _categoryTypeService = categoryTypeService;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<CategoryTypeDTO>> GetCategoryTypes()
        {
            var categoryTypes = _categoryTypeService.GetCategoryTypes();
            var categoryTypeDto = _mapper.Map<IEnumerable<CategoryTypeDTO>>(categoryTypes);
            return Ok(categoryTypeDto);
        }

        [HttpPost]
        public ActionResult<CategoryTypeDTO> CreateCategoryType(CategoryTypeDTO categoryTypeDto)
        {
            var categoryType = _mapper.Map<CategoryType>(categoryTypeDto);
            var createdCategoryType = _categoryTypeService.CreateCategoryType(categoryType);
            var createdCategoryTypeDto = _mapper.Map<CategoryTypeDTO>(createdCategoryType);
            return Ok(createdCategoryTypeDto);
        }

        [HttpGet("income-subcategories-count")]
        public ActionResult<int> GetIncomeSubcategoriesCount()
        {
            try
            {
                return Ok(_categoryTypeService.GetIncomeSubcategoriesCount());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("expense-subcategories-count")]
        public ActionResult<int> GetExpenseSubcategoriesCount()
        {
            try
            {
                return Ok(_categoryTypeService.GetExpenseSubcategoriesCount());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public ActionResult<CategoryTypeDTO> GetCategoryType(int id)
        {
            try
            {
                var categoryType = _categoryTypeService.GetCategoryType(id);
                var categoryTypeDto = _mapper.Map<CategoryTypeDTO>(categoryType);
                return Ok(categoryTypeDto);
            }
            catch (DbUpdateException dbEx)
            {
                return StatusCode(500, $"Database exception: {dbEx.Message}");
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public IActionResult UpdateCategoryType(int id, CategoryTypeDTO updatedCategoryTypeDto)
        {
            try
            {
                var updatedCategoryType = _mapper.Map<CategoryType>(updatedCategoryTypeDto);
                _categoryTypeService.UpdateCategoryType(id, updatedCategoryType);
                return NoContent();
            }
            catch (DbUpdateException dbEx)
            {
                return StatusCode(500, $"Database exception: {dbEx.Message}");
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteCategoryType(int id)
        {
            try
            {
                _categoryTypeService.DeleteCategoryType(id);
                return NoContent();
            }
            catch (DbUpdateException dbEx)
            {
                return StatusCode(500, $"Database exception: {dbEx.Message}");
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("category-data-collection")]
        public ActionResult<CategoryTypeCollectionDTO> GetCombinedData()
        {
            try
            {
                var combinatedData = _categoryTypeService.ReturnCategoryTypeCollection();
                var combinatedDataDto = _mapper.Map<CategoryTypeCollectionDTO>(combinatedData);

                return Ok(combinatedDataDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }
    }
}