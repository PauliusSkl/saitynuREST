using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.JsonWebTokens;
using RestProject.Auth.Model;
using RestProject.Data;
using RestProject.Data.Dtos.Comments;
using RestProject.Data.Dtos.Posts;
using RestProject.Data.Entities;
using RestProject.Data.Repositories;
using System.Security.Claims;
using System.Text.Json;

namespace RestProject.Controllers
{
    [ApiController]
    [Route("api/topics/{topicId}/posts/{postId}/comments")]
    public class CommentsController: ControllerBase
    {
        private readonly ICommentsRepository _commentsRepository;
        private readonly IPostsRepository _postsRepository;
        private readonly IAuthorizationService _authorizationService;

        public CommentsController(ICommentsRepository commentsRepository, IPostsRepository postsRepository, IAuthorizationService authorizationService)
        {
            _commentsRepository = commentsRepository;
            _postsRepository = postsRepository;
            _authorizationService = authorizationService;
        }

        [HttpGet(Name = "GetComments")]
        public async Task<IEnumerable<CommentDto>> GetManyPaging(int topicId, int postId, [FromQuery] CommentSearchParameters searchParameters)
        {
            var comments = await _commentsRepository.GetManyAsync(topicId, postId, searchParameters);

            var previousPageLink = comments.HasPrevious ? CreateCommentResourceUri(searchParameters, ResourceUriType.PreviousPage) : null;

            var nextPageLink = comments.HasNext ? CreateCommentResourceUri(searchParameters, ResourceUriType.NextPage) : null;

            var paginationMetadata = new
            {
                totalCount = comments.TotalCount,
                pageSize = comments.PageSize,
                currentPage = comments.CurrentPage,
                totalPages = comments.TotalPages,
                previousPageLink,
                nextPageLink
            };

            Response.Headers.Add("Pagination", JsonSerializer.Serialize(paginationMetadata));

            return comments.Select(o => new CommentDto(o.Id, o.Content, o.CreationDate));
        }


        //[HttpGet(Name = "GetComments")]
        //public async Task<IEnumerable<CommentDto>> GetMany(int topicId, int postId)
        //{
        //    var comments = await _commentsRepository.GetManyAsync(topicId, postId);

        //    return comments.Select(o => new CommentDto(o.Id, o.Content, o.CreationDate));
        //}

        [HttpPost(Name ="CreateComment")]
        [Authorize(Roles = ForumRoles.registeredUser)]
        public async Task<ActionResult<CommentDto>> Create(int topicId, int postId, CreateCommentDto createCommentDto)
        {
 
            var post = await _postsRepository.GetAsync(postId, topicId);
            if (post == null)
            {
                return NotFound();
            }

            var comment = new Comment { Content = createCommentDto.Content, CreationDate = DateTime.UtcNow,
                Post = post,
                UserId = User.FindFirstValue(JwtRegisteredClaimNames.Sub)
            };

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

            var links = CreateLinksForComments(topicId, postId, comment.Id);

            var commentDto = new CommentDto(comment.Id, comment.Content, comment.CreationDate);

            return Ok(new { Resource = commentDto, Links = links });
        }

        [HttpPut("{commentId}", Name ="UpdateComment")]
        [Authorize(Roles = ForumRoles.registeredUser)]
        public async Task<ActionResult<CommentDto>> Update(int topicId, int postId, int commentId, UpdateCommentDto updateCommentDto)
        {
            var comment = await _commentsRepository.GetAsync(topicId, postId, commentId);

            if (comment == null)
            {
                return NotFound();
            }

            var authorizationResult = await _authorizationService.AuthorizeAsync(User, comment, PolicyNames.ResourceOwner);
            if (!authorizationResult.Succeeded)
            {
                return Forbid();
            }

            comment.Content = updateCommentDto.Content;
            comment.CreationDate = DateTime.UtcNow;

            await _commentsRepository.UpdateAsync(comment);

            return new CommentDto(comment.Id, comment.Content, comment.CreationDate);
        }

        [HttpDelete("{commentId}", Name ="DeleteComment")]
        [Authorize(Roles = ForumRoles.registeredUser)]
        public async Task<ActionResult> Remove(int topicId, int postId, int commentId)
        {
            var comment = await _commentsRepository.GetAsync(topicId, postId, commentId);

            if (comment == null)
            {
                return NotFound();
            }
            var authorizationResult = await _authorizationService.AuthorizeAsync(User, comment, PolicyNames.ResourceOwner);
            if (!authorizationResult.Succeeded)
            {
                
                return Forbid();
            }
            await _commentsRepository.DeleteAsync(comment);

            return NoContent();
        }


        private IEnumerable<LinkDto> CreateLinksForComments(int topicId, int postId, int commentId)
        {
            yield return new LinkDto { Href = Url.Link("GetComment", new { topicId, postId, commentId }), Rel = "self", Method = "GET" };
            yield return new LinkDto { Href = Url.Link("UpdateComment", new { topicId, postId, commentId }), Rel = "UpdateComment", Method = "PUT" };
            yield return new LinkDto { Href = Url.Link("DeleteComment", new { topicId, postId, commentId }), Rel = "DeleteComment", Method = "DELETE" };

        }

        private string? CreateCommentResourceUri(CommentSearchParameters searchParameters, ResourceUriType type)
        {
            switch(type)
            {
                case ResourceUriType.PreviousPage:
                    return Url.Link("GetComments", new 
                    {
                        pageNumber = searchParameters.PageNumber - 1,
                        pageSize = searchParameters.PageSize 
                    });
                case ResourceUriType.NextPage:
                    return Url.Link("GetComments", new 
                    {
                        pageNumber = searchParameters.PageNumber + 1,
                        pageSize = searchParameters.PageSize
                    });
                default:
                    return null;
            }
        }
    }
}
