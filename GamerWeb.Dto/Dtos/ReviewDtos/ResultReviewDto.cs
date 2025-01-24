﻿namespace GamerWeb.Dto.Dtos.ReviewDtos
{
	public class ResultReviewDto
	{
		public int ReviewId { get; set; }
		public string TopImage { get; set; }
		public string GameImage { get; set; }
		public string GameName { get; set; }
		public string Title { get; set; }
		public string Subtitle { get; set; }
		public string Description { get; set; }
		public string Rating { get; set; }
		public DateTime Date { get; set; }
	}
}
