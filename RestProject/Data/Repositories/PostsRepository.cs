using Microsoft.EntityFrameworkCore;
using RestProject.Data.Dtos.Posts;
using RestProject.Data.Entities;
using RestProject.Helpers;

namespace RestProject.Data.Repositories
{
    public interface IPostsRepository
    {
        Task CreateAsync(Post post);
        Task<IEnumerable<Post>> GetManyAsync(int topicId);
        Task<PagedList<Post>> GetManyAsync(int topicId, PostSearchParameters searchParameters);

        Task<Post?> GetAsync(int postId, int topicId);

        Task DeleteAsync(Post post);

        Task UpdateAsync(Post post);

    }

    public class PostsRepository : IPostsRepository
    {

        private readonly ForumDbContext _forumDbContext;

        public PostsRepository(ForumDbContext forumDbContext)
        {
            _forumDbContext = forumDbContext;
        }

        public async Task<IEnumerable<Post>> GetManyAsync(int topicId)
        {
            return await _forumDbContext.Posts.Where(o => o.Topic.Id == topicId).ToListAsync();
        }

        public async Task<PagedList<Post>> GetManyAsync(int topicId, PostSearchParameters searchParameters)
        {
            var queryable = _forumDbContext.Posts.Where(o => o.Topic.Id == topicId).AsQueryable().OrderBy(o => o.CreationDate);

            return await PagedList<Post>.CreateAsync(queryable, searchParameters.PageNumber, searchParameters.PageSize);
        }


        public async Task CreateAsync(Post post)
        {
            _forumDbContext.Posts.Add(post);
            await _forumDbContext.SaveChangesAsync();
        }

        public async Task<Post?> GetAsync(int postId, int topicId)
        {
            return await _forumDbContext.Posts.FirstOrDefaultAsync(o => o.Id == postId && o.Topic.Id == topicId);
        }

        public async Task DeleteAsync(Post post)
        {
            _forumDbContext.Posts.Remove(post);
            await _forumDbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(Post post)
        {
            _forumDbContext.Posts.Update(post);
            await _forumDbContext.SaveChangesAsync();
        }
    }
}
