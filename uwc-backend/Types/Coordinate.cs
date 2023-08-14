namespace Types;

public struct Coordinate
{
    public double Latitude;
    public double Longitude;

    public Coordinate(double latitude, double longitude)
    {
        Latitude = latitude;
        Longitude = longitude;
    }

    public static Coordinate operator +(Coordinate a, Coordinate b)
    {
        return new(a.Latitude + b.Latitude, a.Longitude + b.Longitude);
    }

    public static Coordinate operator -(Coordinate a, Coordinate b)
    {
        return new(a.Latitude - b.Latitude, a.Longitude - b.Longitude);
    }

    public static Coordinate operator *(Coordinate a, float b)
    {
        return new(a.Latitude * b, a.Latitude * b);
    }

    public static Coordinate operator *(Coordinate a, double b)
    {
        return new(a.Latitude * b, a.Latitude * b);
    }

    public static Coordinate Lerp(Coordinate from, Coordinate to, float t)
    {
        return from + (to - from) * t;
    }

    public static Coordinate Lerp(Coordinate from, Coordinate to, double t)
    {
        return from + (to - from) * t;
    }

    public override string ToString()
    {
        return "Lat: " + Latitude + ". Longitude: " + Longitude;
    }
}

public static class CoordinateExtensions
{
    public static double DistanceTo(this Coordinate from, Coordinate to)
    {
        return Math.Sqrt(Math.Pow(from.Latitude - to.Latitude, 2) + Math.Pow(from.Longitude - to.Longitude, 2));
    }
}