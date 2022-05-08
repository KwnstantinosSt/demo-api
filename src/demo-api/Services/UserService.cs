using demo_api.Configs;
using demo_api.Context;
using demo_api.Dtos.UserDtos;
using demo_api.Models;

namespace demo_api.Services
{
    public interface IUserService
    {
        public string GetToken(User user);
        public User CheckExists(LoginUserDto login);
        public bool Login(LoginUserDto login, User user);
        public bool RegisterUser(RegisterUserDto login);
        public User GetLoggedInUser();

    }
    public class UserService : IUserService
    {
        private readonly MyContext _context;
        private readonly JwtSettings _jwtSettings;

        public UserService(MyContext context, JwtSettings jwtSettings)
        {
            _context = context;
            _jwtSettings = jwtSettings;
        }

        public User CheckExists(LoginUserDto login)
        {
            throw new NotImplementedException();
        }

        public User GetLoggedInUser()
        {
            throw new NotImplementedException();
        }

        public string GetToken(User user)
        {
            throw new NotImplementedException();
        }

        public bool Login(LoginUserDto login, User user)
        {
            throw new NotImplementedException();
        }

        public bool RegisterUser(RegisterUserDto login)
        {
            throw new NotImplementedException();
        }



        /*
               try {
                var Token = new UserTokens();
                var Valid = logins.Any(x => x.UserName.Equals(userLogins.UserName, StringComparison.OrdinalIgnoreCase));
                if (Valid) {
                    var user = logins.FirstOrDefault(x => x.UserName.Equals(userLogins.UserName, StringComparison.OrdinalIgnoreCase));
                    Token = JwtHelpers.JwtHelpers.GenTokenkey(new UserTokens() {
                        EmailId = user.EmailId,
                            GuidId = Guid.NewGuid(),
                            UserName = user.UserName,
                            Id = user.Id,
                    }, jwtSettings);
                } else {
                    return BadRequest($ "wrong password");
                }
                return Ok(Token);
            } catch (Exception ex) {
                throw;
            }
        */

    }
}