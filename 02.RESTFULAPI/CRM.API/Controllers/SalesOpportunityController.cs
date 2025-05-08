using CRM.Application.Services;
using CRM.Core.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CRM.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class SalesOpportunityController: ControllerBase
    {
        private readonly SalesOpportunityService _salesOpportunityService;

        public SalesOpportunityController(SalesOpportunityService salesOpportunityService)
        {
            _salesOpportunityService = salesOpportunityService;
        }

        [HttpGet]
        public async Task<IEnumerable<SalesOpportunity>> GetAllSalesOpportunitiesAsync()
        {
            return await _salesOpportunityService.GetAllSalesOpportunitiesAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SalesOpportunity?>> GetSalesOpportunityByIdAsync(Guid id)
        {
            var salesOpportunity = await _salesOpportunityService.GetSalesOpportunityByIdAsync(id);
            if (salesOpportunity == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(salesOpportunity);
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddSalesOpportunityAsync(SalesOpportunity salesOpportunity)
        {
            await _salesOpportunityService.AddSalesOpportunityAsync(salesOpportunity);
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateSalesOpportunityAsync(SalesOpportunity salesOpportunity)
        {
            await _salesOpportunityService.UpdateSalesOpportunityAsync(salesOpportunity);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSalesOpportunityAsync(Guid id)
        {
            var salesOpportunity = await _salesOpportunityService.GetSalesOpportunityByIdAsync(id);
            if (salesOpportunity == null)
            {
                return NotFound();
            }
            else
            {
                await _salesOpportunityService.DeleteSalesOpportunityAsync(id);
                return Ok();
            }
        }

    }
}
