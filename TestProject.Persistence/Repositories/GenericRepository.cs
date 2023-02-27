using Microsoft.EntityFrameworkCore;
using TestProject.Application.Contracts;
using TestProject.Domain.Common;
using TestProject.Persistence.DatabaseContext;

namespace TestProject.Persistence.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        protected readonly LocationDatabaseContext _dbContext;

        public GenericRepository(LocationDatabaseContext dbContext) 
        {
            _dbContext = dbContext;
        }

        public async Task<IReadOnlyList<T>> GetAll()
        {
            return await _dbContext.Set<T>().AsNoTracking().ToListAsync();
        }

        public async Task<T> GetById(Guid id)
        {
            return await _dbContext.Set<T>().FindAsync(id);
        }

        public async Task Create(T entity)
        {
            await _dbContext.AddAsync<T>(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task Update(T entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }

        public async Task Delete(T entity)
        {
            _dbContext.Remove(entity);
            await _dbContext.SaveChangesAsync();
        }
    }
}
