using AutoMapper;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using RentalCarsApi.Controllers;
using RentalCarsApi.Models.DTO.Car;
using RentalCarsApi.Profiles;
using RentalCarsApi.Services.CarServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApiTests.Mock;
using Xunit;

namespace WebApiTests.UnitTests
{
    public class CarsControllerTests
    {
        #region GetAllCars_ShouldReturn200Status
        [Fact]
        public async Task GetAllCars_ShouldReturn200Status()
        {
            // Arrange
            var carService = new Mock<ICarService>();
            var _mapper = new Mock<IMapper>();
            carService.Setup(s => s.GetCars()).ReturnsAsync(CarMockData.GetCars());
            var controller = new CarsController(_mapper.Object, carService.Object);

            // Act
            var result = (OkObjectResult)await controller.GetCars();

            //Assert
            result.StatusCode.Should().Be(200);
        }
        #endregion

        #region GetCar_ShouldReturn200Status
        [Fact]
        public async Task GetCar_ShouldReturn200Status()
        {
            // Arrange
            var carService = new Mock<ICarService>();
            var _mapper = new Mock<IMapper>();
            carService.Setup(s => s.GetCar(1)).ReturnsAsync(CarMockData.GetCar());
            var controller = new CarsController(_mapper.Object, carService.Object);

            // Act
            var result = (OkObjectResult)await controller.GetCar(1);

            //Assert
            result.StatusCode.Should().Be(200);
        }
        #endregion

        #region CreateCar_ShouldCall_ICategoryService_SaveAsync_AtleastOnce
        [Fact]
        public async Task CreateCar_ShouldCall_ICarService_SaveAsync_AtleastOnce()
        {
            // Arrange
            var carService = new Mock<ICarService>();
            // Auto mapper configuration
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new CarProfile());
            });
            var mapper = mockMapper.CreateMapper();
            var car = CarMockData.PostCar();
            var controller = new CarsController(mapper, carService.Object);
            var mappedCar = mapper.Map<CarCreateDTO>(car);

            // Act
            var result = await controller.CreateCar(mappedCar);

            //Assert
            carService.Verify(s => s.CreateCar(car), Times.Exactly(0));
        }
        #endregion
    }
}
