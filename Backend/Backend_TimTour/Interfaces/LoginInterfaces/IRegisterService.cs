using Backend_TimTour.Models.ResultsModels;

namespace Backend_TimTour.Interfaces.LoginInterfaces
{
    public interface IRegisterService
    {
        Task<ServiceResult> RegisterUserAsync(string userName, string password,string email); 

    }
}
