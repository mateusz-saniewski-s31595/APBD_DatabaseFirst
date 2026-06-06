namespace APBD_DatabaseFirst.DTOs;

public class PatientResponseDto
{
    public string Pesel { get; set; } = null!;
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public int Age { get; set; }
    public string Sex { get; set; } = null!;   // "Male" / "Female"

    public List<AdmissionResponseDto> Admissions { get; set; } = new();
    public List<BedAssignmentResponseDto> BedAssignments { get; set; } = new();
}
