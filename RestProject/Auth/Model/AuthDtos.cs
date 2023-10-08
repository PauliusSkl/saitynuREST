using System.ComponentModel.DataAnnotations;

namespace RestProject.Auth.Model
{
    public record RegisterUserDto([Required] string Username, [EmailAddress][Required] string Email, [Required] string Password);
    public record LoginDto(string Username, string Password);

    public record UserDto(string Id, string Username, string Email);

    public record SuccessfulLoginDto(string AccessToken);
}
