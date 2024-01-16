
using Emp;
using Emp.Data;
using Microsoft.EntityFrameworkCore;



var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<Db_context>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("EmployeeConnectionString")));
builder.Services.AddApplicationInsightsTelemetry();

// Startup.cs






var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
static IHostBuilder CreateHostBuilder(String[] args) =>
    Host.CreateDefaultBuilder(args)
    .ConfigureWebHostDefaults(webBuilder =>
    {
        webBuilder.UseContentRoot(Directory.GetCurrentDirectory());
        webBuilder.UseWebRoot("webBuilder");
        //webBuilder.UseStartup<Startup>();
    });
