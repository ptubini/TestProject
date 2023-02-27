using TestProject.Domain.Common;

namespace TestProject.Application.Contracts
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        Task<T> GetById(Guid id);
        Task<IReadOnlyList<T>> GetAll();
        Task Create(T entity);
        Task Update(T entity);
        Task Delete(T entity);       
    }
}
