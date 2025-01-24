using AutoMapper;
using GamerWeb.Business.Abstract;
using GamerWeb.Dto.Dtos.CommentDtos;
using GamerWeb.Dto.Dtos.ReviewDtos;
using GamerWeb.Models;
using Microsoft.AspNetCore.Mvc;
using X.PagedList.Extensions;

namespace GamerWeb.Controllers
{
    public class ReviewController : Controller
    {
        private readonly IReviewService _reviewService;
        private readonly ICommentService _commentService;
        private readonly IMapper _mapper;

        public ReviewController(IReviewService reviewService, IMapper mapper, ICommentService commentService)
        {
            _reviewService = reviewService;
            _mapper = mapper;
            _commentService = commentService;
        }

        public async Task<IActionResult> Index(int page = 1)
        {
            var values = await _reviewService.TGetListAsync();
            var valueList = _mapper.Map<List<ResultReviewDto>>(values);
            int pageSize = 10;
            var pagedList = valueList.ToPagedList(page, pageSize);
            return View(pagedList);
        }

        public async Task<IActionResult> ReviewDetails(int id)
        {
            var value = await _reviewService.TGetByIdAsync(id);
            var reviewValue = _mapper.Map<ResultReviewDto>(value);

            var last4Reviews = await _reviewService.TGetLast4ReviewsAsync();
            var last4ReviewsDto = _mapper.Map<List<ResultReviewDto>>(last4Reviews);

            var comments = await _commentService.TGetCommentsByReviewIdAsync(id);
            var commentsDto = _mapper.Map<List<ResultCommentDto>>(comments);

            var viewModel = new ReviewDetailsViewModel
            {
                Review = reviewValue,
                Last4Reviews = last4ReviewsDto,
                Comments = commentsDto
            };

            return View(viewModel);
        }
    }
}
