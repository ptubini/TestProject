using Microsoft.EntityFrameworkCore;
using TestProject.Application.Contracts;
using TestProject.Domain;
using TestProject.Persistence.DatabaseContext;

namespace TestProject.Persistence.Repositories
{
    public class LocationRepository : GenericRepository<Location>, ILocationRepository
    {
        public LocationRepository(LocationDatabaseContext dbContext) : base(dbContext)
        {
        }

        public async Task<IReadOnlyList<Location>> GetAvailableLocations()
        {
            return await _dbContext.Set<Location>()
                .Where(q => q.OpenTime.TimeOfDay >= new TimeSpan(10, 0, 0) 
                    && q.CloseTime.TimeOfDay <= new TimeSpan(13, 0, 0))
                .ToListAsync();
        }
    }
}
