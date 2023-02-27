using Moq;
using TestProject.Application.Contracts;
using TestProject.Domain;

namespace TestProject.Application.UnitTests.Mocks
{
    public class MockLocationRepository
    {
        public static Mock<ILocationRepository> GetMockLocationRepository()
        {
            List<Location> locations = new List<Location>
            {
                new Location
                {
                    Id = Guid.NewGuid(),
                    Name = "Test Bakery",
                    OpenTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 10, 0, 0),
                    CloseTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 14, 0, 0),
                },
                new Location
                {
                    Id = Guid.NewGuid(),
                    Name = "Test BarberShop",
                    OpenTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 10, 0, 0),
                    CloseTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 13, 0, 0),
                },
                new Location
                {
                    Id = Guid.NewGuid(),
                    Name = "Test SuperMarket",
                    OpenTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 10, 0, 0),
                    CloseTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 18, 0, 0),
                },
            };

            var mockRepo = new Mock<ILocationRepository>();

            mockRepo.Setup(r => r.GetAll()).ReturnsAsync(locations);

            mockRepo.Setup(r => r.GetAvailableLocations()).ReturnsAsync(locations
                .Where(q => q.OpenTime.TimeOfDay >= new TimeSpan(10, 0, 0)
                    && q.CloseTime.TimeOfDay <= new TimeSpan(13, 0, 0))
                .ToList());

            mockRepo.Setup(r => r.Create(It.IsAny<Location>()))
                .Returns((Location location) =>
                {
                    locations.Add(location);
                    return Task.CompletedTask;
                });

            return mockRepo;
        }
    }
}
