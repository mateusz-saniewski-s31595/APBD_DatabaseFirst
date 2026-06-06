using APBD_DatabaseFirst.DTOs;

namespace APBD_DatabaseFirst.Services;

public interface IBedAssignmentService
{
    Task<BedAssignmentCreatedDto> AssignBedAsync(string pesel, BedAssignmentRequestDto request);
}