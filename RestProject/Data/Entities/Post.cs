using RestProject.Auth.Model;
using System.ComponentModel.DataAnnotations;

namespace RestProject.Data.Entities
{
    public class Post : IUserOwnedResource
    {

        public int Id { get; set; }

        public string Name { get; set; }

        public string Body { get; set; }

        public DateTime CreationDate { get; set; }


        public Topic Topic { get; set; }

        [Required]
        public string UserId { get; set; }

        public ForumRestUser User { get; set; }
    }
}
