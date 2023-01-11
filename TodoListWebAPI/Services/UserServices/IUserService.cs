using TodoListAppFinalEdition.Shared;

namespace TodoListWebAPI.Services.UserServices
{
    public interface IUserService
    {

        public string GetUserName();
        string CreateToken(User user);
        void CreatePassWordHash(string password, out byte[] passwordHash, out byte[] passwordSalt);
        bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt);
    }
}
