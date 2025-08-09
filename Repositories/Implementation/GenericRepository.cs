using EmployeeCustomProp.Models.DB;
using EmployeeCustomProp.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EmployeeCustomProp.Repositories.Implementation
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly AppDbContext _context;
        private readonly DbSet<T> _dbSet;

        public GenericRepository(AppDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }
        public async virtual Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public async virtual Task DeleteAsync(int id)
        {
            var entity = await _dbSet.FindAsync(id);
            if (entity != null)
            {
                _dbSet.Remove(entity);
            }
            return;
        }

        public async virtual Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.AsNoTracking().ToListAsync();
        }

        public async virtual Task<T> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);

        }

        public virtual void Update(T entity)
        {
            _dbSet.Update(entity);
        }

        public async virtual Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
