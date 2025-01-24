using GamerWeb.DataAccess.Abstract;
using GamerWeb.DataAccess.Context;
using GamerWeb.DataAccess.Repositories;
using GamerWeb.Entity.Entities;
using Microsoft.EntityFrameworkCore;

namespace GamerWeb.DataAccess.EntityFramework
{
    public class EfReviewDal : GenericRepository<Review>, IReviewDal
    {
        public EfReviewDal(GamerWebContext context) : base(context)
        {
        }

        public async Task<List<Review>> GetLast4ReviewsAsync()
        {
            return await _context.Reviews.OrderByDescending(r => r.Date).Take(4).ToListAsync();
        }
    }
}
