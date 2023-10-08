using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.JsonWebTokens;
using RestProject.Auth.Model;
using RestProject.Data;
using RestProject.Data.Dtos.Posts;
using RestProject.Data.Entities;
using RestProject.Data.Repositories;
using System.Security.Claims;
using System.Text.Json;

namespace RestProject.Controllers
{
    [ApiController]
    [Route("api/topics/{topicId}/posts")]
    public class PostController : ControllerBase
    {
        private readonly IPostsRepository _postsRepository;
        private readonly ITopicsRepository _topicsRepository;
        private readonly IAuthorizationService _authorizationService;

        public PostController(IPostsRepository postsRepository, ITopicsRepository topicsRepository, IAuthorizationService authorizationService)
        {
            _postsRepository = postsRepository;
            _topicsRepository = topicsRepository;
            _authorizationService = authorizationService;
        }

        //[HttpGet(Name = "GetPosts")]
        //public async Task<IEnumerable<PostDto>> GetMany(int topicId)
        //{
        //    var posts = await _postsRepository.GetManyAsync(topicId);

        //    return posts.Select(o => new PostDto(o.Id, o.Name, o.Body, o.CreationDate));
        //}

        [HttpGet(Name = "GetPosts")]
        public async Task<IEnumerable<PostDto>> GetManyPaging(int topicId, [FromQuery] PostSearchParameters searchParameters)
        {
            var posts = await _postsRepository.GetManyAsync(topicId, searchParameters);

            var previousPageLink = posts.HasPrevious ? CreatePostsResourceUri(searchParameters, ResourceUriType.PreviousPage) : null;

            var nextPageLink = posts.HasNext ? CreatePostsResourceUri(searchParameters, ResourceUriType.NextPage) : null;

            var paginationMetadata = new
            {
                totalCount = posts.TotalCount,
                pageSize = posts.PageSize,
                currentPage = posts.CurrentPage,
                totalPages = posts.TotalPages,
                previousPageLink,
                nextPageLink
            };

            Response.Headers.Add("Pagination", JsonSerializer.Serialize(paginationMetadata));

            return posts.Select(o => new PostDto(o.Id, o.Name, o.Body, o.CreationDate));
        }

        [HttpPost(Name ="CreatePost")]
        [Authorize(Roles = ForumRoles.registeredUser)]
        public async Task<ActionResult<PostDto>> Create(int topicId, CreatePostDto createPostDto)
        {

            var topic = await _topicsRepository.GetAsync(topicId);
            if (topic == null)
            {
                return NotFound();
            }

            var post = new Post { Name = createPostDto.Name, Body = createPostDto.Body, CreationDate = DateTime.Now,
                Topic = topic,
                UserId = User.FindFirstValue(JwtRegisteredClaimNames.Sub)
            };

            await _postsRepository.CreateAsync(post);

            return Created("", new PostDto(post.Id, post.Name, post.Body, post.CreationDate));

        }

        [HttpGet("{postId}", Name = "GetPost")]
        public async Task<ActionResult<PostDto>> GetOne(int topicId, int postId)
        {
            var post = await _postsRepository.GetAsync(postId, topicId);

            if (post == null)
            {
                return NotFound();
            }

            var links = CreateLlinksForPosts(topicId, post.Id);
            var postDto = new PostDto(post.Id, post.Name, post.Body, post.CreationDate);

            return Ok(new { Resource = postDto, Links = links });

            //return new PostDto(post.Id, post.Name, post.Body, post.CreationDate);
        }

        [HttpDelete("{postId}", Name ="DeletePost")]
        public async Task<ActionResult> Remove(int topicId, int postId)
        {
            var post = await _postsRepository.GetAsync(postId, topicId);

            if (post == null)
            {
                return NotFound();
            }

            var authorizationResult = await _authorizationService.AuthorizeAsync(User, post, PolicyNames.ResourceOwner);
            if (!authorizationResult.Succeeded)
            {
                return Forbid();
            }

            await _postsRepository.DeleteAsync(post);

            return NoContent();

        }

        [HttpPut("{postId}", Name ="UpdatePost")]
        [Authorize(Roles = ForumRoles.registeredUser)]
        public async Task<ActionResult<PostDto>> Update(int topicId, int postId, UpdatePostDto updatePostDto)
        {
            var post = await _postsRepository.GetAsync(postId, topicId);

            if (post == null)
            {
                return NotFound();
            }

            var authorizationResult = await _authorizationService.AuthorizeAsync(User, post, PolicyNames.ResourceOwner);
            if (!authorizationResult.Succeeded)
            {
                return Forbid();
            }

            post.Body = updatePostDto.Body;

            await _postsRepository.UpdateAsync(post);

            return Ok(new PostDto(post.Id, post.Name, post.Body, post.CreationDate));
        }

        private IEnumerable<LinkDto> CreateLlinksForPosts(int topicId, int postId)
        {
            yield return new LinkDto { Href = Url.Link("GetPost", new { postId }), Rel = "self", Method = "GET" };
            yield return new LinkDto { Href = Url.Link("DeletePost", new {topicId, postId }), Rel = "DeletePost", Method = "DELETE" };
            yield return new LinkDto { Href = Url.Link("UpdatePost", new { topicId, postId }), Rel = "UpdatePost", Method = "PUT" };
            yield return new LinkDto { Href = Url.Link("GetComments", new { topicId, postId }), Rel = "GetComments", Method = "GET" };
        }

        private string? CreatePostsResourceUri(PostSearchParameters searchParameters, ResourceUriType type)
        {
            switch (type)
            {
                case ResourceUriType.PreviousPage:
                    return Url.Link("GetPosts", new
                    {
                        pageNumber = searchParameters.PageNumber - 1,
                        pageSize = searchParameters.PageSize
                    });
                case ResourceUriType.NextPage:
                    return Url.Link("GetPosts", new
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
