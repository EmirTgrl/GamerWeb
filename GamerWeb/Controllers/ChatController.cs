using GamerWeb.DataAccess.Context;
using Microsoft.AspNetCore.Mvc;

namespace GamerWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;
        private readonly string _apiKey;
        private readonly GamerWebContext _context;

        public ChatController(IHttpClientFactory httpClientFactory, IConfiguration configuration, GamerWebContext context)
        {
            _httpClient = httpClientFactory.CreateClient();
            _baseUrl = configuration["GroqSettings:BaseUrl"];
            _apiKey = configuration["GroqSettings:ApiKey"];
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        // SendMessage Action
        [HttpPost("send")]
        public async Task<IActionResult> SendMessage([FromBody] MessageDto messageDto)
        {
            if (string.IsNullOrEmpty(messageDto.Message))
            {
                return BadRequest(new { message = "Mesaj boş olamaz." });
            }

            string responseMessage = string.Empty;

            var gameName = ExtractGameNameFromMessage(messageDto.Message);

            if (string.IsNullOrEmpty(gameName))
            {
                responseMessage = "Oyun adı bulunamadı. Lütfen mesajınızda oyun adını belirtin.";
            }
            else
            {
                var game = _context.Games.FirstOrDefault(p => p.Name.Contains(gameName));
                if (game != null)
                {
                    var platforms = new List<string>();
                    if (!string.IsNullOrEmpty(game.SteamUrl)) platforms.Add("Steam");
                    if (!string.IsNullOrEmpty(game.EpicUrl)) platforms.Add("Epic Games");
                    if (!string.IsNullOrEmpty(game.PsUrl)) platforms.Add("PlayStation");
                    if (!string.IsNullOrEmpty(game.XboxUrl)) platforms.Add("Xbox");

                    responseMessage = $"Oyun: {game.Name}\n" +
                  $"Açıklama: {game.Description}\n" +
                  $"Çıkış Tarihi: {game.ReleaseDate}\n" +
                  $"Mevcut Platformlar: {string.Join(", ", platforms)}";
                }
                else
                {
                    responseMessage = $"'{gameName}' adında bir oyun bulunamadı.";
                }
            }

            return Ok(new { message = responseMessage });
        }

        private string ExtractGameNameFromMessage(string message)
        {
            message = message.Trim();

            var allGames = _context.Games.Select(g => g.Name).ToList();

            foreach (var game in allGames)
            {
                // Oyun adı, mesaj içinde geçiyorsa
                if (message.Contains(game, StringComparison.OrdinalIgnoreCase))
                {
                    return game;
                }
            }

            return string.Empty;
        }

        public class MessageDto
        {
            public string Message { get; set; }
        }
    }
}
