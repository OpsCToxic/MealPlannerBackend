using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace MealPlanBackend.Models
{
    [BsonIgnoreExtraElements]
    public class User
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string UserId { get; set; } = String.Empty;
        [BsonElement("username")]
        public string Username { get; set; } = String.Empty;
        [BsonElement("email")]
        public string Email { get; set; } = String.Empty;
        [BsonElement("passwordHash")]
        public string PasswordHash { get; set; } = String.Empty;
        [BsonElement("fullName")]
        public string FullName { get; set; } = String.Empty;
        [BsonElement("createdAt")]
        public DateTime CreatedAt { get; set; }
        [BsonElement("healthInformation")]
        public HealthProfile HealthInformation { get; set; } = null!;
    }
    public class HealthProfile
    {
        // Health-related properties such as weight, height, allergies, etc.
        [BsonElement("weight")]
        public double Weight { get; set; }
        [BsonElement("age")]
        public int Age { get; set; }
        [BsonElement("gender")]
        public string Gender { get; set; } = String.Empty;
        [BsonElement("preferredCuisines")]
        public List<string> PreferredCuisines { get; set; } = null!;
        [BsonElement("plannedMeals")]
        public List<string> PlannedMeals { get; set; } = null!;
        [BsonElement("dietaryRestrictions")]
        public List<string> DietaryRestrictions { get; set; } = null!;
        //Store height in inches
        [BsonElement("height")]
        public int Height { get; set; }
        // Text that describes food allergies of user
        [BsonElement("allergies")]
        public string Allergies { get; set; } = String.Empty;
        [BsonElement("hoursActivity")]
        public int HoursOfFitness { get; set; }
        [BsonElement("activites")]
        public string Activities { get; set; } = String.Empty;

    }

}
