using Backend_TimTour.Models;

namespace Backend_TimTour.Interfaces.LoginInterfaces
{
    public interface ITokenService
    {
        string GenerateToken(User user);
    }
}
