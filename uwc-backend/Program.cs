using Microsoft.EntityFrameworkCore;
using Repositories;
using Repositories.Implementations;
using Services.Authentication;
using Services.Employee;
using Services.LiveData;
using Services.Mcp;
using Services.Message;
using Services.Report;
using Services.Routing;
using Services.Task;
using Services.UI;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

#region Database

builder.Services.AddDbContext<UwcDbContext>(o => o.UseSqlServer(builder.Configuration.GetConnectionString("Db")));
builder.Services.AddTransient<UnitOfWork>();

#endregion

#region Services

builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
builder.Services.AddScoped<IEmployeeInformationService, EmployeeInformationService>();
builder.Services.AddScoped<IMcpService, McpService>();
builder.Services.AddScoped<IMessageService, MessageService>();
builder.Services.AddScoped<IRouteService, RouteService>();

builder.Services.AddSingleton<VehiclePositionService>();
builder.Services.AddScoped<CleanerReportService>();
builder.Services.AddScoped<DriverReportService>();
builder.Services.AddScoped<McpRepository>();
builder.Services.AddScoped<VehicleReportService>();

builder.Services.AddScoped<RouteOptimizationService>();

builder.Services.AddScoped<TaskService>();

builder.Services.AddScoped<DesktopUICustomizationService>();
builder.Services.AddScoped<MobileUICustomizationService>();

#endregion

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();