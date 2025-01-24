using AutoMapper;
using GamerWeb.Business.Abstract;
using GamerWeb.Dto.Dtos.GameDtos;
using GamerWeb.Entity.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GamerWeb.Areas.Admin.Controllers
{
    [Authorize(Roles = "admin")]
    [Area("Admin")]
    [Route("Admin/AdminGame")]
    public class AdminGameController : Controller
    {
        private readonly IGameService _gameService;
        private readonly IMapper _mapper;

        public AdminGameController(IGameService gameService, IMapper mapper)
        {
            _gameService = gameService;
            _mapper = mapper;
        }

        [Route("GameList")]
        public async Task<IActionResult> GameList()
        {
            var values = await _gameService.TGetListAsync();
            var gameList = _mapper.Map<List<ResultGameDto>>(values);
            return View(gameList);
        }

        [Route("DeleteGame/{id}")]
        public async Task<IActionResult> DeleteGame(int id)
        {
            await _gameService.TDeleteAsync(id);
            return RedirectToAction("GameList");
        }

        [Route("CreateGame")]
        [HttpGet]
        public IActionResult CreateGame()
        {
            return View();
        }

        [ValidateAntiForgeryToken]
        [Route("CreateGame")]
        [HttpPost]
        public async Task<IActionResult> CreateGame(CreateGameDto createGameDto)
        {
            if (createGameDto.ImageFile != null && createGameDto.ImageFile.Length > 0)
            {
                // wwwroot/images dizinini oluştur
                var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images");

                // Klasör yoksa oluştur
                if (!Directory.Exists(imagePath))
                    Directory.CreateDirectory(imagePath);

                // Dosya ismini güvenli hale getir
                var fileName = Path.GetFileNameWithoutExtension(createGameDto.ImageFile.FileName);
                var extension = Path.GetExtension(createGameDto.ImageFile.FileName);
                var uniqueFileName = $"{fileName}_{Guid.NewGuid()}{extension}";

                // Dosya tam yolunu oluştur
                var filePath = Path.Combine(imagePath, uniqueFileName);

                // Dosyayı belirtilen dizine kaydet
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await createGameDto.ImageFile.CopyToAsync(stream);
                }

                // Veritabanına kaydedilecek ImageUrl
                createGameDto.ImageUrl = $"/images/{uniqueFileName}";
            }

            // Veritabanına yeni oyunu kaydet
            var newGame = _mapper.Map<Game>(createGameDto);
            await _gameService.TCreateAsync(newGame);
            TempData["SuccessMessage"] = "Oyun başarıyla eklendi.";
            return RedirectToAction("GameList");
        }

        [Route("UpdateGame/{id}")]
        [HttpGet]
        public async Task<IActionResult> UpdateGame(int id)
        {
            var value = await _gameService.TGetByIdAsync(id);
            var updateGame = _mapper.Map<UpdateGameDto>(value);
            return View(updateGame);
        }

        [ValidateAntiForgeryToken]
        [Route("UpdateGame/{id}")]
        [HttpPost]
        public async Task<IActionResult> UpdateGame(UpdateGameDto updateGameDto)
        {
            var game = await _gameService.TGetByIdAsync(updateGameDto.GameId);
            if (updateGameDto.ImageFile != null && updateGameDto.ImageFile.Length > 0)
            {
                // Eski resmi silmek için
                if (!string.IsNullOrEmpty(game.ImageUrl))
                {
                    var oldImagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", game.ImageUrl.TrimStart('/'));
                    if (System.IO.File.Exists(oldImagePath))
                    {
                        System.IO.File.Delete(oldImagePath);
                    }
                }

                // Yeni resmi yüklemek için
                var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images");
                if (!Directory.Exists(imagePath))
                    Directory.CreateDirectory(imagePath);

                var fileName = Path.GetFileNameWithoutExtension(updateGameDto.ImageFile.FileName);
                var extension = Path.GetExtension(updateGameDto.ImageFile.FileName);
                var uniqueFileName = $"{fileName}_{Guid.NewGuid()}{extension}";
                var filePath = Path.Combine(imagePath, uniqueFileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await updateGameDto.ImageFile.CopyToAsync(stream);
                }

                updateGameDto.ImageUrl = $"/images/{uniqueFileName}";  // Yeni resmin yolu
            }
            else
            {
                updateGameDto.ImageUrl = game.ImageUrl;  // Yeni resim seçilmemişse eski resim kullanılacak
            }

            var updatedGame = _mapper.Map(updateGameDto, game);
            await _gameService.TUpdateAsync(updatedGame);
            TempData["SuccessMessage"] = "Oyun başarıyla güncellendi.";
            return RedirectToAction("GameList");
        }
    }
}
