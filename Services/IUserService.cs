using MealPlanBackend.Models;

namespace MealPlanBackend.Services
{
    public interface IUserService
    {
        Task<List<User>> GetUsersAsync();
        Task<User> GetUserByIdAsync(string userId);
        Task<User> GetUserByUsernameAsync(string userId);
        Task<User> CreateUserAsync(User user);
        Task<bool> UpdateUserAsync(string userId, User user);
        Task<bool> RemoveUserAsync(string userId);
    }
}