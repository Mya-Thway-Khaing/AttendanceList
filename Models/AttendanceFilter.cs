namespace AttendanceList.Models;

public class AttendanceFilter
{
    public string? EmployeeName { get; set; }
    public string? Department   { get; set; }
    public string? DateFrom     { get; set; }
    public string? DateTo       { get; set; }
    public string? Shift        { get; set; }
    public string? Status       { get; set; }

    public int ActiveCount =>
        (!string.IsNullOrWhiteSpace(EmployeeName) ? 1 : 0) +
        (!string.IsNullOrWhiteSpace(Department)   ? 1 : 0) +
        (!string.IsNullOrWhiteSpace(DateFrom)     ? 1 : 0) +
        (!string.IsNullOrWhiteSpace(DateTo)       ? 1 : 0) +
        (!string.IsNullOrWhiteSpace(Shift)        ? 1 : 0) +
        (!string.IsNullOrWhiteSpace(Status)       ? 1 : 0);

    public bool HasAnyFilter => ActiveCount > 0;
}
