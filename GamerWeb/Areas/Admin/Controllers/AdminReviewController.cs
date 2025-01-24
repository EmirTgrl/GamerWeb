using AutoMapper;
using GamerWeb.Business.Abstract;
using GamerWeb.Dto.Dtos.ReviewDtos;
using GamerWeb.Entity.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GamerWeb.Areas.Admin.Controllers
{
    [Authorize(Roles = "admin")]
    [Area("Admin")]
    [Route("Admin/AdminReview")]
    public class AdminReviewController : Controller
    {
        private readonly IReviewService _reviewService;
        private readonly IMapper _mapper;

        public AdminReviewController(IReviewService reviewService, IMapper mapper)
        {
            _reviewService = reviewService;
            _mapper = mapper;
        }

        [Route("ReviewList")]
        public async Task<IActionResult> ReviewList()
        {
            var values = await _reviewService.TGetListAsync();
            var reviewList = _mapper.Map<List<ResultReviewDto>>(values);
            return View(reviewList);
        }

        [Route("DeleteReview/{id}")]
        public async Task<IActionResult> DeleteReview(int id)
        {
            await _reviewService.TDeleteAsync(id);
            return RedirectToAction("ReviewList");
        }

        [Route("CreateReview")]
        [HttpGet]
        public IActionResult CreateReview()
        {
            return View();
        }

        [ValidateAntiForgeryToken]
        [Route("CreateReview")]
        [HttpPost]
        public async Task<IActionResult> CreateReview(CreateReviewDto createReviewDto)
        {
            string SaveImageFile(IFormFile imageFile)
            {
                if (imageFile != null)
                {
                    // Benzersiz dosya adı oluştur
                    var uniqueFileName = Guid.NewGuid().ToString() + "_" + imageFile.FileName;

                    // wwwroot/images/reviews klasörüne dosya yolu oluştur
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot/images", uniqueFileName);

                    // Dosyayı klasöre kaydet
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        imageFile.CopyTo(stream);
                    }

                    // Resim URL'sini döndür
                    return $"/images/{uniqueFileName}";
                }

                return null;
            }

            // TopImage ve GameImage dosyalarını kaydet
            if (createReviewDto.TopImageFile != null)
            {
                createReviewDto.TopImage = SaveImageFile(createReviewDto.TopImageFile);
            }

            if (createReviewDto.GameImageFile != null)
            {
                createReviewDto.GameImage = SaveImageFile(createReviewDto.GameImageFile);
            }

            createReviewDto.Date = DateTime.Now;

            var newReview = _mapper.Map<Review>(createReviewDto);
            await _reviewService.TCreateAsync(newReview);
            TempData["SuccessMessage"] = "İnceleme başarıyla eklendi.";
            return RedirectToAction("ReviewList");
        }

        [Route("UpdateReview/{id}")]
        [HttpGet]
        public async Task<IActionResult> UpdateReview(int id)
        {
            var value = await _reviewService.TGetByIdAsync(id);
            var updateReview = _mapper.Map<UpdateReviewDto>(value);
            return View(updateReview);
        }

        [ValidateAntiForgeryToken]
        [Route("UpdateReview/{id}")]
        [HttpPost]
        public async Task<IActionResult> UpdateReview(UpdateReviewDto updateReviewDto)
        {
            // Veritabanından mevcut veriyi al
            var review = await _reviewService.TGetByIdAsync(updateReviewDto.ReviewId);

            // TopImage güncelleme işlemi
            if (updateReviewDto.TopImageFile != null && updateReviewDto.TopImageFile.Length > 0)
            {
                // Eski TopImage silinir
                if (!string.IsNullOrEmpty(review.TopImage))
                {
                    var oldTopImagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", review.TopImage.TrimStart('/'));
                    if (System.IO.File.Exists(oldTopImagePath))
                    {
                        System.IO.File.Delete(oldTopImagePath);
                    }
                }

                // Yeni TopImage yüklenir
                var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images");
                if (!Directory.Exists(imagePath))
                    Directory.CreateDirectory(imagePath);

                var topImageFileName = Path.GetFileNameWithoutExtension(updateReviewDto.TopImageFile.FileName);
                var topImageExtension = Path.GetExtension(updateReviewDto.TopImageFile.FileName);
                var uniqueTopImageName = $"{topImageFileName}_{Guid.NewGuid()}{topImageExtension}";
                var topImagePath = Path.Combine(imagePath, uniqueTopImageName);

                using (var stream = new FileStream(topImagePath, FileMode.Create))
                {
                    await updateReviewDto.TopImageFile.CopyToAsync(stream);
                }

                updateReviewDto.TopImage = $"/images/{uniqueTopImageName}";  // Yeni TopImage yolu
            }
            else
            {
                updateReviewDto.TopImage = review.TopImage;  // Yeni resim seçilmemişse eski TopImage kullanılacak
            }

            // GameImage güncelleme işlemi
            if (updateReviewDto.GameImageFile != null && updateReviewDto.GameImageFile.Length > 0)
            {
                // Eski GameImage silinir
                if (!string.IsNullOrEmpty(review.GameImage))
                {
                    var oldGameImagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", review.GameImage.TrimStart('/'));
                    if (System.IO.File.Exists(oldGameImagePath))
                    {
                        System.IO.File.Delete(oldGameImagePath);
                    }
                }

                // Yeni GameImage yüklenir
                var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images");
                if (!Directory.Exists(imagePath))
                    Directory.CreateDirectory(imagePath);

                var gameImageFileName = Path.GetFileNameWithoutExtension(updateReviewDto.GameImageFile.FileName);
                var gameImageExtension = Path.GetExtension(updateReviewDto.GameImageFile.FileName);
                var uniqueGameImageName = $"{gameImageFileName}_{Guid.NewGuid()}{gameImageExtension}";
                var gameImagePath = Path.Combine(imagePath, uniqueGameImageName);

                using (var stream = new FileStream(gameImagePath, FileMode.Create))
                {
                    await updateReviewDto.GameImageFile.CopyToAsync(stream);
                }

                updateReviewDto.GameImage = $"/images/{uniqueGameImageName}";  // Yeni GameImage yolu
            }
            else
            {
                updateReviewDto.GameImage = review.GameImage;  // Yeni resim seçilmemişse eski GameImage kullanılacak
            }

            // Veritabanı güncellemesi
            var updatedReview = _mapper.Map(updateReviewDto, review);
            await _reviewService.TUpdateAsync(updatedReview);
            TempData["SuccessMessage"] = "İnceleme başarıyla güncellendi.";
            return RedirectToAction("ReviewList");
        }
    }
}
