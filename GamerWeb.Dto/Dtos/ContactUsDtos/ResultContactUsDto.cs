﻿namespace GamerWeb.Dto.Dtos.ContactUsDtos
{
	public class ResultContactUsDto
	{
		public int ContactUsId { get; set; }
		public string Mail { get; set; }
		public string Subject { get; set; }
		public string Body { get; set; }
		public DateTime Date { get; set; }
	}
}
