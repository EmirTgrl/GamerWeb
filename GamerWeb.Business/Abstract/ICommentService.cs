using GamerWeb.Entity.Entities;

namespace GamerWeb.Business.Abstract
{
    public interface ICommentService : IGenericService<Comment>
    {
        Task<List<Comment>> TGetCommentsByReviewIdAsync(int reviewId);
        Task<List<Comment>> TGetCommentsByNewsIdAsync(int newsId);
        Task<List<Comment>> TGetUnapprovedCommentsAsync();
        Task ApproveCommentAsync(int id);
    }
}
