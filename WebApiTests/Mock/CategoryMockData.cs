using RentalCarsApi.Models;
using System.Collections.Generic;

namespace WebApiTests.Mock
{
    public class CategoryMockData
    {
        public static List<Category> GetCategories()
        {
            return new List<Category>
            {
                new Category
                {
                    Id = 1,
                    Name = "Compact"
                },
                new Category
                {
                    Id = 2,
                    Name = "Premium"
                },
                new Category
                {
                    Id = 3,
                    Name = "Minivan"
                }
            };
        }

        public static Category GetCategory()
        {
            return new Category()
            {
                Id = 1,
                Name = "Compact"
            };
        }

        public static Category PostCategory()
        {
            return new Category()
            {
                Id = 1,
                Name = "Compact"
            };
        }
    }
}
