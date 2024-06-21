using Backend_TimTour.Models.ResultsModels;

namespace Backend_TimTour.Interfaces.UserInterfaces
{
    public interface IUserRegisterService
    {
        Task<ServiceResult> RegisterUserAsync(string username, string password, string email, string type);
    }
}
