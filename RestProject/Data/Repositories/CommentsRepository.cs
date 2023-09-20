using Microsoft.EntityFrameworkCore;
using RestProject.Data.Entities;

namespace RestProject.Data.Repositories
{
    public interface ICommentsRepository
    {
        Task CreateAsync(Comment comment);
        Task<IEnumerable<Comment>> GetManyAsync(int topicId, int postId);

        Task<Comment?> GetAsync(int topicId, int postId, int commentId);

        Task DeleteAsync(Comment comment);

        Task UpdateAsync(Comment comment);
    }

    public class CommentsRepository : ICommentsRepository
    {

        private readonly ForumDbContext _forumDbContext;

        public CommentsRepository(ForumDbContext forumDbContext)
        {
            _forumDbContext = forumDbContext;
        }

        public async Task<IEnumerable<Comment>> GetManyAsync(int topicId, int postId)
        {
            return await _forumDbContext.Comments.Where(o => o.Post.Id == postId && o.Post.Topic.Id == topicId).ToListAsync();
        }

        public async Task CreateAsync(Comment comment)
        {
            _forumDbContext.Comments.Add(comment);
            await _forumDbContext.SaveChangesAsync();
        }

        public async Task<Comment?> GetAsync(int topicId, int postId, int commentId)
        {
            return await _forumDbContext.Comments.FirstOrDefaultAsync(o => o.Id == commentId && o.Post.Id == postId && o.Post.Topic.Id == topicId);
        }

        public async Task DeleteAsync(Comment comment)
        {
            _forumDbContext.Comments.Remove(comment);
            await _forumDbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(Comment comment)
        {
            _forumDbContext.Comments.Update(comment);
            await _forumDbContext.SaveChangesAsync();
        }
    }
}
