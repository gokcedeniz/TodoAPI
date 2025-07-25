using Domain.Entities.Base;
using Domain.Interfaces;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class BaseRepository<T> : IRepository<T> where T : TBase
    {
        private readonly AppDbContext _context;
        private readonly DbSet<T> _dbSet;

        public BaseRepository(AppDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }
        private IQueryable<T> List => _dbSet.Where(x => x.IsDeleted == false);
        public async Task<List<T>> GetAllAsync() => await List.ToListAsync();
        public async Task<T> GetByIdAsync(Guid id) => await List.FirstOrDefaultAsync(x => x.Id == id);
        public async Task AddAsync(T item)
        {
            _dbSet.Add(item);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateAsync(T item)
        {
            _dbSet.Update(item);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteAsync(Guid id)
        {
            var item = await GetByIdAsync(id);
            if (item != null)
            {
                _dbSet.Remove(item);
                await _context.SaveChangesAsync();
            }
        }
    }
}
