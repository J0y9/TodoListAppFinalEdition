using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using TodoListAppFinalEdition.Shared;
using TodoListWebAPI.Data;

namespace TodoListWebAPI.Services.UserServices
{
    public class UserService : IUserService
    {
        private readonly TodoContext _context;
        private readonly IConfiguration _config;

        public UserService(TodoContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }
         public async Task<User> AddUser(User user)
        {
            var itemCreated = await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return itemCreated.Entity;
        }
        public void CreatePassWordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }

        public bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(passwordHash);
            }
        }
        public string CreateToken(User user)
        {
            List<Claim> myClaims = new List<Claim>() {
                new Claim("userId",user.Id.ToString())
                
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                _config.GetSection("AppSettings:Token").Value!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            var token = new JwtSecurityToken(
                claims: myClaims,
                
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds);
            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            return jwt;
        }

        public async Task<IEnumerable<User>> GetUsers()
        {
            return await _context.Users.ToListAsync();
        }

        public User GetUser(string userName)
        {
            var user =  _context.Users.Where(u => u.Username == userName).FirstOrDefault();
            if(user!=null)
            {
                return user;
            }
            return null!;
        }
    }
}
