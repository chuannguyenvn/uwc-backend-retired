using System.Diagnostics;

namespace Commons.Types
{
    public struct Coordinate
    {
        public double Latitude;
        public double Longitude;
        
        public Coordinate(double latitude, double longitude)
        {
            Latitude = latitude;
            Longitude = longitude;
        }

        public Coordinate(List<double> coordinates)
        {
            Debug.Assert(coordinates.Count == 2);
            Latitude = coordinates[1];
            Longitude = coordinates[0];
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
            return new(a.Latitude * b, a.Longitude * b);
        }

        public static Coordinate operator *(Coordinate a, double b)
        {
            return new(a.Latitude * b, a.Longitude * b);
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
        
        public double AngleTo(Coordinate other)
        {
            // Calculate the vectors between 'this' and 'other' coordinates
            Coordinate vector1 = new Coordinate(other.Latitude - Latitude, other.Longitude - Longitude);
            Coordinate vector2 = new Coordinate(1.0, 0.0); // Reference vector pointing along positive longitude

            // Calculate the dot product between the two vectors
            double dotProduct = vector1.Latitude * vector2.Latitude + vector1.Longitude * vector2.Longitude;

            // Calculate the magnitudes of the vectors
            double magnitude1 = Math.Sqrt(vector1.Latitude * vector1.Latitude + vector1.Longitude * vector1.Longitude);
            double magnitude2 = Math.Sqrt(vector2.Latitude * vector2.Latitude + vector2.Longitude * vector2.Longitude);

            // Calculate the angle between the vectors in radians
            double angleInRadians = Math.Acos(dotProduct / (magnitude1 * magnitude2));

            // Convert the angle from radians to degrees
            double angleInDegrees = angleInRadians * (180.0 / Math.PI);

            return angleInDegrees;
        }
        
        public double DistanceTo(Coordinate other)
        {
            return Math.Sqrt(Math.Pow(Latitude - other.Latitude, 2) + Math.Pow(Longitude - other.Longitude, 2));
        }
    }
}