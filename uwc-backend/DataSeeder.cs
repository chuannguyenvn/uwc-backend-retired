using Models;
using Models.Types;
using Repositories;

public class DataSeeder
{
    private readonly UwcDbContext _uwcDbContext;

    private readonly string[] _maleSupervisorFirstNames = {"Samuel", "Simon", "Sebastian", "Stephen", "Scott"};
    private readonly string[] _femaleSupervisorFirstNames = {"Sophia", "Samantha", "Sara", "Stella", "Sienna"};

    private readonly string[] _supervisorLastNames =
    {
        "Smith", "Stevens", "Sanders", "Sullivan", "Scott", "Simpson", "Spencer", "Stone", "Sanchez", "Summers"
    };

    private readonly string[] _maleDriverFirstNames =
    {
        "David", "Daniel", "Dylan", "Dominic", "Derek", "Dennis", "Dean", "Damon", "Darius", "Donovan"
    };

    private readonly string[] _femaleDriverFirstNames =
    {
        "Danielle", "Diana", "Daisy", "Daphne", "Delilah", "Deborah", "Demi", "Dakota", "Destiny", "Diana"
    };

    private readonly string[] _driverLastNames =
    {
        "Davis", "Dawson", "Dixon", "Donovan", "Dunn", "Daniels", "Davenport", "Davidson", "Delgado", "Drake"
    };

    private readonly string[] _maleCleanerFirstNames =
    {
        "Connor", "Caleb", "Cameron", "Colin", "Carter", "Christopher", "Clayton", "Cody", "Cooper", "Christian"
    };

    private readonly string[] _femaleCleanerFirstNames =
    {
        "Chloe", "Claire", "Caroline", "Chelsea", "Catherine", "Cassandra", "Camila", "Charlotte", "Cora", "Cecilia"
    };

    private readonly string[] _cleanerLastNames =
    {
        "Carlson", "Clark", "Cunningham", "Cooper", "Cortez", "Chang", "Chapman", "Cohen", "Cruz", "Crawford"
    };

    private List<UserProfile> _allProfiles = new();

    public DataSeeder(UwcDbContext uwcDbContext)
    {
        _uwcDbContext = uwcDbContext;
    }

    public void SeedSupervisorProfiles()
    {
        for (int i = 0; i < 10; i++)
        {
            var firstName = i < 5 ? _maleSupervisorFirstNames[i] : _femaleSupervisorFirstNames[i - 5];
            string lastName = _supervisorLastNames[i];
            Gender gender = i < 5 ? Gender.Male : Gender.Female;
            DateTime dateOfBirth = GenerateRandomDateOfBirth();

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
        for (int i = 0; i < 20; i++)
        {
            var firstName = i < 10 ? _maleDriverFirstNames[i] : _femaleDriverFirstNames[i - 10];
            string lastName = _driverLastNames[i];
            Gender gender = i < 10 ? Gender.Male : Gender.Female;
            DateTime dateOfBirth = GenerateRandomDateOfBirth();

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
        for (int i = 0; i < 20; i++)
        {
            var firstName = i < 10 ? _maleCleanerFirstNames[i] : _femaleCleanerFirstNames[i - 10];
            string lastName = _cleanerLastNames[i];
            Gender gender = i < 10 ? Gender.Male : Gender.Female;
            DateTime dateOfBirth = GenerateRandomDateOfBirth();

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

    private DateTime GenerateRandomDateOfBirth()
    {
        Random random = new Random();
        int year = random.Next(1960, 2000);
        int month = random.Next(1, 13);
        int day = random.Next(1, DateTime.DaysInMonth(year, month) + 1);
        return new DateTime(year, month, day);
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
    }

    public void SeedDrivingLicenses()
    {
    }

    public void SeedMcps()
    {
    }

    public void SeedVehicles()
    {
    }

    public void SeedTasks()
    {
    }

    public void SeedRoutes()
    {
    }

    public void SeedMessages()
    {
    }
}