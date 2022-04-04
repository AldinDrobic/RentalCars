using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RentalCarsApi.Models;
using RentalCarsApi.Models.DTO.Category;
using RentalCarsApi.Services.CategoryServices;

namespace RentalCarsApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ICategoryService _categoryService;

        public CategoriesController(IMapper mapper, ICategoryService categoryService)
        {
            _mapper = mapper;
            _categoryService = categoryService;
        }

        /// <summary>
        /// Get a list of all categories
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetCategories()
        {
            return Ok(_mapper.Map<List<CategoryReadDTO>>(await _categoryService.GetCategories()));
        }

        /// <summary>
        /// Get specific category by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategory(int id)
        {
            return Ok(_mapper.Map<CategoryReadDTO>(await _categoryService.GetCategory(id)));
        }

        /// <summary>
        /// Create new category 
        /// </summary>
        /// <param name="dtoCategory">Category class that is used for creating new category object</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<CategoryReadDTO>> CreateCategory(CategoryCreateDTO dtoCategory)
        {

            Category domainCategory = _mapper.Map<Category>(dtoCategory);

            await _categoryService.CreateCategory(domainCategory);

            return CreatedAtAction("GetCategory",
                new { id = domainCategory.Id },
                _mapper.Map<CategoryReadDTO>(domainCategory));
        }

        /// <summary>
        /// Deletes a category from database
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            if (!_categoryService.CategoryExists(id))
                return NotFound();

            await _categoryService.DeleteCategory(id);

            return NoContent();
        }

        /// <summary>
        /// Update a specific category by id
        /// </summary>
        /// <param name="id">Category objects identifier</param>
        /// <param name="dtoCategory">Category dto object that arrives from body</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateCategory(int id, CategoryEditDTO dtoCategory)
        {
            Category domainCategory = _mapper.Map<Category>(dtoCategory);
            await _categoryService.UpdateCategory(id, domainCategory);

            return CreatedAtAction("GetCategory",
                new { id = domainCategory.Id },
                _mapper.Map<CategoryReadDTO>(domainCategory));
        }


    }
}
