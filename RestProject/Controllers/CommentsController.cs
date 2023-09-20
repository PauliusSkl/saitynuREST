using Microsoft.AspNetCore.Mvc;
using RestProject.Data.Dtos.Comments;
using RestProject.Data.Dtos.Posts;
using RestProject.Data.Entities;
using RestProject.Data.Repositories;

namespace RestProject.Controllers
{
    [ApiController]
    [Route("api/topics/{topicId}/posts/{postId}/comments")]
    public class CommentsController: ControllerBase
    {
        private readonly ICommentsRepository _commentsRepository;
        private readonly IPostsRepository _postsRepository;
        private readonly ITopicsRepository _topicsRepository;

        public CommentsController(ICommentsRepository commentsRepository, IPostsRepository postsRepository, ITopicsRepository topicsRepository)
        {
            _commentsRepository = commentsRepository;
            _postsRepository = postsRepository;
            _topicsRepository = topicsRepository;
        }

        [HttpGet(Name = "GetComments")]
        public async Task<IEnumerable<CommentDto>> GetMany(int topicId, int postId)
        {
            var comments = await _commentsRepository.GetManyAsync(topicId, postId);

            return comments.Select(o => new CommentDto(o.Id, o.Content, o.CreationDate));
        }

        [HttpPost(Name ="CreateComment")]
        public async Task<ActionResult<CommentDto>> Create(int topicId, int postId, CreateCommentDto createCommentDto)
        {
 
            var post = await _postsRepository.GetAsync(postId, topicId);
            if (post == null)
            {
                return NotFound();
            }

            var comment = new Comment { Content = createCommentDto.Content, CreationDate = DateTime.Now, Post = post };

            await _commentsRepository.CreateAsync(comment);
            return Created("", new CommentDto(comment.Id, comment.Content, comment.CreationDate));
        }

        [HttpGet("{commentId}", Name ="GetComment")]
        public async Task<ActionResult<CommentDto>> GetOne(int topicId, int postId, int commentId)
        {
            var comment = await _commentsRepository.GetAsync(topicId, postId, commentId);

            if (comment == null)
            {
                return NotFound();
            }

            return new CommentDto(comment.Id, comment.Content, comment.CreationDate);
        }

        [HttpPut("{commentId}", Name ="UpdateComment")]
        public async Task<ActionResult<CommentDto>> Update(int topicId, int postId, int commentId, UpdateCommentDto updateCommentDto)
        {
            var comment = await _commentsRepository.GetAsync(topicId, postId, commentId);

            if (comment == null)
            {
                return NotFound();
            }

            comment.Content = updateCommentDto.Content;
            comment.CreationDate = DateTime.Now;

            await _commentsRepository.UpdateAsync(comment);

            return new CommentDto(comment.Id, comment.Content, comment.CreationDate);
        }

        [HttpDelete("{commentId}", Name ="DeleteComment")]
        public async Task<ActionResult> Remove(int topicId, int postId, int commentId)
        {
            var comment = await _commentsRepository.GetAsync(topicId, postId, commentId);

            if (comment == null)
            {
                return NotFound();
            }

            await _commentsRepository.DeleteAsync(comment);

            return NoContent();
        }

    }
}
