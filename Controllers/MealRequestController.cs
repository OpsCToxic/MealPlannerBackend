using MealPlanBackend.Models;
using MealPlanBackend.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MealPlanBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MealRequestController : ControllerBase
    {
        private readonly IMealRequestService _mealPlannerService;

        public MealRequestController(IMealRequestService mealPlannerService)
        {
            _mealPlannerService = mealPlannerService;
        }

        [HttpPost("generateMealPlan")]
        public IActionResult GenerateMealPlan([FromBody] User userProfile)
        {
            // Convert the UserProfileDto to the required format for the meal planner service
            var userMealPlan = _mealPlannerService.GenerateMealPlanAsync(userProfile);

            if (userMealPlan != null)
            {
                return Ok(userMealPlan);
            }
            else
            {
                return BadRequest("Unable to generate meal plan");
            }
        }
    }
}
