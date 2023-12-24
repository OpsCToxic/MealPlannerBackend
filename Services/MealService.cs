using MealPlanBackend.Models;
using MongoDB.Driver;
using MongoDB.Driver.Core.Operations;

namespace MealPlanBackend.Services
{
    public class MealService: IMealService
    {
        private readonly IMongoCollection<Meal> _meals;

        public MealService(IMealPlannerDatabaseSettings settings, IMongoClient mongoClient) {
            var database = mongoClient.GetDatabase(settings.DatabaseName);
            _meals = database.GetCollection<Meal>(settings.MealsCollectionName);
        }
        public async Task<List<Meal>> GetMealsAsync()
        {
            return await _meals.Find(meal => true).ToListAsync();
        }

        public async Task<Meal> GetMealByIdAsync(string mealId)
        {
            return await _meals.Find(meal => meal.MealId == mealId).FirstOrDefaultAsync();
        }

        public async Task<Meal> CreateMealAsync(Meal meal)
        {
            await _meals.InsertOneAsync(meal);
            return meal;
        }

        public async Task<bool> UpdateMealAsync(string mealId, Meal meal)
        {
            var result = await _meals.ReplaceOneAsync(meal => meal.MealId == mealId, meal);
            return result.IsAcknowledged && result.ModifiedCount > 0;
        }

        public async Task<bool> RemoveMealAsync(string mealId)
        {
            var result = await _meals.DeleteOneAsync(meal => meal.MealId == mealId);
            return result.IsAcknowledged && result.DeletedCount > 0;
        }

        public async Task<List<Meal>> GetMealsByUserIdAsync(string userId)
        {
            return await _meals.Find(meal => meal.UserId == userId).ToListAsync();
        }

        public async Task<Meal> GetMealByIdForUserAsync(string userId, string mealId)
        {
            return await _meals.Find(meal => meal.UserId == userId && meal.MealId == mealId).FirstOrDefaultAsync();
        }

        public async Task<Meal> CreateMealForUserAsync(string userId, Meal meal)
        {
            meal.UserId = userId; // Set the associated user ID
            await _meals.InsertOneAsync(meal);
            return meal;
        }

        public async Task<bool> UpdateMealForUserAsync(string userId, string mealId, Meal meal)
        {
            meal.UserId = userId; // Ensure the meal belongs to the specified user
            var result = await _meals.ReplaceOneAsync(m => m.UserId == userId && m.MealId == mealId, meal);
            return result.IsAcknowledged && result.ModifiedCount > 0;
        }

        public async Task<bool> RemoveMealForUserAsync(string userId, string mealId)
        {
            var result = await _meals.DeleteOneAsync(meal => meal.UserId == userId && meal.MealId == mealId);
            return result.IsAcknowledged && result.DeletedCount > 0;
        }
    }
}
