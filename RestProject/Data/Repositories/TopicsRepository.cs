using Microsoft.EntityFrameworkCore;
using RestProject.Data.Dtos.Topics;
using RestProject.Data.Entities;
using RestProject.Helpers;

namespace RestProject.Data.Repositories
{
    public interface ITopicsRepository
    {
        Task CreateAsync(Topic topic);
        Task DeleteAsync(Topic topic);
        Task<Topic?> GetAsync(int topicId);
        Task<IEnumerable<Topic>> GetManyAsync();

        Task<PagedList<Topic>> GetManyAsync(TopicSearchParameters topicSearchParameters);
        Task UpdateAsync(Topic topic);
    }

    public class TopicsRepository : ITopicsRepository
    {
        private readonly ForumDbContext _forumDbContext;

        public TopicsRepository(ForumDbContext forumDbContext)
        {
            _forumDbContext = forumDbContext;
        }

        public async Task<Topic?> GetAsync(int topicId)
        {
            return await _forumDbContext.Topics.FirstOrDefaultAsync(o => o.Id == topicId);
        }

        public async Task<IEnumerable<Topic>> GetManyAsync()
        {
            return await _forumDbContext.Topics.ToListAsync();
        }


        public async Task<PagedList<Topic>> GetManyAsync(TopicSearchParameters topicSearchParameters)
        {
            var queryable = _forumDbContext.Topics.AsQueryable().OrderBy(o => o.CreationDate);



            return await PagedList<Topic>.CreateAsync(queryable, topicSearchParameters.PageNumber, topicSearchParameters.PageSize);
        }

        public async Task CreateAsync(Topic topic)
        {
            _forumDbContext.Topics.Add(topic);
            await _forumDbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(Topic topic)
        {
            _forumDbContext.Topics.Update(topic);
            await _forumDbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Topic topic)
        {
            _forumDbContext.Topics.Remove(topic);
            await _forumDbContext.SaveChangesAsync();
        }
    }
}
