using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NomsNoms.Data;

namespace NomsNoms.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MealPlanController : ControllerBase
    {
        private readonly MealPlanRepository _repository;
        public MealPlanController(MealPlanRepository repository)
        {
            _repository = repository;
        }

        [HttpGet("type")]
        public async Task<IActionResult> GetAllType()
        {
            return Ok(await _repository.GetAllType());
        }
    }
}
