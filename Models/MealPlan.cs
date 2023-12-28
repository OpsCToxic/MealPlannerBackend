using DnsClient;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MealPlanBackend.Models
{
    [BsonIgnoreExtraElements]
    public class MealPlan
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string MealPlanId { get; set; } = String.Empty;
        [BsonElement("userId")]
        public string UserId { get; set; } = String.Empty;
        [BsonElement("planFrequency")]\
        // In days
        public string PlanFrequency { get; set; } = String.Empty;
        [BsonElement("mealList")]
        public List<Meal> MealList { get; set; } = null!;
    }
}
