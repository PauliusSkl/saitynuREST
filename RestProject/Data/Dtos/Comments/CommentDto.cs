namespace RestProject.Data.Dtos.Comments
{
    public record CommentDto(int Id, string Content, DateTime CreationDate);

    public record CreateCommentDto(string Content);

    public record UpdateCommentDto(string Content);
}
