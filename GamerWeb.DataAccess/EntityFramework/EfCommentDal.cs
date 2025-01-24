using GamerWeb.DataAccess.Abstract;
using GamerWeb.DataAccess.Context;
using GamerWeb.DataAccess.Repositories;
using GamerWeb.Entity.Entities;
using Microsoft.EntityFrameworkCore;

namespace GamerWeb.DataAccess.EntityFramework
{
    public class EfCommentDal : GenericRepository<Comment>, ICommentDal
    {
        public EfCommentDal(GamerWebContext context) : base(context)
        {
        }

        public async Task<List<Comment>> GetCommentsByNewsIdAsync(int newsId)
        {
            return await _context.Set<Comment>().Where(c => c.NewsId == newsId && c.IsApproved).ToListAsync();
        }

        public async Task<List<Comment>> GetCommentsByReviewIdAsync(int reviewId)
        {
            return await _context.Set<Comment>().Where(c => c.ReviewId == reviewId && c.IsApproved).ToListAsync();
        }

        public async Task<List<Comment>> GetUnapprovedCommentsAsync()
        {
            return await _context.Set<Comment>().Where(c => !c.IsApproved).ToListAsync();
        }
    }
}
