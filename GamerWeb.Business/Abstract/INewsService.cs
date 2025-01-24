using GamerWeb.Dto.Dtos.NewsDtos;
using GamerWeb.Dto.Dtos.ReviewDtos;
using GamerWeb.Entity.Entities;

namespace GamerWeb.Business.Abstract
{
	public interface INewsService : IGenericService<News>
	{
		Task<List<ResultNewsDto>> TGetLast4NewsAsync();
	}
}
