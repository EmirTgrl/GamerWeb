using GamerWeb.Business.Abstract;
using GamerWeb.DataAccess.Abstract;
using GamerWeb.Entity.Entities;

namespace GamerWeb.Business.Concrete
{
    public class CommentManager : GenericManager<Comment>, ICommentService
    {
        private readonly ICommentDal _commentDal;

        public CommentManager(ICommentDal commentDal) : base(commentDal)
        {
            _commentDal = commentDal;
        }

        public async Task ApproveCommentAsync(int id)
        {
            var comment = await _commentDal.GetByIdAsync(id);
            if (comment != null)
            {
                comment.IsApproved = true;
                await _commentDal.UpdateAsync(comment);
            }
        }

        public async Task<List<Comment>> TGetCommentsByNewsIdAsync(int newsId)
        {
            return await _commentDal.GetCommentsByNewsIdAsync(newsId); // Sadece onaylı yorumlar
        }

        public async Task<List<Comment>> TGetCommentsByReviewIdAsync(int reviewId)
        {
            return await _commentDal.GetCommentsByReviewIdAsync(reviewId); // Sadece onaylı yorumlar
        }

        public async Task<List<Comment>> TGetUnapprovedCommentsAsync()
        {
            return await _commentDal.GetUnapprovedCommentsAsync(); // Sadece onaysız yorumlar
        }
    }

}
