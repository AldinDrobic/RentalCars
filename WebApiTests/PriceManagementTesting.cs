using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RentalCarsApi.Controllers;
using RentalCarsApi.Data;
using RentalCarsApi.Models;
using RentalCarsApi.Models.DTO.Price;
using RentalCarsApi.Services.PriceServices;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;
using Moq;
using WebApiTests.MockData;
using RentalCarsApi.Controllers;

namespace WebApiTests
{
    public class PriceManagementTesting
    {
        


        [Fact]
        public async Task GetPrices()
        {

            //Arrange
            var priceService = new Mock<IPriceService>();
            priceService.Setup(_ => _.GetPrices()).ReturnsAsync(PriceMockData.GetPrices());
            var sut = new PricesController(priceService.Object);


            //Act
            //var result = (ObjectResult)await sut.GetPrices();

            //Assert

        }
    }
}