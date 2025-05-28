using Microsoft.AspNetCore.Mvc;
using Palantir00CW9S30320.Services;
using Palantir00CW9S30320.DTOs;
using System.Threading.Tasks;
using System;

namespace Palantir00CW9S30320.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PrescriptionsController : ControllerBase
    {
        private readonly IPrescriptionService _service;

        public PrescriptionsController(IPrescriptionService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Create(NewPrescriptionDto dto)
        {
            try
            {
                await _service.AddPrescriptionAsync(dto);
                return CreatedAtAction(nameof(Create), null);
            }
            catch (KeyNotFoundException knf)
            {
                return NotFound(knf.Message);
            }
            catch (InvalidOperationException inv)
            {
                return BadRequest(inv.Message);
            }
        }
    }
}