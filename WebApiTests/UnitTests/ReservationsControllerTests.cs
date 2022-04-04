using AutoMapper;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using RentalCarsApi.Controllers;
using RentalCarsApi.Models.DTO.Reservation;
using RentalCarsApi.Profiles;
using RentalCarsApi.Services.CarServices;
using RentalCarsApi.Services.ReservationServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApiTests.Mock;
using Xunit;

namespace WebApiTests.UnitTests
{
    public class ReservationsControllerTests
    {
        #region GetAllReservations_ShouldReturn200Status
        [Fact]
        public async Task GetAllReservations_ShouldReturn200Status()
        {
            // Arrange
            var reservationService = new Mock<IReservationService>();
            var carService = new Mock<ICarService>();
            var _mapper = new Mock<IMapper>();
            reservationService.Setup(s => s.GetReservations()).ReturnsAsync(ReservationMockData.GetReservations());
            var controller = new ReservationsController(_mapper.Object, reservationService.Object, carService.Object);

            // Act
            var result = (OkObjectResult)await controller.GetReservations();

            //Assert
            result.StatusCode.Should().Be(200);
        }
        #endregion

        #region GetReservation_ShouldReturn200Status
        [Fact]
        public async Task GetReservation_ShouldReturn200Status()
        {
            // Arrange
            var reservationService = new Mock<IReservationService>();
            var carService = new Mock<ICarService>();
            var _mapper = new Mock<IMapper>();
            reservationService.Setup(s => s.GetReservationById(1)).ReturnsAsync(ReservationMockData.GetReservation());
            var controller = new ReservationsController(_mapper.Object, reservationService.Object, carService.Object);

            // Act
            var result = (OkObjectResult)await controller.GetReservationById(1);

            //Assert
            result.StatusCode.Should().Be(200);
        }
        #endregion

        #region CreateReservation_ShouldCall_ICategoryService_SaveAsync_AtleastOnce
        [Fact]
        public async Task CreateReservation_ShouldCall_IReservationService_SaveAsync_AtleastOnce()
        {
            // Arrange
            var reservationService = new Mock<IReservationService>();
            var carService = new Mock<ICarService>();
            // Auto mapper configuration
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new ReservationProfile());
            });
            var mapper = mockMapper.CreateMapper();
            var reservation = ReservationMockData.PostReservation();
            var controller = new ReservationsController(mapper, reservationService.Object, carService.Object);
            var mappedCar = mapper.Map<ReservationCreateDTO>(reservation);

            // Act
            var result = await controller.CreateReservation(mappedCar);

            //Assert
            reservationService.Verify(s => s.CreateReservation(reservation), Times.Exactly(0));
        }
        #endregion
    }
}
