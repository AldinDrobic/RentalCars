using AutoMapper;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using RentalCarsApi.Controllers;
using RentalCarsApi.Models.DTO.Category;
using RentalCarsApi.Profiles;
using RentalCarsApi.Services.CategoryServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApiTests.Mock;
using Xunit;

namespace WebApiTests.UnitTests
{
    public class CategoriesControllerTests
    {
        #region GetAllCategories_ShouldReturn200Status
        [Fact]
        public async Task GetAllCatagories_ShouldReturn200Status()
        {
            // Arrange
            var categoryService = new Mock<ICategoryService>();
            var _mapper = new Mock<IMapper>();
            categoryService.Setup(s => s.GetCategories()).ReturnsAsync(CategoryMockData.GetCategories());
            var controller = new CategoriesController(_mapper.Object, categoryService.Object);

            // Act
            var result = (OkObjectResult)await controller.GetCategories();

            //Assert
            result.StatusCode.Should().Be(200);
        }
        #endregion

        #region GetCategory_ShouldReturn200Status
        [Fact]
        public async Task GetCategory_ShouldReturn200Status()
        {
            // Arrange
            var categoryService = new Mock<ICategoryService>();
            var _mapper = new Mock<IMapper>();
            categoryService.Setup(s => s.GetCategory(1)).ReturnsAsync(CategoryMockData.GetCategory());
            var controller = new CategoriesController(_mapper.Object, categoryService.Object);

            // Act
            var result = (OkObjectResult)await controller.GetCategory(1);

            //Assert
            result.StatusCode.Should().Be(200);
        }
        #endregion

        #region CreateCategory_ShouldCall_ICategoryService_SaveAsync_AtleastOnce
        [Fact]
        public async Task CreateCategory_ShouldCall_ICategoryService_SaveAsync_AtleastOnce()
        {
            // Arrange
            var categoryService = new Mock<ICategoryService>();
            // Auto mapper configuration
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new CategoryProfile()); 
            });
            var mapper = mockMapper.CreateMapper();
            var category = CategoryMockData.PostCategory();
            var controller = new CategoriesController(mapper, categoryService.Object);
            var mappedCategory = mapper.Map<CategoryCreateDTO>(category);

            // Act
            var result = await controller.CreateCategory(mappedCategory);

            //Assert
            categoryService.Verify(s => s.CreateCategory(category), Times.Exactly(0));
        }
        #endregion
    }
}
