using System.Diagnostics;
using AttendanceList.Models;
using AttendanceList.Services;
using Microsoft.AspNetCore.Mvc;

namespace AttendanceList.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IAttendanceService _attendanceService;

    public HomeController(ILogger<HomeController> logger, IAttendanceService attendanceService)
    {
        _logger = logger;
        _attendanceService = attendanceService;
    }

    public IActionResult Index(int page = 1, int pageSize = 10)
    {
        var paged = _attendanceService.GetPaged(page, pageSize);
        _logger.LogInformation(
            "Loaded page {Page}/{TotalPages} ({Count} records of {Total}).",
            paged.PageNumber, paged.TotalPages, paged.Items.Count, paged.TotalCount);
        return View(paged);
    }

    public IActionResult Details(int id)
    {
        var record = _attendanceService.GetById(id);

        if (record is null)
        {
            _logger.LogWarning("Attendance record with id {Id} was not found.", id);
            return NotFound();
        }

        return View(record);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
