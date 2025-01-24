using System.ComponentModel.DataAnnotations;

namespace GamerWeb.Entity.Entities
{
	public class Game
	{
		[Key]
		public int GameId { get; set; }
		public string Name { get; set; }
		public string Type { get; set; }
        public string Description { get; set; }
        public string ReleaseDate { get; set; }
		public string ImageUrl { get; set; }
		public string? SteamUrl { get; set; }
		public string? EpicUrl { get; set; }
		public string? PsUrl { get; set; }
		public string? XboxUrl { get; set; }
	}
}
