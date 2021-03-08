using SoftNews.Data.Model;
using SoftNews.Repository.Interfaces;
using SoftNews.Service.Interfaces;
using System.Threading.Tasks;

namespace SoftNews.Service.Implementations
{
    public class AuthService : IAuthService
    {
        private IAuthRepository authRepository;
        public AuthService(IAuthRepository authRepository)
        {
            this.authRepository = authRepository;
        }

        public Task<User> Login(string username, string password)
        {
            return authRepository.Login(username, password);
        }

        public Task<User> Register(User user, string password)
        {
            return authRepository.Register(user, password);
        }

        public Task<bool> UserExists(string username)
        {
            return authRepository.UserExists(username);
        }
    }
}
