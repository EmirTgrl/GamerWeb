namespace GamerWeb.Dto.Dtos.CommentDtos
{
    public class ResultCommentDto
    {
        public int CommentId { get; set; }
        public string Name { get; set; }
        public string Content { get; set; }
        public int? NewsId { get; set; }
        public int? ReviewId { get; set; }
        public string? NewsTitle { get; set; }
        public string? ReviewTitle { get; set; }
        public DateTime Date { get; set; }
        public bool IsApproved { get; set; }
    }
}
