using RestProject.Auth.Model;
using System.ComponentModel.DataAnnotations;

namespace RestProject.Data.Entities
{
    public class Topic : IUserOwnedResource
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime CreationDate { get; set; }

        public DateTime? ExpiresIn { get; set; }

        [Required]
        public string UserId { get; set; }

        public ForumRestUser User { get; set; }


    }
}
