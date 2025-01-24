using GamerWeb.DataAccess.Abstract;
using GamerWeb.DataAccess.Context;
using GamerWeb.DataAccess.Repositories;
using GamerWeb.Entity.Entities;
using Microsoft.EntityFrameworkCore;

namespace GamerWeb.DataAccess.EntityFramework
{
	public class EfNewsDal : GenericRepository<News>, INewsDal
	{
		public EfNewsDal(GamerWebContext context) : base(context)
		{
		}

		public async Task<List<News>> GetLast4NewsAsync()
		{
			return await _context.News.OrderByDescending(r => r.Date).Take(4).ToListAsync();
		}
	}
}
