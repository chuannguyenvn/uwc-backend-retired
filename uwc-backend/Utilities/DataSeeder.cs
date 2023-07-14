using Models;
using Models.Types;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

public class DataSeeder
{
    private readonly UwcDbContext _uwcDbContext;

    private readonly List<UserProfile> _allProfiles = new();

    public DataSeeder(UwcDbContext uwcDbContext)
    {
        _uwcDbContext = uwcDbContext;
    }

    public void SeedSupervisorProfiles()
    {
        string[] maleSupervisorFirstNames = DataSeederHelper.MaleSupervisorFirstNames;
        string[] femaleSupervisorFirstNames = DataSeederHelper.FemaleSupervisorFirstNames;
        string[] supervisorLastNames = DataSeederHelper.SupervisorLastNames;

        for (int i = 0; i < 10; i++)
        {
            var firstName = i < 5 ? maleSupervisorFirstNames[i] : femaleSupervisorFirstNames[i - 5];
            string lastName = supervisorLastNames[i];
            Gender gender = i < 5 ? Gender.Male : Gender.Female;
            DateTime dateOfBirth = DataSeederHelper.GenerateRandomDate(1960, 2000);

            var supervisorProfile = new SupervisorProfile
            {
                FirstName = firstName,
                LastName = lastName,
                Gender = gender,
                DateOfBirth = dateOfBirth,
                Role = UserRole.Supervisor
            };

            _uwcDbContext.SupervisorProfiles.Add(supervisorProfile);
            _allProfiles.Add(supervisorProfile);
        }
    }

    public void SeedDriverProfiles()
    {
        string[] maleDriverFirstNames = DataSeederHelper.MaleDriverFirstNames;
        string[] femaleDriverFirstNames = DataSeederHelper.FemaleDriverFirstNames;
        string[] driverLastNames = DataSeederHelper.DriverLastNames;

        for (int i = 0; i < 20; i++)
        {
            var firstName = i < 10 ? maleDriverFirstNames[i] : femaleDriverFirstNames[i - 10];
            string lastName = driverLastNames[i];
            Gender gender = i < 10 ? Gender.Male : Gender.Female;
            DateTime dateOfBirth = DataSeederHelper.GenerateRandomDate(1960, 2000);

            var driverProfile = new DriverProfile
            {
                FirstName = firstName,
                LastName = lastName,
                Gender = gender,
                DateOfBirth = dateOfBirth,
                Role = UserRole.Driver
            };

            _uwcDbContext.DriverProfiles.Add(driverProfile);
            _allProfiles.Add(driverProfile);
        }
    }

    public void SeedCleanerProfiles()
    {
        string[] maleCleanerFirstNames = DataSeederHelper.MaleCleanerFirstNames;
        string[] femaleCleanerFirstNames = DataSeederHelper.FemaleCleanerFirstNames;
        string[] cleanerLastNames = DataSeederHelper.CleanerLastNames;

        for (int i = 0; i < 20; i++)
        {
            var firstName = i < 10 ? maleCleanerFirstNames[i] : femaleCleanerFirstNames[i - 10];
            string lastName = cleanerLastNames[i];
            Gender gender = i < 10 ? Gender.Male : Gender.Female;
            DateTime dateOfBirth = DataSeederHelper.GenerateRandomDate(1960, 2000);

            var cleanerProfile = new CleanerProfile
            {
                FirstName = firstName,
                LastName = lastName,
                Gender = gender,
                DateOfBirth = dateOfBirth,
                Role = UserRole.Cleaner
            };

            _uwcDbContext.CleanerProfiles.Add(cleanerProfile);
            _allProfiles.Add(cleanerProfile);
        }
    }

    public void SeedAccounts()
    {
        foreach (var userProfile in _allProfiles)
        {
            var account = new Account
            {
                Username = userProfile.FirstName.ToLower() + "_" + userProfile.LastName.ToLower(),
                PasswordHash = "password",
                Salt = "salt",
                LinkedProfile = userProfile,
                Settings = "",
            };

            _uwcDbContext.Accounts.Add(account);
        }
    }

    public void SeedDrivingHistories()
    {
        Random random = new Random();

        foreach (var driverProfile in _uwcDbContext.DriverProfiles.ToList())
        {
            int historyCount = random.Next(1, 11);

            for (int i = 0; i < historyCount; i++)
            {
                var drivingHistory = new DrivingHistory
                {
                    DriverProfile = driverProfile,
                    Timestamp = DataSeederHelper.GenerateRandomDate(2015, 2023),
                    VehicleUsed = GetRandomVehicle()
                };

                _uwcDbContext.DrivingHistories.Add(drivingHistory);
            }
        }
    }

    private Vehicle GetRandomVehicle()
    {
        Random random = new Random();
        int vehicleCount = _uwcDbContext.Vehicles.Count();
        int randomIndex = random.Next(vehicleCount);
        return _uwcDbContext.Vehicles.Skip(randomIndex).FirstOrDefault();
    }

    public void SeedDrivingLicenses()
    {
        foreach (var driverProfile in _uwcDbContext.DriverProfiles)
        {
            int licenseCount = DataSeederHelper.GenerateRandomLicenseCount();

            for (int i = 0; i < licenseCount; i++)
            {
                var drivingLicense = new DrivingLicense
                {
                    DriverProfile = driverProfile,
                    IssueDate = DataSeederHelper.GenerateRandomDate(2000, 2022),
                    IssuePlace = DataSeederHelper.GenerateRandomIssuePlace(),
                    Type = DataSeederHelper.GenerateRandomLicenseType()
                };

                _uwcDbContext.DrivingLicenses.Add(drivingLicense);
            }
        }
    }

    public void SeedMcps()
    {
        Random random = new Random();

        for (int i = 0; i < 30; i++)
        {
            float capacity = random.Next(500, 1000);
            float currentLoad = random.Next(0, (int)capacity);
            (double latitude, double longitude) = DataSeederHelper.GenerateRandomCoordinates(DataSeederHelper.MinLatitude,
                DataSeederHelper.MaxLatitude,
                DataSeederHelper.MinLongitude,
                DataSeederHelper.MaxLongitude);

            var mcp = new Mcp {Capacity = capacity, CurrentLoad = currentLoad, Latitude = latitude, Longitude = longitude};

            _uwcDbContext.Mcps.Add(mcp);
        }
    }
    
    public void SeedVehicles()
    {
        string[] licensePlates = DataSeederHelper.GenerateLicensePlates(30);
        string[] models = {"Model 1", "Model 2", "Model 3", "Model 4", "Model 5"};
        VehicleType[] vehicleTypes = {VehicleType.FrontLoader, VehicleType.SideLoader, VehicleType.RearLoader};

        Random random = new Random();

        for (int i = 0; i < 30; i++)
        {
            string licensePlate = licensePlates[i];
            string model = models[random.Next(models.Length)];
            VehicleType vehicleType = vehicleTypes[random.Next(vehicleTypes.Length)];
            double currentLoad = random.NextDouble() * 100;
            double capacity = random.NextDouble() * 200;
            double averageSpeed = random.NextDouble() * 80;

            var vehicle = new Vehicle
            {
                LicensePlate = licensePlate,
                Model = model,
                VehicleType = vehicleType,
                CurrentLoad = currentLoad,
                Capacity = capacity,
                AverageSpeed = averageSpeed
            };

            _uwcDbContext.Vehicles.Add(vehicle);
        }
    }

    public void FinishSeeding()
    {
        _uwcDbContext.SaveChanges();
    }
}