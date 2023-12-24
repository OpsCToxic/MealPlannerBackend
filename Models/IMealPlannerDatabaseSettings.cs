namespace MealPlanBackend.Models
{
    public interface IMealPlannerDatabaseSettings
    {
        string MealsCollectionName { get; set; }
        string UsersCollectionName { get; set; }
        string ConnectionURI { get; set; }
        string DatabaseName { get; set; }
    }
}
