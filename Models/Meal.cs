using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace MealPlanBackend.Models
{
    [BsonIgnoreExtraElements]
    public class Meal
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string MealId { get; set; } = String.Empty;
       
        [BsonElement("date")]
        public DateTime Date { get; set; }
        [BsonElement("mealType")]
        public string MealType { get; set; } = String.Empty;
        [BsonElement("recipeTitle")]
        public string RecipeTitle { get; set; } = String.Empty;
        [BsonElement("recipeInstructions")]
        public string RecipeInstructions { get; set; } = String.Empty;
        [BsonElement("ingredients")]
        public List<Ingredient> Ingredients { get; set; } = null!;
        [BsonElement("calories")]
        public int Calories { get; set; }
        [BsonElement("notes")]
        public string Notes { get; set; } = String.Empty;
    }

    public class Ingredient
    {
        [BsonElement("name")]
        public string Name { get; set; } = String.Empty;
        [BsonElement("quantity")]
        public double Quantity { get; set; }
        [BsonElement("unit")]
        public string Unit { get; set; } = String.Empty;
    }
    

    // Other CRUD operations can be similarly implemented using the MongoDB.Driver methods.

}
