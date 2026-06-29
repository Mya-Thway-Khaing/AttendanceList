namespace AttendanceList.Models;

public class AttendanceIndexViewModel
{
    public PaginatedList<AttendanceRecord> PagedData { get; set; } = null!;
    public AttendanceFilter Filter { get; set; } = new();
}
