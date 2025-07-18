using Point = SharpKml.Dom.Point;

namespace FieldInformer.Application.KMLParser;

public class CentroidParser(string path) : BaseModelParser<Centroid>(path)
{
    protected override Centroid Parse(Placemark placemark)
    {
        var centroid = base.Parse(placemark);
        if (placemark.Geometry is Point point)
        {
            centroid.Coordiantes = new(point.Coordinate.Latitude, point.Coordinate.Longitude);
        }

        return centroid;
    }
}