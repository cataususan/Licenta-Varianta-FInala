using Backend_TimTour.Models.ResultsModels;

namespace Backend_TimTour.Interfaces.LoginInterfaces
{
    public interface ILoginService
    {
        Task<(ServiceResult,string)> LoginAsync(string email, string password);
    }
}
