using AutoMapper;
using GamerWeb.Business.Abstract;
using GamerWeb.Dto.Dtos.CommentDtos;
using GamerWeb.Entity.Entities;
using Microsoft.AspNetCore.Mvc;

namespace GamerWeb.Controllers
{
    public class CommentController : Controller
    {
        private readonly ICommentService _commentService;
        private readonly IMapper _mapper;

        public CommentController(ICommentService commentService, IMapper mapper)
        {
            _commentService = commentService;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateCommentDto createCommentDto)
        {
            if (ModelState.IsValid)
            {
                var comment = _mapper.Map<Comment>(createCommentDto);
                await _commentService.TCreateAsync(comment);

                // Yorum gönderildikten sonra detay sayfasına yönlendiriyoruz
                if (createCommentDto.ReviewId.HasValue)
                {
                    return RedirectToAction("ReviewDetails", "Review", new { id = createCommentDto.ReviewId });
                }
                else if (createCommentDto.NewsId.HasValue)
                {
                    return RedirectToAction("NewsDetails", "News", new { id = createCommentDto.NewsId });
                }
            }
            return View(createCommentDto);
        }
    }
}
