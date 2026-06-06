namespace APBD_DatabaseFirst.DTOs;

public class RoomResponseDto
{
    public string Id { get; set; } = null!;
    public bool HasTv { get; set; }
    public WardResponseDto Ward { get; set; } = null!;
}