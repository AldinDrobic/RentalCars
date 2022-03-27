using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RentalCarsApi.Data;
using RentalCarsApi.Models;
using RentalCarsApi.Models.DTO.Category;

namespace RentalCarsApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriesController: ControllerBase
    {
        private readonly RentalCarsDbContext _context;
        private readonly IMapper _mapper;

        public CategoriesController(RentalCarsDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        /// <summary>
        /// Get a list of all categories
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<List<CategoryReadDTO>>> GetCategories()
        {
            var categories = _mapper.Map<List<CategoryReadDTO>>(await _context.Categories.ToListAsync());

            return Ok(categories);
        }

        /// <summary>
        /// Get specific category by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryReadDTO>> GetCategory(int id)
        {
            var category = await _context.Categories.FindAsync(id);

            if (category == null)
                return NotFound();

            return Ok(_mapper.Map<CategoryReadDTO>(category));
        }

        /// <summary>
        /// Create a new category
        /// </summary>
        /// <param name="category">Category class that is used for creating new category object</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<CategoryReadDTO>> CreateCategory(CategoryCreateDTO dtoCategory)
        {
            if (dtoCategory == null)
                return BadRequest();
            Category domainCategory = _mapper.Map<Category>(dtoCategory);

            await _context.Categories.AddAsync(domainCategory);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCategory", new {id = domainCategory.Id}, _mapper.Map<CategoryReadDTO>(domainCategory));
        }

    }
}
