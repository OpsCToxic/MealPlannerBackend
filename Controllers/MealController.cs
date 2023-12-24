using MealPlanBackend.Models;
using MealPlanBackend.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MealPlanBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MealController : ControllerBase
    {
        private readonly IMealService mealService;

        public MealController(IMealService studentService)
        {
            this.mealService = studentService;
        }
        // GET: api/<MealController>
        [HttpGet]
        public async Task<ActionResult<List<Meal>>> Get()
        {
            return await mealService.GetMealsAsync();
        }

        // GET api/<MealController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Meal>> Get(string id)
        {
            var meal = await mealService.GetMealByIdAsync(id);

            if (meal == null)
            {
                return NotFound($"Meal with Id = {id} not found");
            }

            return Ok(meal);
        }

        // POST api/<MealController>
        [HttpPost]
        public async Task<ActionResult<Meal>> Post([FromBody] Meal meal)
        {
            await mealService.CreateMealAsync(meal);
            return CreatedAtAction(nameof(Get), new { id = meal.MealId }, meal);
        }

        // PUT api/<MealController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, [FromBody] Meal meal)
        {
            var existingMeal = await mealService.GetMealByIdAsync(id);

            if (existingMeal == null)
            {
                return NotFound($"Meal with Id = {id} not found");
            }

            await mealService.UpdateMealAsync(id, meal);

            return NoContent();
        }

        // DELETE api/<MealController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var existingMeal = mealService.GetMealByIdAsync(id);

            if (existingMeal == null)
            {
                return NotFound($"Meal with Id = {id} not found");
            }

            await mealService.RemoveMealAsync(id);

            return Ok($"Student with Id = {id} deleted");
        }
        // New endpoint to get all meals for a specific user
        [HttpGet("user/{userId}")]
        public async Task<ActionResult<List<Meal>>> GetMealsForUser(string userId)
        {
            var mealsForUser = await mealService.GetMealsByUserIdAsync(userId);
            return Ok(mealsForUser);
        }

        // New endpoint to create a meal for a specific user
        [HttpPost("user/{userId}")]
        public async Task<ActionResult<Meal>> PostMealForUser(string userId, [FromBody] Meal meal)
        {
            var createdMeal = await mealService.CreateMealForUserAsync(userId, meal);
            return CreatedAtAction(nameof(Get), new { id = createdMeal.MealId }, createdMeal);
        }

        // New endpoint to update a meal for a specific user by meal ID
        [HttpPut("user/{userId}/{id}")]
        public async Task<IActionResult> PutMealForUser(string userId, string id, [FromBody] Meal meal)
        {
            var existingMeal = await mealService.GetMealByIdForUserAsync(userId, id);

            if (existingMeal == null)
            {
                return NotFound($"Meal with Id = {id} for User = {userId} not found");
            }

            await mealService.UpdateMealForUserAsync(userId, id, meal);

            return NoContent();
        }

        // New endpoint to delete a meal for a specific user by meal ID
        [HttpDelete("user/{userId}/{id}")]
        public async Task<IActionResult> DeleteMealForUser(string userId, string id)
        {
            var existingMeal = await mealService.GetMealByIdForUserAsync(userId, id);

            if (existingMeal == null)
            {
                return NotFound($"Meal with Id = {id} for User = {userId} not found");
            }

            await mealService.RemoveMealForUserAsync(userId, id);

            return Ok($"Meal with Id = {id} for User = {userId} deleted");
        }
    }
}

