using AttendanceList.Models;

namespace AttendanceList.Services;

public class AttendanceService : IAttendanceService
{
    private static readonly string[] _names =
    {
        "Alice Johnson", "Brian Smith", "Catherine Lee", "David Brown", "Emma Wilson",
        "Frank Davis", "Grace Miller", "Henry Moore", "Isabella Taylor", "James Anderson",
        "Karen Thomas", "Liam Jackson", "Mia White", "Noah Harris", "Olivia Martin",
        "Paul Thompson", "Quinn Garcia", "Rachel Martinez", "Samuel Robinson", "Tina Clark",
        "Umar Rodriguez", "Victoria Lewis", "William Walker", "Xena Hall", "Yusuf Allen",
        "Zoe Young", "Aaron Hernandez", "Bella King", "Carlos Wright", "Diana Lopez"
    };

    private static readonly string[] _departments =
        { "HR", "Finance", "Operations", "IT", "Marketing", "Sales", "Logistics", "Legal" };

    private static readonly string[] _shifts =
        { "Morning", "Day", "Evening", "Night" };

    private static readonly string[] _statuses =
        { "Present", "Absent", "Late", "Half Day", "On Leave" };

    private static readonly List<AttendanceRecord> _records;

    static AttendanceService()
    {
        _records = new List<AttendanceRecord>();
        var rnd = new Random(42); // fixed seed for reproducibility
        var baseDate = new DateTime(2026, 1, 1);

        for (int i = 1; i <= 120; i++)
        {
            var name = _names[(i - 1) % _names.Length];
            var empId = 1000 + ((i - 1) % _names.Length) + 1;
            var date = baseDate.AddDays(rnd.Next(0, 180));
            var status = _statuses[rnd.Next(_statuses.Length)];
            var dept = _departments[rnd.Next(_departments.Length)];
            var shift = _shifts[rnd.Next(_shifts.Length)];

            string notes = status switch
            {
                "Present" => "On time.",
                "Absent" => "No check-in recorded.",
                "Late" => $"Arrived {rnd.Next(5, 60)} minutes late.",
                "Half Day" => "Left early due to personal reasons.",
                "On Leave" => "Approved leave.",
                _ => string.Empty
            };

            _records.Add(new AttendanceRecord
            {
                Id = i,
                EmployeeId = empId,
                EmployeeName = name,
                AttendanceDate = date,
                Status = status,
                Notes = notes,
                Department = dept,
                Shift = shift
            });
        }
    }

    public IEnumerable<AttendanceRecord> GetAll() => _records;

    public AttendanceRecord? GetById(int id) =>
        _records.FirstOrDefault(x => x.Id == id);

    public PaginatedList<AttendanceRecord> GetPaged(int pageNumber, int pageSize)
    {
        if (pageNumber < 1) pageNumber = 1;

        var totalCount = _records.Count;

        // pageSize == 0 means "All" — return everything on a single page
        if (pageSize <= 0)
            return new PaginatedList<AttendanceRecord>(_records, totalCount, 1, totalCount);

        var items = _records
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize);

        return new PaginatedList<AttendanceRecord>(items, totalCount, pageNumber, pageSize);
    }
}
