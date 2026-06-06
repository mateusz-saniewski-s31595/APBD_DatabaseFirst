namespace APBD_DatabaseFirst.DTOs;

public class BedResponseDto
{
    public int Id { get; set; }
    public BedTypeResponseDto BedType { get; set; } = null!;
    public RoomResponseDto Room { get; set; } = null!;
}