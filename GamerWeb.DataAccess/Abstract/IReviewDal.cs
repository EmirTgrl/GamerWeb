using GamerWeb.Entity.Entities;

namespace GamerWeb.DataAccess.Abstract
{
	public interface IReviewDal : IGenericDal<Review>
	{
        Task<List<Review>> GetLast4ReviewsAsync();
    }
}
