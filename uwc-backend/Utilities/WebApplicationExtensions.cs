using Repositories;

public static class WebApplicationExtensions
{
    public static WebApplication SeedData(this WebApplication webApplication)
    {
        var scopedFactory = webApplication.Services.GetRequiredService<IServiceScopeFactory>();
        using var scope = scopedFactory.CreateScope();
        using var dbContext = scope.ServiceProvider.GetService<UwcDbContext>();
        if (dbContext == null) throw new NullReferenceException("Database is not present for seeding.");
        dbContext.Database.EnsureCreated();

        var dataSeeder = new DataSeeder(dbContext);
        dataSeeder.SeedSupervisorProfiles();
        dataSeeder.SeedDriverProfiles();
        dataSeeder.SeedCleanerProfiles();
        dataSeeder.SeedAccounts();
        dataSeeder.SeedDrivingHistories();
        dataSeeder.SeedDrivingLicenses();
        dataSeeder.SeedMcps();
        dataSeeder.SeedVehicles();
        // dataSeeder.SeedTasks();
        // dataSeeder.SeedRoutes();
        dataSeeder.SeedMessages();
        dataSeeder.FinishSeeding();

        return webApplication;
    }
}