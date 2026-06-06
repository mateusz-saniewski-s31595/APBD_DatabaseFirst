using System.ComponentModel.DataAnnotations;

namespace APBD_DatabaseFirst.DTOs;

public class BedAssignmentDto
{
    [Required]
    public DateTime From { get; set; }
    
    public DateTime? To { get; set; }

    [Required] public string BedType { get; set; } = null!;
    
    [Required]
    public string Ward { get; set; } = null!;
}