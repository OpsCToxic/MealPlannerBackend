using MealPlanBackend.Models;

namespace MealPlanBackend.Services
{
    public interface IMealService
    {
        Task<List<Meal>> GetMealsAsync();
        Task<Meal> GetMealByIdAsync(string mealId);
        Task<Meal> CreateMealAsync(Meal meal);
        Task<bool> UpdateMealAsync(string mealId, Meal meal);
        Task<bool> RemoveMealAsync(string mealId);
        Task<List<Meal>> GetMealsByUserIdAsync(string userId);
        Task<Meal> GetMealByIdForUserAsync(string userId, string mealId);
        Task<Meal> CreateMealForUserAsync(string userId, Meal meal);
        Task<bool> UpdateMealForUserAsync(string userId, string mealId, Meal meal);
        Task<bool> RemoveMealForUserAsync(string userId, string mealId);
    }
}
