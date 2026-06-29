using AttendanceList.Models;

namespace AttendanceList.Services;

public interface IAttendanceService
{
    IEnumerable<AttendanceRecord> GetAll();
    AttendanceRecord? GetById(int id);
    PaginatedList<AttendanceRecord> GetPaged(int pageNumber, int pageSize);
}
