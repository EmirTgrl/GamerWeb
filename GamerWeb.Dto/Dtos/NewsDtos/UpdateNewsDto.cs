using Microsoft.AspNetCore.Http;

namespace GamerWeb.Dto.Dtos.NewsDtos
{
	public class UpdateNewsDto
	{
		public int NewsId { get; set; }
        public IFormFile ImageFile { get; set; } 
        public string ImageUrl { get; set; }
        public string GameName { get; set; }
		public string Title { get; set; }
		public string Description { get; set; }
		public string Subtitle { get; set; }
		public DateTime Date { get; set; }
	}
}
