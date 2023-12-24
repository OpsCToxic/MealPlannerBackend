using MealPlanBackend.Models;
using MongoDB.Driver;

namespace MealPlanBackend.Services
{
    public class UserService: IUserService
    {
        private readonly IMongoCollection<User> _users;

        public UserService(IMealPlannerDatabaseSettings settings, IMongoClient mongoClient)
        {
            var database = mongoClient.GetDatabase(settings.DatabaseName);
            _users = database.GetCollection<User>(settings.UsersCollectionName);
        }
        public async Task<List<User>> GetUsersAsync()
        {
            return await _users.Find(user => true).ToListAsync();
        }
        public async Task<User> GetUserByIdAsync(string userId)
        {
            return await _users.Find(user => user.UserId == userId).FirstOrDefaultAsync();
        }
        public async Task<User> GetUserByUsernameAsync(string username)
        {
            return await _users.Find(user => user.Username == username).FirstOrDefaultAsync();
        }
        public async Task<User> CreateUserAsync(User user)
        {
            await _users.InsertOneAsync(user);
            return user;
        }
        public async Task<bool> UpdateUserAsync(string userId, User user)
        {
            var result = await _users.ReplaceOneAsync(meal => meal.UserId == userId, user);
            return result.IsAcknowledged && result.ModifiedCount > 0;
        }
        public async Task<bool> RemoveUserAsync(string userId)
        {
            var result = await _users.DeleteOneAsync(meal => meal.UserId == userId);
            return result.IsAcknowledged && result.DeletedCount > 0;
        }
    }
}
