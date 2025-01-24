using GamerWeb.Dto.Dtos.CommentDtos;
using GamerWeb.Dto.Dtos.ReviewDtos;

namespace GamerWeb.Models
{
    public class ReviewDetailsViewModel
    {
        public ResultReviewDto Review { get; set; }
        public List<ResultReviewDto> Last4Reviews { get; set; }
        public List<ResultCommentDto> Comments { get; set; }
    }
}
