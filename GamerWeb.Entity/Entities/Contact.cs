using System.ComponentModel.DataAnnotations;

namespace GamerWeb.Entity.Entities
{
	public class Contact
	{
		[Key]
		public string Description { get; set; }
		public string Mail { get; set; }
		public string Address { get; set; }

	}
}
