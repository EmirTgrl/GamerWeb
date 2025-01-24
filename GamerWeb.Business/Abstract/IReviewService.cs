using GamerWeb.Dto.Dtos.ReviewDtos;
using GamerWeb.Entity.Entities;

namespace GamerWeb.Business.Abstract
{
	public interface IReviewService : IGenericService<Review>
	{
        Task<List<ResultReviewDto>> TGetLast4ReviewsAsync();
    }
}
