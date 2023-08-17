using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Models;
using Repositories;
using Services.AccountManagement;
using Services.Administration;
using Services.Authentication;
using Services.LiveData;
using Services.Mcp;
using Services.Message;
using Services.Profile;
using Services.Report;
using Services.Routing;
using Services.TaskEntry;
using Services.UI;
using Services.Vehicle;

var builder = WebApplication.CreateBuilder(args);

var settings = new Settings();
builder.Configuration.Bind("Settings", settings);
builder.Services.AddSingleton(settings);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

#region Database

builder.Services.AddDbContext<UwcDbContext>(o => o.UseSqlServer(builder.Configuration.GetConnectionString("Db")));
builder.Services.AddTransient<UnitOfWork>();

#endregion

#region Services

builder.Services.AddScoped<IAccountManagementService, AccountManagementService>();
builder.Services.AddScoped<IAdministrationService, AdministrationService>();
builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
builder.Services.AddScoped<IUserProfileService, UserProfileService>();
builder.Services.AddScoped<IMcpService, McpService>();
builder.Services.AddScoped<IMessageService, MessageService>();
builder.Services.AddScoped<IWorkService, WorkService>();
builder.Services.AddScoped<IVehicleService, VehicleService>();
builder.Services.AddScoped<IDrivingHistoryService, DrivingHistoryService>();
builder.Services.AddScoped<IDrivingLicenseService, DrivingLicenseService>();

builder.Services.AddScoped<CleanerReportService>();
builder.Services.AddScoped<DriverReportService>();
builder.Services.AddScoped<VehicleReportService>();
builder.Services.AddScoped<RouteOptimizationService>();
builder.Services.AddScoped<DesktopUICustomizationService>();
builder.Services.AddScoped<MobileUICustomizationService>();

builder.Services.AddSingleton<McpCapacityService>();
builder.Services.AddHostedService<McpCapacityService>(provider => provider.GetService<McpCapacityService>());

builder.Services.AddSingleton<VehicleLocationService>();
builder.Services.AddHostedService<VehicleLocationService>(provider => provider.GetService<VehicleLocationService>());

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(o =>
    {
        o.TokenValidationParameters = new TokenValidationParameters
        {
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(settings.BearerKey)),
            ValidateIssuerSigningKey = true,
            ValidateAudience = false,
            ValidateIssuer = false
        };
    });

#endregion

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

// app.SeedData();

app.Run();