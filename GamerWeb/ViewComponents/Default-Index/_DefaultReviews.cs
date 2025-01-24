using AutoMapper;
using GamerWeb.Business.Abstract;
using GamerWeb.Dto.Dtos.ReviewDtos;
using Microsoft.AspNetCore.Mvc;

namespace GamerWeb.ViewComponents.Default_Index
{
    public class _DefaultReviews : ViewComponent
    {
        private readonly IReviewService _reviewService;
        private readonly IMapper _mapper;

        public _DefaultReviews(IReviewService reviewService, IMapper mapper)
        {
            _reviewService = reviewService;
            _mapper = mapper;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var last4Reviews = await _reviewService.TGetLast4ReviewsAsync();
            var last4ReviewsDto = _mapper.Map<List<ResultReviewDto>>(last4Reviews);
            return View(last4ReviewsDto);
        }
    }
}
