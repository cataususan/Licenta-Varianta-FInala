using Backend_TimTour.Interfaces.UserInterfaces;
using Backend_TimTour.Models;
using Backend_TimTour.Models.PrefferenceModels;
using Backend_TimTour.Models.ResultsModels;
using MongoDB.Driver;

namespace Backend_TimTour.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IMongoCollection<User> _users;

        public UserRepository(IMongoClient mongoClient)
        {
            var database = mongoClient.GetDatabase("Users");
            _users = database.GetCollection<User>("Users");
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await _users.Find(user => true).ToListAsync();
        }
        public async Task<RepositoryResult> ValidateUserCreationByEmail(string emailToFind)
        {
            var filter = Builders<User>.Filter.Eq(user => user.Email, emailToFind);
            var userFound=await _users.Find(filter).FirstOrDefaultAsync();

            if (userFound != null)
            {
                return RepositoryResult.USER_ALREADY_EXIST;
            }
            else
                return RepositoryResult.USER_CAN_BE_CREATED;
        }
        public async Task<(RepositoryResult,User)> FindByEmailAsync(string email)
        {
            var filter = Builders<User>.Filter.Eq(user => user.Email, email);

            var userFound = await _users.Find(filter).FirstOrDefaultAsync();

            if (userFound != null)
            {
                return(RepositoryResult.USER_FOUND,userFound);
            }
            else
            {
                return (RepositoryResult.USER_NOT_FOUND, userFound);
            }
        }

        public async Task<RepositoryResult> AddAsync(User user)
        {
            try
            {
                await _users.InsertOneAsync(user);
                return RepositoryResult.USER_CREATED;
            }
            catch (MongoWriteException ex)
            {
                Console.WriteLine($"Insert failed: {ex.Message}");
                return RepositoryResult.USER_CANT_BE_CREATED;
            }
        }
        public async Task<RepositoryResult> InsertUserPrefferences(string email, Prefference prefference)
        {
            var filter = Builders<User>.Filter.Eq(user => user.Email, email);
            var update = Builders<User>.Update
                .Set("Prefference", prefference);
            try
            {
                await _users.UpdateOneAsync(filter, update);
                return RepositoryResult.USER_SUCCESFULLY_UPDATED;
            }
            catch(MongoWriteException ex)
            {
                Console.WriteLine($"Update failed: {ex.Message}");
                return RepositoryResult.USER_CAN_NOT_BE_UPDATED;
            }
            
        }
    }
}
