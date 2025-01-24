using AutoMapper;
using GamerWeb.Business.Abstract;
using GamerWeb.Dto.Dtos.GameDtos;
using Microsoft.AspNetCore.Mvc;
using X.PagedList;
using System.Linq;
using X.PagedList.Extensions;

namespace GamerWeb.Controllers
{
    public class GameController : Controller
    {
        private readonly IGameService _gameService;
        private readonly IMapper _mapper;

        public GameController(IGameService gameService, IMapper mapper)
        {
            _gameService = gameService;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index(int page = 1)
        {
            var values = await _gameService.TGetListAsync();
            var valueList = _mapper.Map<List<ResultGameDto>>(values);
            var pagedList = valueList.ToPagedList(page, 10);
            return View(pagedList);
        }

        // Tüm oyunları dönen bir JSON endpoint
        [HttpGet]
        public async Task<JsonResult> GetAllGames()
        {
            var values = await _gameService.TGetListAsync();
            var result = _mapper.Map<List<ResultGameDto>>(values);
            return Json(result);
        }

        // Arama işlevi
        [HttpGet]
        public async Task<JsonResult> Search(string query)
        {
            var values = await _gameService.TGetListAsync();
            var filteredGames = values
                .Where(x => x.Name.Contains(query, StringComparison.OrdinalIgnoreCase))
                .ToList();
            var result = _mapper.Map<List<ResultGameDto>>(filteredGames);
            return Json(result);
        }
    }
}
