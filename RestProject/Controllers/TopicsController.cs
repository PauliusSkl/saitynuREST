using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestProject.Auth.Model;
using RestProject.Data;
using RestProject.Data.Dtos.Topics;
using RestProject.Data.Entities;
using RestProject.Data.Repositories;
using System.Security.Claims;
using System.Text.Json;
using Microsoft.IdentityModel.JsonWebTokens;

namespace RestProject.Controllers
{
    [ApiController]
    [Route("api/topics")]
    public class TopicsController : ControllerBase
    {
        private readonly ITopicsRepository _topicsRepository;
        private readonly IAuthorizationService _authorizationService;

        public TopicsController(ITopicsRepository topicsRepository, IAuthorizationService authorizationService)
        {
            _topicsRepository = topicsRepository;
            _authorizationService = authorizationService;
        }

        //automapper
        //[HttpGet]
        public async Task<IEnumerable<TopicDto>> GetMany()
        {
            var topics = await _topicsRepository.GetManyAsync();

            return topics.Select(o => new TopicDto(o.Id, o.Name, o.Description, o.CreationDate));

        }


        [HttpGet(Name = "GetTopics")]

        public async Task<IEnumerable<TopicDto>> GetManyPaging([FromQuery] TopicSearchParameters searchParameters)
        {
            var topics = await _topicsRepository.GetManyAsync(searchParameters);

            var previousPageLink = topics.HasPrevious ? CreateTopicsResourceUri(searchParameters, ResourceUriType.PreviousPage) : null;

            var nextPageLink = topics.HasNext ? CreateTopicsResourceUri(searchParameters, ResourceUriType.NextPage) : null;

            var paginationMetadata = new
            {
                totalCount = topics.TotalCount,
                pageSize = topics.PageSize,
                currentPage = topics.CurrentPage,
                totalPages = topics.TotalPages,
                previousPageLink,
                nextPageLink
            };



            Response.Headers.Add("Pagination", JsonSerializer.Serialize(paginationMetadata));

            return topics.Select(o => new TopicDto(o.Id, o.Name, o.Description, o.CreationDate));

        }

        // api/topics/{topicId}
        [HttpGet("{topicId}", Name = "GetTopic")]
        public async Task<IActionResult> Get(int topicId)
        {
            var topic = await _topicsRepository.GetAsync(topicId);

            if (topic == null)
            {
                return NotFound();
            }


            var links = CreateLinksForTopic(topic.Id);
            var topicDto = new TopicDto(topic.Id, topic.Name, topic.Description, topic.CreationDate);
            return Ok(new { Resource = topicDto, Links = links });

            //return new TopicDto(topic.Id, topic.Name, topic.Description, topic.CreationDate);
        }


        [HttpPost(Name ="CreateTopic")]
        [Authorize(Roles = ForumRoles.registeredUser)]
        public async Task<ActionResult<TopicDto>> Create(CreateTopicDto createTopicDto)
        {
            var topic = new Topic { Name = createTopicDto.name, Description = createTopicDto.Description,
                CreationDate = DateTime.UtcNow,
                UserId = User.FindFirstValue(JwtRegisteredClaimNames.Sub)
            };

            await _topicsRepository.CreateAsync(topic);

            return Created("", new TopicDto(topic.Id, topic.Name, topic.Description, topic.CreationDate));

            //return CreatedAtAction("GetTopic", new { topicId = topic.Id}, new TopicDto(topic.Name, topic.Description, topic.CreationDate));
        }

        [HttpPut("{topicId}", Name = "UpdateTopic")]
        [Authorize(Roles = ForumRoles.registeredUser)]
        public async Task<ActionResult<TopicDto>> Update(int topicId, UpdateTopicDto updateTopicDto)
        {

            var topic = await _topicsRepository.GetAsync(topicId);

            if (topic == null)
            {
                return NotFound();
            }

            var authorizationResult = await _authorizationService.AuthorizeAsync(User, topic, PolicyNames.ResourceOwner);
            if (!authorizationResult.Succeeded)
            {
                // 404
                return Forbid();
            }

            topic.Description = updateTopicDto.Description;
            await _topicsRepository.UpdateAsync(topic);

            return Ok(new TopicDto(topic.Id, topic.Name, topic.Description, topic.CreationDate));
        }


        [HttpDelete("{topicId}", Name = "DeleteTopic")]
        [Authorize(Roles = ForumRoles.registeredUser)]
        public async Task<ActionResult> Remove(int topicId)
        {
            var topic = await _topicsRepository.GetAsync(topicId);

            if (topic == null)
            {
                return NotFound();
            }

            var authorizationResult = await _authorizationService.AuthorizeAsync(User, topic, PolicyNames.ResourceOwner);
            if (!authorizationResult.Succeeded)
            {
                return Forbid();
            }


            await _topicsRepository.DeleteAsync(topic);

            return NoContent();
        }

        private IEnumerable<LinkDto> CreateLinksForTopic(int topicID)
        {
            yield return new LinkDto { Href = Url.Link("GetTopic", new { topicId = topicID }), Rel = "self", Method = "GET" };

            yield return new LinkDto { Href = Url.Link("DeleteTopic", new { topicId = topicID }), Rel = "DeleteTopic", Method = "DELETE" };

            yield return new LinkDto { Href = Url.Link("UpdateTopic", new { topicId = topicID }), Rel = "UpdateTopic", Method = "PUT" };

            yield return new LinkDto { Href = Url.Link("GetPosts", new { topicId = topicID }), Rel = "GetPosts", Method = "GET" };

        }

        private string? CreateTopicsResourceUri(TopicSearchParameters topicSearchParameters, ResourceUriType type)
        {
            switch (type)
            {
                case ResourceUriType.PreviousPage:
                    return Url.Link("GetTopics", new
                    {
                        pageNumber = topicSearchParameters.PageNumber - 1,
                        pageSize = topicSearchParameters.PageSize
                    });
                case ResourceUriType.NextPage:
                    return Url.Link("GetTopics", new
                    {
                        pageNumber = topicSearchParameters.PageNumber + 1,
                        pageSize = topicSearchParameters.PageSize
                    });
                default:
                    return Url.Link("GetTopics", new
                    {
                        pageNumber = topicSearchParameters.PageNumber,
                        pageSize = topicSearchParameters.PageSize
                    });
            }
        }   
    }
}
