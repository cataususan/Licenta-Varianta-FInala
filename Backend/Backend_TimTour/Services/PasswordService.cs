using Backend_TimTour.Interfaces.LoginInterfaces;

namespace Backend_TimTour.Services
{
    public class PasswordService:IPasswordService
    {
        public string EncryptPassword(string password)
        {
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(password);
            return hashedPassword;
        }

        public bool ValidatePassword(string password, string passwordHash)
        {
            return BCrypt.Net.BCrypt.Verify(password, passwordHash);
        }
    }
}
