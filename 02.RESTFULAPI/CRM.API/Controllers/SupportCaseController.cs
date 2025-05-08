using CRM.Application.Services;
using CRM.Core.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CRM.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class SupportCaseController: ControllerBase
    {
        private readonly SupportCaseService _supportCaseService;

        public SupportCaseController(SupportCaseService supportCaseService)
        {
            _supportCaseService = supportCaseService;
        }

        [HttpGet]
        public async Task<IEnumerable<SupportCase>> GetAllSupportCasesAsync()
        {
            return await _supportCaseService.GetAllSupportCasesAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SupportCase?>> GetSupportCaseByIdAsync(Guid id)
        {
            var supportCase = await _supportCaseService.GetSupportCaseByIdAsync(id);
            if (supportCase == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(supportCase);
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddSupportCaseAsync(SupportCase supportCase)
        {
            await _supportCaseService.AddSupportCaseAsync(supportCase);
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateSupportCaseAsync(SupportCase supportCase)
        {
            await _supportCaseService.UpdateSupportCaseAsync(supportCase);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSupportCaseAsync(Guid id)
        {
            var supportCase = await _supportCaseService.GetSupportCaseByIdAsync(id);
            if (supportCase == null)
            {
                return NotFound();
            }
            else
            {
                await _supportCaseService.DeleteSupportCaseAsync(id);
                return Ok();
            }
        }
    }
}
