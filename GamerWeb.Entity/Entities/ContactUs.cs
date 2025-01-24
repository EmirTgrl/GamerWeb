using System.ComponentModel.DataAnnotations;

namespace GamerWeb.Entity.Entities
{
	public class ContactUs
	{
		[Key]
		public int ContactUsId { get; set; }
		public string Mail { get; set; }
		public string Subject { get; set; }
		public string Body { get; set; }
		public DateTime Date { get; set; }
	}
}
