using MealPlanBackend.Models;
using System.Threading.Tasks;

namespace MealPlanBackend.Services
{
    public interface IMealRequestService
    {
        Task<MealPlan> GenerateMealPlanAsync(User userProfile);
    }


}

