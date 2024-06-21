using Backend_TimTour.Interfaces.LoginInterfaces;
using Backend_TimTour.Interfaces.UserInterfaces;
using Backend_TimTour.Models;
using Backend_TimTour.Models.Factories;
using Backend_TimTour.Models.PrefferenceModels;
using Backend_TimTour.Models.ResultsModels;

namespace Backend_TimTour.Services
{
    public class UserRegisterService:IUserRegisterService
    {
        private readonly IPasswordService _passwordService;
        private readonly IUserRepository _userRepository;
        public UserRegisterService(IUserRepository userRepository, IPasswordService passwordService)
        {
            _userRepository = userRepository;
            _passwordService = passwordService;
        }
        public async Task<ServiceResult> RegisterUserAsync(string username,string password,string email, string type)
        {
            if(await _userRepository.ValidateUserCreationByEmail(email) == RepositoryResult.USER_CAN_BE_CREATED)
            {
                string encryptedPassword = _passwordService.EncryptPassword(password);
                var emptyPrefference = PrefferenceFactory.CreateEmptyPrefference();
                User registeredUser = new User
                {
                    Email = email,
                    Name = username,
                    Password = encryptedPassword,
                    Prefference = emptyPrefference,
                    Type = type
                };

                var result = await _userRepository.AddAsync(registeredUser);

                if(result == RepositoryResult.USER_CREATED)
                {
                    return ServiceResult.USER_SUCCESFULLY_REGISTERED;
                }
                else
                {
                    return ServiceResult.REGISTRATION_FAILED;
                }    

            }
            else
            {
                return ServiceResult.EMAIL_ALREADY_USED;
            }
        }
    }
}
