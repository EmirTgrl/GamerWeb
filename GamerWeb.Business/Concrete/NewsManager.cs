using GamerWeb.Business.Abstract;
using GamerWeb.DataAccess.Abstract;
using GamerWeb.Dto.Dtos.NewsDtos;
using GamerWeb.Entity.Entities;

namespace GamerWeb.Business.Concrete
{
    public class NewsManager : GenericManager<News>, INewsService
    {
        private readonly INewsDal _newsDal;

        public NewsManager(INewsDal newsDal) : base(newsDal)
        {
            _newsDal = newsDal;
        }

        public async Task<List<ResultNewsDto>> TGetLast4NewsAsync()
        {
            var news = await _newsDal.GetLast4NewsAsync();
            return news.Select(r => new ResultNewsDto
            {
                NewsId = r.NewsId,
                ImageUrl = r.ImageUrl,
                GameName = r.GameName,
                Title = r.Title,
                Description = r.Description,
                Date = r.Date
            }).ToList();
        }
    }
}
