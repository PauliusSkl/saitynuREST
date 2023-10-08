using Microsoft.AspNetCore.Identity;
using System.Security.Policy;

namespace RestProject.Auth.Model
{
    public class ForumRestUser : IdentityUser
    {
        [PersonalData]
        public string? AdditionalInfo { get; set; }

    }
}
