using AutoMapper;
using GamerWeb.Business.Abstract;
using GamerWeb.Dto.Dtos.CommentDtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GamerWeb.Areas.Admin.Controllers
{
    [Authorize(Roles = "admin")]
    [Area("Admin")]
    [Route("Admin/AdminComment")]
    public class AdminCommentController : Controller
    {
        private readonly ICommentService _commentService;
        private readonly INewsService _newsService;
        private readonly IReviewService _reviewService;
        private readonly IMapper _mapper;

        public AdminCommentController(ICommentService commentService, IMapper mapper, INewsService newsService, IReviewService reviewService)
        {
            _commentService = commentService;
            _mapper = mapper;
            _newsService = newsService;
            _reviewService = reviewService;
        }

        [Route("CommentList")]
        public async Task<IActionResult> CommentList()
        {
            var comments = await _commentService.TGetListAsync();
            var commentDtos = new List<ResultCommentDto>();

            foreach (var comment in comments)
            {
                var commentDto = new ResultCommentDto
                {
                    CommentId = comment.CommentId,
                    Name = comment.Name,
                    Content = comment.Content,
                    Date = comment.Date,
                    NewsId = comment.NewsId,
                    ReviewId = comment.ReviewId,
                    IsApproved = comment.IsApproved // Onay durumunu kontrol etmek için
                };

                if (comment.NewsId != null)
                {
                    var news = await _newsService.TGetByIdAsync(comment.NewsId.Value);
                    commentDto.NewsTitle = news?.Title;
                }
                else if (comment.ReviewId != null)
                {
                    var review = await _reviewService.TGetByIdAsync(comment.ReviewId.Value);
                    commentDto.ReviewTitle = review?.Title;
                }

                commentDtos.Add(commentDto);
            }

            return View(commentDtos);
        }

        [Route("DeleteComment/{id}")]
        public async Task<IActionResult> DeleteComment(int id)
        {
            var comment = await _commentService.TGetByIdAsync(id);
            if (comment != null)
            {
                await _commentService.TDeleteAsync(comment.CommentId);
            }
            return RedirectToAction("CommentList");
        }

        [Route("CommentDetails/{id}")]
        public async Task<IActionResult> CommentDetails(int id)
        {
            var comment = await _commentService.TGetByIdAsync(id);
            if (comment == null)
            {
                return NotFound();
            }

            var commentDto = _mapper.Map<ResultCommentDto>(comment);
            return View(commentDto);
        }

        [Route("UnapprovedCommentList")]
        public async Task<IActionResult> UnapprovedCommentList()
        {
            var comments = await _commentService.TGetUnapprovedCommentsAsync();
            var commentDtos = comments.Select(comment => new ResultCommentDto
            {
                CommentId = comment.CommentId,
                Name = comment.Name,
                Content = comment.Content,
                Date = comment.Date,
                NewsId = comment.NewsId,
                ReviewId = comment.ReviewId,
                IsApproved = comment.IsApproved
            }).ToList();

            return View(commentDtos);
        }

        [HttpPost]
        [Route("ApproveComment/{id}")]
        public async Task<IActionResult> ApproveComment(int id)
        {
            try
            {
                var comment = await _commentService.TGetByIdAsync(id);
                if (comment == null)
                {
                    return NotFound(new { message = "Yorum bulunamadı." });
                }

                await _commentService.ApproveCommentAsync(id);
                return Ok(new { message = "Yorum başarıyla onaylandı." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Yorum onaylanırken bir hata oluştu.", details = ex.Message });
            }
        }

    }
}
