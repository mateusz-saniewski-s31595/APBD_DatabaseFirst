using APBD_DatabaseFirst.DTOs;
using APBD_DatabaseFirst.Services;
using Microsoft.AspNetCore.Mvc;

namespace APBD_DatabaseFirst.Controllers;

[ApiController]
[Route("api/patients")]
public class PatientsController : ControllerBase
{
    private readonly IPatientService _patientService;

    public PatientsController(IPatientService patientService)
    {
        _patientService = patientService;
    }
    
    // GET /api/patients
    // GET /api/patients?search=an
    [HttpGet]
    public async Task<IActionResult> GetPatients([FromQuery] string? search)
    {
        var patients = await _patientService.GetAllPatientsAsync(search);
        return Ok(patients);
    }
}