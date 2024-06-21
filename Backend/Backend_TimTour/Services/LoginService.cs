using Backend_TimTour.Interfaces.LoginInterfaces;
using Backend_TimTour.Interfaces.UserInterfaces;
using Backend_TimTour.Models.ResultsModels;

namespace Backend_TimTour.Services
{
    public class LoginService:ILoginService
    {
        private readonly IPasswordService _passwordService;
        private readonly IUserRepository _userRepository;
        private readonly ITokenService _tokenService;

        public LoginService(IUserRepository userRepository, ITokenService tokenService, IPasswordService passwordService)
        {
            _userRepository = userRepository;
            _tokenService = tokenService;
            _passwordService = passwordService;
        }
        public async Task<(ServiceResult,string)> LoginAsync(string email,string password)
        {
            var (result,user) = await _userRepository.FindByEmailAsync(email);
            if (result == RepositoryResult.USER_FOUND)
            {
                bool isPasswordValid = _passwordService.ValidatePassword(password, user.Password);
                if (isPasswordValid)
                {
                    var token = _tokenService.GenerateToken(user);
                    return (ServiceResult.LOGIN_SUCCESFUL, token);
                }
                else
                {
                    var failedString = "Login failed because the password is invalid";
                    return (ServiceResult.PASSWORD_INVALID, failedString);
                }
            }
            else
            {
                var userNotFoundString = "User does not exist in our database";
                return (ServiceResult.USER_NOT_FOUND_IN_DATABASE, userNotFoundString);
            }
        }
    }
}
