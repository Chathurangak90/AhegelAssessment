using Ahegel.Contracts;
using Ahegel.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Ahegel.Api
{
    [Route("api/patients")]
    [ApiController]
    public class PatientController : ControllerBase
    {
        private readonly IPatientService _patientService;

        public PatientController(IPatientService patientService)
        {
            _patientService = patientService;
        }

        [HttpGet]
        public async Task<IActionResult> GetPatients() => Ok(await _patientService.GetPatients());

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPatient(int id)
        {
            var patient = await _patientService.GetPatient(id);
            if (patient == null) return NotFound($"Patient with ID {id} not found.");
            return Ok(patient);
        }

        [HttpPost]
        public async Task<IActionResult> CreatePatient([FromBody] Patient patient)
        {
            if (patient == null) return BadRequest("Patient data is required.");
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var createdPatient = await _patientService.CreatePatient(patient);
            return CreatedAtAction(nameof(GetPatient), new { id = createdPatient.Id }, createdPatient);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePatient(int id, [FromBody] Patient patient)
        {
            if (patient == null) return BadRequest("Patient data is required.");
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var updatedPatient = await _patientService.UpdatePatient(id, patient);
            if (updatedPatient == null) return NotFound($"Patient with ID {id} not found.");

            return Ok(updatedPatient);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> SoftDeletePatient(int id)
        {
            var result = await _patientService.SoftDeletePatient(id);
            if (!result) return NotFound($"Patient with ID {id} not found.");

            return Ok(new { Message = "Patient deleted successfully" });
        }
    }
}
