using APBD_DatabaseFirst.DTOs;
using APBD_DatabaseFirst.Exceptions;
using APBD_DatabaseFirst.Services;
using Microsoft.AspNetCore.Mvc;

namespace APBD_DatabaseFirst.Controllers;

[ApiController]
[Route("api/patients")]
public class BedAssignmentsController : ControllerBase
{
    private readonly IBedAssignmentService _bedAssignmentService;

    public BedAssignmentsController(IBedAssignmentService bedAssignmentService)
    {
        _bedAssignmentService = bedAssignmentService;
    }

    // POST /api/patients/{pesel}/bedassignments
    [HttpPost("{pesel}/bedassignments")]
    public async Task<IActionResult> AssignBed(
        [FromRoute] string pesel,
        [FromBody] BedAssignmentRequestDto request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            var result = await _bedAssignmentService.AssignBedAsync(pesel, request);
            return CreatedAtAction(nameof(AssignBed), new { pesel }, result);
        }
        catch (NotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }
}