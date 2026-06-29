using System.Diagnostics;
using AttendanceList.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IAttendanceService, AttendanceService>();

builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Logging.AddDebug();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}
else
{
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

var chromeConfig = builder.Configuration.GetSection("ChromeWebUi");
var openOnStartup = chromeConfig.GetValue<bool>("OpenOnStartup");
var browser = chromeConfig["Browser"] ?? "chrome";
var baseUrl = chromeConfig["BaseUrl"] ?? "http://localhost:5212";

if (openOnStartup)
{
    app.Lifetime.ApplicationStarted.Register(() =>
    {
        try
        {
            app.Logger.LogInformation("Opening {Browser} at {Url}.", browser, baseUrl);
            Process.Start(new ProcessStartInfo
            {
                FileName = browser,
                Arguments = baseUrl,
                UseShellExecute = true
            });
        }
        catch (Exception ex)
        {
            app.Logger.LogError(ex, "Failed to open {Browser} for {Url}.", browser, baseUrl);
        }
    });
}

app.Run();
