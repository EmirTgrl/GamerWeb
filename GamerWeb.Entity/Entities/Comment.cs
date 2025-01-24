using System.ComponentModel.DataAnnotations;

namespace GamerWeb.Entity.Entities
{
    public class Comment
    {
        [Key]
        public int CommentId { get; set; }
        public string Name { get; set; }
        public string Content { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;
        public bool IsApproved { get; set; } = false;

        public int? NewsId { get; set; }
        public News News { get; set; }

        public int? ReviewId { get; set; }
        public Review Review { get; set; }
    }
}
