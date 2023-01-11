using TodoListAppFinalEdition.Shared;

namespace TodoListWebAPI.Services.UserServices
{
    public interface IUserService
    {
        Task<User> AddUser(User user);
        Task<IEnumerable<User>> GetUsers();
        User GetUser(int Id);
        string CreateToken(User user);
        void CreatePassWordHash(string password, out byte[] passwordHash, out byte[] passwordSalt);
        bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt);
    }
}
