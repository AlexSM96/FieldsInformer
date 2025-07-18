namespace FieldInformer.Domain.Models;

public struct Point
{
    private double _latitude;
    private double _longitude;

    public Point(double lat, double lgt)
    {
        Latitude = lat;
        Longitude = lgt;
    }

    public double Latitude
    {
        get => _latitude;
        set
        {
            if (value < -90 || value > 90)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(value), $"Latitude must be between -90 and 90 degrees.");
            }

            _latitude = value;
        }
    }

    public double Longitude
    {
        get => _longitude;
        set
        {
            if (value < -180 || value > 180)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(value), $"Longitude must be between -180 and 180 degrees.");
            }

            _longitude = value;
        }
    }

    public double GetDistanceToPointInMeters(Point point)
    {
        const double R = 6371 * 1000;

        var dLat = ToRadians(point.Latitude - Latitude);
        var dLon = ToRadians(point.Longitude - Longitude);

        var a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) 
            + Math.Cos(ToRadians(Latitude)) * Math.Cos(ToRadians(Latitude)) 
            * Math.Sin(dLon / 2) * Math.Sin(dLon / 2);

        var c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
        return R * c;
    }


    public bool IsInside(Field field)
    {
        int intersections = 0;
        var polygon = field.Locations.Polygon;
        for (int i = 0; i < polygon.Count; i++)
        {
            var currentEdge = polygon[i];
            var nextEdge = polygon[(i + 1) % polygon.Count];

            if ((currentEdge.Latitude > Latitude && nextEdge.Latitude <= Latitude) ||
                (currentEdge.Latitude <= Latitude && nextEdge.Latitude > Latitude))
            {
                double xinters = (Latitude - currentEdge.Latitude) * (nextEdge.Longitude - currentEdge.Longitude) / (nextEdge.Latitude - currentEdge.Latitude) + currentEdge.Longitude;
                if (xinters >= Longitude) intersections++;
            }
        }

        return intersections % 2 != 0;
    }

    private static double ToRadians(double degrees)
    {
        return degrees * Math.PI / 180;
    }
}
