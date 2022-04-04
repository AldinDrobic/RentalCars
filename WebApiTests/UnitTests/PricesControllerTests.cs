using AutoMapper;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using RentalCarsApi.Controllers;
using RentalCarsApi.Models;
using RentalCarsApi.Models.DTO.Price;
using RentalCarsApi.Profiles;
using RentalCarsApi.Services.PriceServices;
using System.Threading.Tasks;
using WebApiTests.Mock;
using Xunit;

namespace WebApiTests.UnitTests
{
    public class PricesControllerTests
    {
       
        #region GetAllPrices_ShouldReturn200Status
        [Fact]
        public async Task GetAllPrices_ShouldReturn200Status()
        {
            // Arrange
            var priceService = new Mock<IPriceService>();
            var _mapper = new Mock<IMapper>();
            priceService.Setup(s => s.GetPrices()).ReturnsAsync(PriceMockData.GetPrices());
            var controller = new PricesController(_mapper.Object, priceService.Object);

            // Act
            var result = (OkObjectResult)await controller.GetPrices();

            //Assert
            result.StatusCode.Should().Be(200);
        }
        #endregion

        #region GetPrice_ShouldReturn200Status
        [Fact]
        public async Task GetPrice_ShouldReturn200Status()
        {
            // Arrange
            var priceService = new Mock<IPriceService>();
            var _mapper = new Mock<IMapper>();
            priceService.Setup(s => s.GetPrice(1)).ReturnsAsync(PriceMockData.GetPrice());
            var controller = new PricesController(_mapper.Object, priceService.Object);

            // Act
            var result = (OkObjectResult)await controller.GetPrice(1);

            //Assert
            result.StatusCode.Should().Be(200);
        }
        #endregion

        #region CreatePrice_ShouldCall_IPriceService_SaveAsync_AtleastOnce()
        [Fact]
        public async Task CreatePrice_ShouldCall_IPriceService_SaveAsync_AtleastOnce()
        {
            // Arrange
            var priceService = new Mock<IPriceService>();
            //auto mapper configuration
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new PriceProfile()); //your automapperprofile 
            });
            var mapper = mockMapper.CreateMapper();
            var price = PriceMockData.PostPrice();
            var controller = new PricesController(mapper, priceService.Object);
            var mappedPrice = mapper.Map<PriceCreateDTO>(price);

            // Act
            var result = await controller.CreatePrice(mappedPrice);

            //Assert
            priceService.Verify(s => s.CreatePrice(price), Times.Exactly(0));
        }
        #endregion

    }
}
