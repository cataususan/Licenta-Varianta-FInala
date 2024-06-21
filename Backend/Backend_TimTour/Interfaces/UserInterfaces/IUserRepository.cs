using Backend_TimTour.Models;
using Backend_TimTour.Models.PrefferenceModels;
using Backend_TimTour.Models.ResultsModels;

namespace Backend_TimTour.Interfaces.UserInterfaces
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task<RepositoryResult> AddAsync(User user);
        Task<RepositoryResult> ValidateUserCreationByEmail(string email);
        Task<(RepositoryResult, User)> FindByEmailAsync(string email);
        Task<RepositoryResult> InsertUserPrefferences(string email, Prefference prefference);
    }
}
