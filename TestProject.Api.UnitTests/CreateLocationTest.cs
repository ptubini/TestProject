using Microsoft.AspNetCore.Mvc;
using Moq;
using TestProject.Api.Controllers;
using TestProject.Api.Exceptions;
using TestProject.Application.Contracts;
using TestProject.Application.Models;
using TestProject.Application.UnitTests.Mocks;
using TestProject.Domain;

namespace TestProject.Application.UnitTests
{
    public class CreateLocationTest
    {
        private readonly Mock<ILocationRepository> _mockRepo;
        private readonly LocationsController _controller;

        public CreateLocationTest()
        {
            _mockRepo = MockLocationRepository.GetMockLocationRepository();
            _controller = new LocationsController(_mockRepo.Object);
        }

        [Fact]
        public async Task Get_Available_Locations_Returns_Truthy()
        {
            var response = await _controller.GetLocations();
            OkObjectResult okObjectResult = Assert.IsType<OkObjectResult>(response.Result);
            List<LocationDto> locations = Assert.IsType<List<LocationDto>>(okObjectResult.Value);
            Assert.Single(locations);
        }

        [Fact]
        public async Task Create_Location_Returns_Truthy()
        {
            var newLocation = new CreateLocationDto
            (
                "Testing",
                new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 10, 0, 0),
                new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 12, 0, 0)
            );

            var createdResponse = await _controller.CreateLocation(newLocation);

            CreatedAtActionResult createdResult = Assert.IsType<CreatedAtActionResult>(createdResponse.Result);
            Location createdLocation = (Location)createdResult.Value;
            Assert.Equal(newLocation.Name, createdLocation.Name);
            Assert.Equal(newLocation.OpenTime, createdLocation.OpenTime);
            Assert.Equal(newLocation.CloseTime, createdLocation.CloseTime);
        }

        [Fact]
        public async Task Create_Location_Returns_Falsy()
        {
            var newLocation = new CreateLocationDto
            (
                "Testing",
                new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 12, 0, 0),
                new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 12, 0, 0)
            );

            await Assert.ThrowsAsync<BadRequestException>(async () =>
            {
                await _controller.CreateLocation(newLocation);
            });
        }
    }
}