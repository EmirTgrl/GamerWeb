using GamerWeb.DataAccess.Abstract;
using GamerWeb.DataAccess.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace GamerWeb.DataAccess.Repositories
{
	public class GenericRepository<T> : IGenericDal<T> where T : class
	{
		protected readonly GamerWebContext _context;

		public GenericRepository(GamerWebContext context)
		{
			_context = context;
		}

		public async Task CreateAsync(T entity)
		{
			await _context.AddAsync(entity);
			await _context.SaveChangesAsync();
		}

		public async Task DeleteAsync(int id)
		{
			var value = await GetByIdAsync(id);
			_context.Remove(value);
			await _context.SaveChangesAsync();
		}

		public async Task<T> GetByIdAsync(int id)
		{
			return await _context.Set<T>().FindAsync(id);
		}

        public async Task<List<T>> GetAllAsync(Expression<Func<T, bool>> filter = null)
        {
            return filter == null
                ? await _context.Set<T>().ToListAsync()
                : await _context.Set<T>().Where(filter).ToListAsync();
        }

        public async Task<List<T>> GetListAsync()
		{
			return await _context.Set<T>().ToListAsync();
		}

		public async Task UpdateAsync(T entity)
		{
			_context.Update(entity);
			await _context.SaveChangesAsync();
		}
	}
}
