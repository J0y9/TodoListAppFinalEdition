using AutoMapper;
using Azure.Core;
using Azure.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Server.IIS.Core;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using TodoListAppFinalEdition.Shared;
using TodoListWebAPI.Services.UserServices;

namespace TodoListWebAPI.Controllers
{   
    [Route("api/[controller]")]
    [ApiController]

    public class AuthController : ControllerBase
    {
        //public static HMACSHA512 hmac = new HMACSHA512();
        //public static User user = new User()
        //{
        //    Username = "test",
        //    PasswordSalt = hmac.Key,
        //    PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes("1111")),
        //};

        private readonly IConfiguration _config;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public AuthController(IConfiguration config, IUserService userService,IMapper mapper)
        {
            _config = config;
            _userService = userService;
            _mapper = mapper;
        }
            
        [HttpGet, Authorize]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            var userName = await _userService.GetUsers();
            return Ok(userName);
        }
        

        [HttpPost("register")]
        public  ActionResult<User> Register(UserDto inputUser)
        {
            if (inputUser == null)
                return BadRequest(ModelState);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            _userService.CreatePassWordHash(inputUser.Password, out byte[] passwordHash, out byte[] passwordSalt);

            var user = _userService.AddUser(new User()
            {
                Username = inputUser.Username,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                UserTodos = Enumerable.Empty<TodoItem>()
            }) ;
            return Ok(user);
        }
        [HttpPost("login/{userId}")]
        public  ActionResult<string> Login(UserDto inputUser, int userId)
        {
            var user = _userService.GetUser(userId);
            if (inputUser.Username == string.Empty && inputUser.Password == string.Empty)
            {
                return BadRequest("please enter user name and password");
            }
            else if (user.Username != inputUser.Username)
                return BadRequest("Wrong Username or Password");
            else if (!_userService.VerifyPasswordHash(inputUser.Password, user.PasswordHash!, user.PasswordSalt!))
                return BadRequest("Wrong Username or Password");

            string token = _userService.CreateToken(user);
            return Ok(token);

            
        }

        
    }
}
