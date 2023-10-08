using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RestProject.Auth;
using RestProject.Auth.Model;

namespace RestProject.Controllers
{

    [ApiController]
    [AllowAnonymous]
    [Route("api")]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<ForumRestUser> _userManager;
        private readonly IJwtTokenService _jwtTokenService;

        public AuthController(UserManager<ForumRestUser> userManager, IJwtTokenService jwtTokenService)
        {
            _userManager = userManager;
            _jwtTokenService = jwtTokenService;
        }


        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterUserDto registerUserDto)
        {
            var user = await _userManager.FindByNameAsync(registerUserDto.Username);
            if (user != null) 
            {
                return BadRequest("User already exists");
            }

            var newUser = new ForumRestUser
            {
                UserName = registerUserDto.Username,
                Email = registerUserDto.Email
            };

            var createdUserResult = await _userManager.CreateAsync(newUser, registerUserDto.Password);
            if (!createdUserResult.Succeeded)
            {
                return BadRequest(createdUserResult.Errors + "Could not create a user");
            }

            await _userManager.AddToRoleAsync(newUser, ForumRoles.registeredUser);

            return CreatedAtAction(nameof(Register), new UserDto(newUser.Id, newUser.UserName, newUser.Email));

        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
           
            var user = await _userManager.FindByNameAsync(loginDto.Username);
            if (user == null)
            {
                return BadRequest("User name or password is invalid");
            }

            var isPasswordValid = await _userManager.CheckPasswordAsync(user, loginDto.Password);
            if (!isPasswordValid)
            {
                return BadRequest("User name or password is invalid");
            }

            var roles = await _userManager.GetRolesAsync(user);

            var accessToken = _jwtTokenService.CreateAccessToken(user.UserName, user.Id, roles);

            return Ok(new SuccessfulLoginDto(accessToken));
        }


    }
}
