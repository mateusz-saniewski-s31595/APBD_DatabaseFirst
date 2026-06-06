using APBD_DatabaseFirst.DTOs;

namespace APBD_DatabaseFirst.Services;

public interface IPatientService
{
    Task<IEnumerable<PatientResponseDto>> GetAllPatientsAsync(string? search);
}