using CRM.Application.Services;
using CRM.Core.Entities;
using CRM.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CRM.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ActivityController:ControllerBase
    {
        private readonly ActivityService _activityService;

        public ActivityController(ActivityService activityService)
        {
            _activityService = activityService;
        }

        [HttpGet]
        public async Task<IEnumerable<Activity>> GetAllActivitiesAsync()
        {
            return await _activityService.GetAllActivitiesAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Activity>> GetActivityByIdAsync(Guid id)
        {
            var activity = await _activityService.GetActivityByIdAsync(id);
            if (activity == null)
            {
                return NotFound();
            }
            return Ok(activity);
        }

        [HttpPost]
        public async Task<IActionResult> AddActivityAsync(Activity activity)
        {
            await _activityService.AddActivityAsync(activity);
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateActivityAsync(Activity activity)
        {
            await _activityService.UpdateActivityAsync(activity);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteActivityAsync(Guid id)
        {
            var activity = await _activityService.GetActivityByIdAsync(id);
            if (activity == null)
            {
                return NotFound();
            }
            else
            {
                await _activityService.DeleteActivityAsync(id);
                return Ok();
            }

        }
    }
}
