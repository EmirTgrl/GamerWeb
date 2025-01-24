using GamerWeb.Dto.Dtos.CommentDtos;
using GamerWeb.Dto.Dtos.NewsDtos;

namespace GamerWeb.Models
{
	public class NewsDetailsViewModel
	{
		public ResultNewsDto News { get; set; }
		public List<ResultNewsDto> Last4News { get; set; }
        public List<ResultCommentDto> Comments { get; set; }
    }
}
