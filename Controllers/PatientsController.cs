using Microsoft.AspNetCore.Mvc;
using Palantir00CW9S30320.Services;
using Palantir00CW9S30320.DTOs;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Palantir00CW9S30320.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PatientsController : ControllerBase
    {
        private readonly IPrescriptionService _service;

        public PatientsController(IPrescriptionService service)
        {
            _service = service;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PatientWithPrescriptionsDto>> Get(int id)
        {
            try
            {
                var result = await _service.GetPatientAsync(id);
                return Ok(result);
            }
            catch (KeyNotFoundException knf)
            {
                return NotFound(knf.Message);
            }
        }
    }
}