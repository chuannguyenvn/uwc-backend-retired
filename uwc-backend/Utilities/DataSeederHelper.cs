public static class DataSeederHelper
{
    public static readonly string[] MaleSupervisorFirstNames = {"Samuel", "Simon", "Sebastian", "Stephen", "Scott"};
    public static readonly string[] FemaleSupervisorFirstNames = {"Sophia", "Samantha", "Sara", "Stella", "Sienna"};

    public static readonly string[] SupervisorLastNames =
    {
        "Smith", "Stevens", "Sanders", "Sullivan", "Scott", "Simpson", "Spencer", "Stone", "Sanchez", "Summers"
    };

    public static readonly string[] MaleDriverFirstNames =
    {
        "David", "Daniel", "Dylan", "Dominic", "Derek", "Dennis", "Dean", "Damon", "Darius", "Donovan"
    };

    public static readonly string[] FemaleDriverFirstNames =
    {
        "Danielle", "Diana", "Daisy", "Daphne", "Delilah", "Deborah", "Demi", "Dakota", "Destiny", "Diana"
    };

    public static readonly string[] DriverLastNames =
    {
        "Davis",
        "Dawson",
        "Dixon",
        "Donovan",
        "Dunn",
        "Daniels",
        "Davenport",
        "Davidson",
        "Delgado",
        "Drake",
        "Dixon",
        "Dawson",
        "Donovan",
        "Dunn",
        "Daniels",
        "Davenport",
        "Davidson",
        "Delgado",
        "Drake",
        "Douglas"
    };

    public static readonly string[] MaleCleanerFirstNames =
    {
        "Connor", "Caleb", "Cameron", "Colin", "Carter", "Christopher", "Clayton", "Cody", "Cooper", "Christian"
    };

    public static readonly string[] FemaleCleanerFirstNames =
    {
        "Chloe", "Claire", "Caroline", "Chelsea", "Catherine", "Cassandra", "Camila", "Charlotte", "Cora", "Cecilia"
    };

    public static readonly string[] CleanerLastNames =
    {
        "Carlson",
        "Clark",
        "Cunningham",
        "Cooper",
        "Cortez",
        "Chang",
        "Chapman",
        "Cohen",
        "Cruz",
        "Crawford",
        "Carlson",
        "Clark",
        "Cunningham",
        "Cooper",
        "Cortez",
        "Chang",
        "Chapman",
        "Cohen",
        "Cruz",
        "Crawford",
        "Crank"
    };

    public static readonly float MinLatitude = 10.7f;
    public static readonly float MaxLatitude = 10.8f;
    public static readonly float MinLongitude = 106.6f;
    public static readonly float MaxLongitude = 106.7f;

    private static readonly Random Random = new();

    public static DateTime GenerateRandomDate(int startYear, int endYear)
    {
        var year = Random.Next(startYear, endYear + 1);
        var month = Random.Next(1, 13);
        var day = Random.Next(1, DateTime.DaysInMonth(year, month) + 1);
        return new DateTime(year, month, day);
    }

    public static int GenerateRandomLicenseCount()
    {
        return Random.Next(1, 4);
    }

    public static string GenerateRandomIssuePlace()
    {
        string[] issuePlaces = {"City A", "City B", "City C", "City D", "City E"};
        return issuePlaces[Random.Next(issuePlaces.Length)];
    }

    public static string GenerateRandomLicenseType()
    {
        string[] licenseTypes = {"A", "B", "C", "D", "E"};
        return licenseTypes[Random.Next(licenseTypes.Length)];
    }

    public static string[] GenerateLicensePlates(int count)
    {
        var licensePlates = new string[count];

        for (var i = 0; i < count; i++)
        {
            var letters = GenerateRandomLetters(3);
            var numbers = GenerateRandomNumbers(3);

            licensePlates[i] = $"{letters}-{numbers}";
        }

        return licensePlates;
    }

    public static string GenerateRandomLetters(int count)
    {
        const string letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        return new string(Enumerable.Repeat(letters, count).Select(s => s[Random.Next(s.Length)]).ToArray());
    }

    public static string GenerateRandomNumbers(int count)
    {
        const string numbers = "0123456789";
        return new string(Enumerable.Repeat(numbers, count).Select(s => s[Random.Next(s.Length)]).ToArray());
    }

    public static (double latitude, double longitude) GenerateRandomCoordinates(double minLatitude, double maxLatitude, double minLongitude,
        double maxLongitude)
    {
        var random = new Random();
        var latitude = random.NextDouble() * (maxLatitude - minLatitude) + minLatitude;
        var longitude = random.NextDouble() * (maxLongitude - minLongitude) + minLongitude;
        return (latitude, longitude);
    }
}