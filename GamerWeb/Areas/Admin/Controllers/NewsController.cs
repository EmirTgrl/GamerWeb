using AutoMapper;
using GamerWeb.Business.Abstract;
using GamerWeb.DataAccess.Context;
using GamerWeb.Dto.Dtos.GameDtos;
using GamerWeb.Dto.Dtos.NewsDtos;
using GamerWeb.Entity.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GamerWeb.Areas.Admin.Controllers
{
    [Authorize(Roles = "admin")]
    [Area("Admin")]
    [Route("Admin/News")]
    public class NewsController : Controller
    {
        private readonly INewsService _newsService;
        private readonly IMapper _mapper;
        private readonly GamerWebContext _context;

        public NewsController(INewsService newsService, IMapper mapper, GamerWebContext context)
        {
            _newsService = newsService;
            _mapper = mapper;
            _context = context;
        }

        [Route("NewsList")]
        public async Task<IActionResult> NewsList()
        {
            var values = await _newsService.TGetListAsync();
            var newsList = _mapper.Map<List<ResultNewsDto>>(values);
            return View(newsList);
        }

        [Route("DeleteNews/{id}")]
        public async Task<IActionResult> DeleteNews(int id)
        {
            await _newsService.TDeleteAsync(id);
            return RedirectToAction("NewsList");
        }

        [Route("CreateNews")]
        [HttpGet]
        public IActionResult CreateNews()
        {
            return View();
        }

        [ValidateAntiForgeryToken]
        [Route("CreateNews")]
        [HttpPost]
        public async Task<IActionResult> CreateNews(CreateNewsDto createNewsDto)
        {
            if (createNewsDto.ImageFile != null && createNewsDto.ImageFile.Length > 0)
            {
                // wwwroot/images dizinini oluştur
                var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images");

                // Klasör yoksa oluştur
                if (!Directory.Exists(imagePath))
                    Directory.CreateDirectory(imagePath);

                // Dosya ismini güvenli hale getir
                var fileName = Path.GetFileNameWithoutExtension(createNewsDto.ImageFile.FileName);
                var extension = Path.GetExtension(createNewsDto.ImageFile.FileName);
                var uniqueFileName = $"{fileName}_{Guid.NewGuid()}{extension}";

                // Dosya tam yolunu oluştur
                var filePath = Path.Combine(imagePath, uniqueFileName);

                // Dosyayı belirtilen dizine kaydet
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await createNewsDto.ImageFile.CopyToAsync(stream);
                }

                // Veritabanına kaydedilecek ImageUrl
                createNewsDto.ImageUrl = $"/images/{uniqueFileName}";
            }

            createNewsDto.Date = DateTime.Now;

            var newNews = _mapper.Map<News>(createNewsDto);
            await _newsService.TCreateAsync(newNews);
            TempData["SuccessMessage"] = "Haber başarıyla eklendi.";
            return RedirectToAction("NewsList");
        }

        [Route("UpdateNews/{id}")]
        [HttpGet]
        public async Task<IActionResult> UpdateNews(int id)
        {
            var value = await _newsService.TGetByIdAsync(id);
            var updateNews = _mapper.Map<UpdateNewsDto>(value);
            return View(updateNews);
        }

        [ValidateAntiForgeryToken]
        [Route("UpdateNews/{id}")]
        [HttpPost]
        public async Task<IActionResult> UpdateNews(UpdateNewsDto updateNewsDto)
        {
            var newsFind = await _newsService.TGetByIdAsync(updateNewsDto.NewsId);

            // Eğer güncelleme yapılacaksa eski nesneyi izlemekten çıkart
            _context.Entry(newsFind).State = EntityState.Detached;

            if (updateNewsDto.ImageFile != null && updateNewsDto.ImageFile.Length > 0)
            {
                // Eski resmi silmek için
                if (!string.IsNullOrEmpty(newsFind.ImageUrl))
                {
                    var oldImagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", newsFind.ImageUrl.TrimStart('/'));
                    if (System.IO.File.Exists(oldImagePath))
                    {
                        System.IO.File.Delete(oldImagePath);
                    }
                }

                // Yeni resmi yüklemek için
                var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images");
                if (!Directory.Exists(imagePath))
                    Directory.CreateDirectory(imagePath);

                var fileName = Path.GetFileNameWithoutExtension(updateNewsDto.ImageFile.FileName);
                var extension = Path.GetExtension(updateNewsDto.ImageFile.FileName);
                var uniqueFileName = $"{fileName}_{Guid.NewGuid()}{extension}";
                var filePath = Path.Combine(imagePath, uniqueFileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await updateNewsDto.ImageFile.CopyToAsync(stream);
                }

                updateNewsDto.ImageUrl = $"/images/{uniqueFileName}";  // Yeni resmin yolu
            }
            else
            {
                updateNewsDto.ImageUrl = newsFind.ImageUrl;  // Yeni resim seçilmemişse eski resim kullanılacak
            }

            // Güncelleme işlemi
            var news = _mapper.Map<News>(updateNewsDto);
            _context.Entry(news).State = EntityState.Modified; // Güncellemeyi işaretle
            await _newsService.TUpdateAsync(news);
            TempData["SuccessMessage"] = "Haber başarıyla güncellendi.";
            return RedirectToAction("NewsList");
        }

    }
}
