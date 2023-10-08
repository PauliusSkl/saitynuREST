using RestProject.Auth.Model;
using System.ComponentModel.DataAnnotations;

namespace RestProject.Data.Entities
{
    public class Comment : IUserOwnedResource
    {
        public int Id { get; set; }

        public string Content { get; set; }

        public DateTime CreationDate { get; set; }


        public Post Post { get; set; }

        [Required]
        public string UserId { get; set; }

        public ForumRestUser User { get; set; }
    }
}
