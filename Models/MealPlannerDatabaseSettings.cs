namespace MealPlanBackend.Models
{
    public class MealPlannerDatabaseSettings : IMealPlannerDatabaseSettings
    {
        public string MealsCollectionName { get; set; } = String.Empty;
        public string UsersCollectionName { get; set; } = String.Empty;
        public string MealPlanCollectionName { get; set; } = String.Empty;
        public string ConnectionURI { get; set; } = String.Empty;
        public string DatabaseName { get; set; } = String.Empty;
    }
}
