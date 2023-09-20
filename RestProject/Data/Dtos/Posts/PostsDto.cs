namespace RestProject.Data.Dtos.Posts
{
        public record PostDto(int Id, string Name, string Body, DateTime CreationDate);

        public record CreatePostDto(string Name, string Body);

        public record UpdatePostDto(string Body);
}
