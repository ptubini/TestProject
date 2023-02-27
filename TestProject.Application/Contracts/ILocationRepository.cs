using TestProject.Domain;

namespace TestProject.Application.Contracts
{
    public interface ILocationRepository : IGenericRepository<Location>
    {
        Task<IReadOnlyList<Location>> GetAvailableLocations();
    }
}
