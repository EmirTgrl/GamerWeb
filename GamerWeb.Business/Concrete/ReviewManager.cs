using GamerWeb.Business.Abstract;
using GamerWeb.DataAccess.Abstract;
using GamerWeb.Dto.Dtos.ReviewDtos;
using GamerWeb.Entity.Entities;

namespace GamerWeb.Business.Concrete
{
	public class ReviewManager : GenericManager<Review>, IReviewService
	{
        private readonly IReviewDal _reviewDal;
        public ReviewManager(IReviewDal reviewDal) : base(reviewDal)
        {
            _reviewDal = reviewDal;
        }

        public async Task<List<ResultReviewDto>> TGetLast4ReviewsAsync()
        {
            var reviews = await _reviewDal.GetLast4ReviewsAsync();
            return reviews.Select(r => new ResultReviewDto
            {
                ReviewId = r.ReviewId,
                GameImage = r.GameImage,
                GameName = r.GameName,
                Title = r.Title,
                Description = r.Description,
                Date = r.Date
            }).ToList();
        }
    }
}
