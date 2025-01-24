using AutoMapper;
using GamerWeb.Business.Abstract;
using GamerWeb.Dto.Dtos.CommentDtos;
using GamerWeb.Dto.Dtos.NewsDtos;
using GamerWeb.Models;
using Microsoft.AspNetCore.Mvc;
using X.PagedList.Extensions;

namespace GamerWeb.Controllers
{
    public class NewsController : Controller
    {
        private readonly INewsService _newsService;
        private readonly ICommentService _commentService;
        private readonly IMapper _mapper;

        public NewsController(INewsService newsService, IMapper mapper, ICommentService commentService)
        {
            _newsService = newsService;
            _mapper = mapper;
            _commentService = commentService;
        }

        public async Task<IActionResult> Index(int page = 1)
        {
            var values = await _newsService.TGetListAsync();
            var valueList = _mapper.Map<List<ResultNewsDto>>(values);
            int pageSize = 10;
            var pagedList = valueList.ToPagedList(page, pageSize);
            return View(pagedList);
        }

        public async Task<IActionResult> NewsDetails(int id)
        {
            var value = await _newsService.TGetByIdAsync(id);
            var newsValue = _mapper.Map<ResultNewsDto>(value);

            var last4News = await _newsService.TGetLast4NewsAsync();
            var last4NewsDto = _mapper.Map<List<ResultNewsDto>>(last4News);

            var comments = await _commentService.TGetCommentsByNewsIdAsync(id);
            var commentsDto = _mapper.Map<List<ResultCommentDto>>(comments);

            var viewModel = new NewsDetailsViewModel
            {
                News = newsValue,
                Last4News = last4NewsDto,
                Comments = commentsDto
            };

            return View(viewModel);
        }
    }
}
