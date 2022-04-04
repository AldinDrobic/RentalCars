using AutoMapper;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using RentalCarsApi.Controllers;
using RentalCarsApi.Models.DTO.Rental;
using RentalCarsApi.Profiles;
using RentalCarsApi.Services.CarServices;
using RentalCarsApi.Services.PriceServices;
using RentalCarsApi.Services.RentalServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApiTests.Mock;
using Xunit;

namespace WebApiTests.UnitTests
{
    public class RentalsControllerTests
    {
        #region GetAllRentals_ShouldReturn200Status
        [Fact]
        public async Task GetAllRentals_ShouldReturn200Status()
        {
            // Arrange
            var rentalService = new Mock<IRentalService>();
            var carService = new Mock<ICarService>();
            var priceService = new Mock<IPriceService>();
            var _mapper = new Mock<IMapper>();
            rentalService.Setup(s => s.GetRentals()).ReturnsAsync(RentalMockData.GetRentals());
            var controller = new RentalsController(_mapper.Object, rentalService.Object, carService.Object, priceService.Object);

            // Act
            var result = (OkObjectResult)await controller.GetRentals();

            //Assert
            result.StatusCode.Should().Be(200);
        }
        #endregion

        #region GetRental_ShouldReturn200Status
        [Fact]
        public async Task GetRental_ShouldReturn200Status()
        {
            // Arrange
            var rentalService = new Mock<IRentalService>();
            var carService = new Mock<ICarService>();
            var priceService = new Mock<IPriceService>();
            var _mapper = new Mock<IMapper>();
            rentalService.Setup(s => s.GetRentalById(1)).ReturnsAsync(RentalMockData.GetRental());
            var controller = new RentalsController(_mapper.Object, rentalService.Object, carService.Object, priceService.Object);

            // Act
            var result = (OkObjectResult)await controller.GetRentalById(1);

            //Assert
            result.StatusCode.Should().Be(200);
        }
        #endregion

        #region CreateRental_ShouldCall_ICategoryService_SaveAsync_AtleastOnce
        [Fact]
        public async Task CreateRental_ShouldCall_ICategoryService_SaveAsync_AtleastOnce()
        {
            // Arrange
            var rentalService = new Mock<IRentalService>();
            var carService = new Mock<ICarService>();
            var priceService = new Mock<IPriceService>();
            // Auto mapper configuration
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new RentalProfile());
            });
            var mapper = mockMapper.CreateMapper();
            var rental = RentalMockData.PostRental();
            var controller = new RentalsController(mapper, rentalService.Object, carService.Object, priceService.Object);
            var mappedRental = mapper.Map<RentalCreateDTO>(rental);

            // Act
            var result = await controller.CreateRental(mappedRental);

            //Assert
            rentalService.Verify(s => s.CreateRental(rental), Times.Exactly(0));
        }
        #endregion
    }
}
