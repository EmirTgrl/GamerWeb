using GamerWeb.Entity.Entities;

namespace GamerWeb.DataAccess.Abstract
{
    public interface ICommentDal : IGenericDal<Comment>
    {
        Task<List<Comment>> GetCommentsByReviewIdAsync(int reviewId);
        Task<List<Comment>> GetCommentsByNewsIdAsync(int newsId);
        Task<List<Comment>> GetUnapprovedCommentsAsync();
    }
}
