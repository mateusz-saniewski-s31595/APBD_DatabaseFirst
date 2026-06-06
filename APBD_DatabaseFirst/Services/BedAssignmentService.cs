using APBD_DatabaseFirst.Data;
using APBD_DatabaseFirst.DTOs;
using APBD_DatabaseFirst.Exceptions;
using APBD_DatabaseFirst.Models;
using Microsoft.EntityFrameworkCore;

namespace APBD_DatabaseFirst.Services;

public class BedAssignmentService : IBedAssignmentService
{
    private readonly DbfirstContext _context;

    public BedAssignmentService(DbfirstContext context)
    {
        _context = context;
    }

    public async Task<BedAssignmentCreatedDto> AssignBedAsync(string pesel, BedAssignmentRequestDto request)
    {
        if (!await _context.Patients.AnyAsync(p => p.Pesel == pesel))
            throw new NotFoundException($"Patient with PESEL '{pesel}' was not found");

        var ward = await _context.Wards.FirstOrDefaultAsync(w => w.Name == request.Ward)
            ?? throw new NotFoundException($"Ward '{request.Ward}' does not exist");

        var bedType = await _context.BedTypes.FirstOrDefaultAsync(bt => bt.Name == request.BedType)
            ?? throw new NotFoundException($"Bed type '{request.BedType}' does not exist");

        var requestFrom = request.From;
        var requestTo   = request.To;

        var availableBed = await _context.Beds
            .Where(b =>
                b.BedTypeId == bedType.Id &&
                b.Room.WardId == ward.Id)
            .Where(b => !b.BedAssignments.Any(ba =>
                (requestTo == null || ba.From < requestTo) &&
                (ba.To == null || ba.To > requestFrom)
            ))
            .FirstOrDefaultAsync()
            ?? throw new NotFoundException(
                $"No available bed of type '{request.BedType}' in ward '{request.Ward}' " +
                $"for the requested period ({request.From:yyyy-MM-dd HH:mm} – " +
                $"{(request.To.HasValue ? request.To.Value.ToString("yyyy-MM-dd HH:mm") : "open-ended")}).");

        var assignment = new BedAssignment
        {
            PatientPesel = pesel,
            BedId        = availableBed.Id,
            From         = request.From,
            To           = request.To
        };

        _context.BedAssignments.Add(assignment);
        await _context.SaveChangesAsync();

        return new BedAssignmentCreatedDto
        {
            Id           = assignment.Id,
            PatientPesel = assignment.PatientPesel,
            BedId        = assignment.BedId,
            From         = assignment.From,
            To           = assignment.To
        };
    }
}