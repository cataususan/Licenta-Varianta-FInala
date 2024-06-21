namespace Backend_TimTour.Interfaces.LoginInterfaces
{
    public interface IPasswordService
    {
        public bool ValidatePassword(string password, string passwordHash);
        public string EncryptPassword(string password);
    }
}
