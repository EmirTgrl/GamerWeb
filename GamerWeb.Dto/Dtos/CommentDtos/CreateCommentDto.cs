namespace GamerWeb.Dto.Dtos.CommentDtos
{
    public class CreateCommentDto
    {
        public string Name { get; set; }
        public string Content { get; set; }
        public int? NewsId { get; set; }
        public int? ReviewId { get; set; }
    }
}
