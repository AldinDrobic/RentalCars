using Microsoft.EntityFrameworkCore;
using RentalCarsApi.Data;
using RentalCarsApi.Models;

namespace RentalCarsApi.Services.CategoryServices
{
    public class CategoryService : ICategoryService
    {
        private readonly RentalCarsDbContext _context;

        public CategoryService(RentalCarsDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Add category to database
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task AddCategoryToDatabase(Category category)
        {
            try
            {
                await _context.Categories.AddAsync(category);
                await SaveDatabase();
            }
            catch (Exception)
            {
                throw new Exception("Could not add data to database!");
            }
        }

        /// <summary>
        /// Return bool based on if category exists or not
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool CategoryExists(int id)
        {
            return _context.Categories.Any(e => e.Id == id);
        }

        /// <summary>
        /// Create new category
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<Category> CreateCategory(Category category)
        {
            if (category == null)
                throw new Exception("You need to specify the car details!");

            await AddCategoryToDatabase(category);
            await SaveDatabase();

            return category;
        }

        /// <summary>
        /// Delete a category
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task DeleteCategory(int id)
        {
            var category = await _context.Categories.FindAsync(id);

            if (category == null)
                throw new Exception("Category not found");

            _context.Categories.Remove(category);
            await SaveDatabase();
        }

        /// <summary>
        /// Get a list of categories
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Category>> GetCategories()
        {
            return await _context.Categories
                .ToListAsync();
        }

        /// <summary>
        /// Get a specific category by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<Category> GetCategory(int id)
        {
            var category = await _context.Categories.FindAsync(id);

            if (category == null)
                throw new Exception("Car not found");

            return category;
        }

        /// <summary>
        /// Saves to database
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task SaveDatabase()
        {
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw new Exception("Could not save data to database!");
            }
        }

        /// <summary>
        /// Update a category
        /// </summary>
        /// <param name="id"></param>
        /// <param name="category"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task UpdateCategory(int id, Category category)
        {
            if (id != category.Id)
                throw new Exception("Identifier from endpoint doesn't match body id!");

            _context.Entry(category).State = EntityState.Modified;
            await SaveDatabase();
        }
    }
}
