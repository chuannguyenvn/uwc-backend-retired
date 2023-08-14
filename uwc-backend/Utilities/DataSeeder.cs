using Models;
using Repositories;
using Types;

public class DataSeeder
{
    private readonly List<UserProfile> _allProfiles = new();
    private readonly UwcDbContext _uwcDbContext;

    public DataSeeder(UwcDbContext uwcDbContext)
    {
        _uwcDbContext = uwcDbContext;
    }

    public void SeedSupervisorProfiles()
    {
        var maleSupervisorFirstNames = DataSeederHelper.MaleSupervisorFirstNames;
        var femaleSupervisorFirstNames = DataSeederHelper.FemaleSupervisorFirstNames;
        var supervisorLastNames = DataSeederHelper.SupervisorLastNames;

        for (var i = 0; i < 10; i++)
        {
            var firstName = i < 5 ? maleSupervisorFirstNames[i] : femaleSupervisorFirstNames[i - 5];
            var lastName = supervisorLastNames[i];
            var gender = i < 5 ? Gender.Male : Gender.Female;
            var dateOfBirth = DataSeederHelper.GenerateRandomDate(1960, 2000);

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
        var maleDriverFirstNames = DataSeederHelper.MaleDriverFirstNames;
        var femaleDriverFirstNames = DataSeederHelper.FemaleDriverFirstNames;
        var driverLastNames = DataSeederHelper.DriverLastNames;

        for (var i = 0; i < 20; i++)
        {
            var firstName = i < 10 ? maleDriverFirstNames[i] : femaleDriverFirstNames[i - 10];
            var lastName = driverLastNames[i];
            var gender = i < 10 ? Gender.Male : Gender.Female;
            var dateOfBirth = DataSeederHelper.GenerateRandomDate(1960, 2000);

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
        var maleCleanerFirstNames = DataSeederHelper.MaleCleanerFirstNames;
        var femaleCleanerFirstNames = DataSeederHelper.FemaleCleanerFirstNames;
        var cleanerLastNames = DataSeederHelper.CleanerLastNames;

        for (var i = 0; i < 20; i++)
        {
            var firstName = i < 10 ? maleCleanerFirstNames[i] : femaleCleanerFirstNames[i - 10];
            var lastName = cleanerLastNames[i];
            var gender = i < 10 ? Gender.Male : Gender.Female;
            var dateOfBirth = DataSeederHelper.GenerateRandomDate(1960, 2000);

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
                Settings = ""
            };

            _uwcDbContext.Accounts.Add(account);
        }
    }

    public void SeedDrivingHistories()
    {
        var random = new Random();

        foreach (var driverProfile in _uwcDbContext.DriverProfiles.ToList())
        {
            var historyCount = random.Next(1, 11);

            for (var i = 0; i < historyCount; i++)
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
        var random = new Random();
        var vehicleCount = _uwcDbContext.Vehicles.Count();
        var randomIndex = random.Next(vehicleCount);
        return _uwcDbContext.Vehicles.Skip(randomIndex).FirstOrDefault();
    }

    public void SeedDrivingLicenses()
    {
        foreach (var driverProfile in _uwcDbContext.DriverProfiles)
        {
            var licenseCount = DataSeederHelper.GenerateRandomLicenseCount();

            for (var i = 0; i < licenseCount; i++)
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
        var realDataList = new List<Mcp>
        {
            new() {Capacity = 127, Latitude = 10.7647293589828, Longitude = 106.663266154958, Address = "580 Bà Hạt, P.6, Q.10, TPHCM"},
            new()
            {
                Capacity = 82,
                Latitude = 10.7675037535589,
                Longitude = 106.667284093661,
                Address = "500 Nguyễn Tri Phương, P.9, Q.10, TPHCM"
            },
            new()
            {
                Capacity = 142,
                Latitude = 10.7629567124463,
                Longitude = 106.656977523526,
                Address = "968 3 Tháng 2, P.15, Q.11, TPHCM"
            },
            new()
            {
                Capacity = 98,
                Latitude = 10.7815403101810,
                Longitude = 106.655190599178,
                Address = "389 Lý Thường Kiệt, P.8, Q.Tân Bình, TPHCM"
            },
            new()
            {
                Capacity = 162,
                Latitude = 10.7771799685277,
                Longitude = 106.660531841364,
                Address = "334 Tô Hiến Thành, P.14, Q.10, TPHCM"
            },
            new()
            {
                Capacity = 128,
                Latitude = 10.7712254318066,
                Longitude = 106.665784313171,
                Address = "54 Thành Thái, P.10, Q.10, TPHCM"
            },
            new()
            {
                Capacity = 89,
                Latitude = 10.7668398628915,
                Longitude = 106.659288627832,
                Address = "300 Lý Thường Kiệt, P.14, Q.10, TPHCM"
            },
            new()
            {
                Capacity = 120,
                Latitude = 10.752208718089143,
                Longitude = 106.64971397274442,
                Address = "96 Phạm Đình Hổ, P.2, Q.6, TPHCM"
            },
            new()
            {
                Capacity = 200, Latitude = 10.786135, Longitude = 106.651209, Address = "1150 Lạc Long Quân, P.8, Q.Tân Bình, TPHCM"
            },
            new() {Capacity = 201, Latitude = 10.782575, Longitude = 106.660679, Address = "153 Bắc Hải, P.15, Q.10, TPHCM"},
            new() {Capacity = 195, Latitude = 10.775672, Longitude = 106.667233, Address = "533 Sư Vạn Hạnh, P.12, Q.10, TPHCM"},
            new() {Capacity = 141, Latitude = 10.776559, Longitude = 106.663600, Address = "218 Thành Thái, P.15, Q.10, TPHCM"},
            new() {Capacity = 137, Latitude = 10.771117, Longitude = 106.652352, Address = "84 Nguyễn Thị Nhỏ, P.9, Q.Tân Bình, TPHCM"},
            new() {Capacity = 183, Latitude = 10.7807, Longitude = 106.676, Address = "276 Cách Mạng Tháng Tám, P.15, Q.3, TPHCM"},
            new() {Capacity = 140, Latitude = 10.7612, Longitude = 106.661, Address = "93 Lý Thường Kiệt, P.7, Q.10, TPHCM"}
        };

        foreach (var realData in realDataList)
        {
            var mcp = new Mcp
            {
                Capacity = realData.Capacity, Latitude = realData.Latitude, Longitude = realData.Longitude, Address = realData.Address
            };

            _uwcDbContext.Mcps.Add(mcp);
        }
    }


    public void SeedVehicles()
    {
        var licensePlates = DataSeederHelper.GenerateLicensePlates(30);
        string[] models = {"Model 1", "Model 2", "Model 3", "Model 4", "Model 5"};
        VehicleType[] vehicleTypes = {VehicleType.FrontLoader, VehicleType.SideLoader, VehicleType.RearLoader};

        var random = new Random();

        for (var i = 0; i < 30; i++)
        {
            var licensePlate = licensePlates[i];
            var model = models[random.Next(models.Length)];
            var vehicleType = vehicleTypes[random.Next(vehicleTypes.Length)];
            var currentLoad = random.NextDouble() * 100;
            var capacity = random.NextDouble() * 200;
            var averageSpeed = random.NextDouble() * 80;

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