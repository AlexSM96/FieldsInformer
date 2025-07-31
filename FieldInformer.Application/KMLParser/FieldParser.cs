using FieldInformer.Domain.Enums;
using Point = FieldInformer.Domain.Models.Point;

namespace FieldInformer.Application.KMLParser;

public class FieldParser(string path) : BaseModelParser<Field>(path)
{
    protected override Field Parse(Placemark placemark)
    {
        var field = base.Parse(placemark);

        field.Organization = field.Id % 2 == 0
            ? Organizations.Rassvet.ToOrganization(Enum.GetValues<RassvetDeps>())
            : Organizations.Zaria.ToOrganization(Enum.GetValues<ZariaDeps>());

        if (placemark.Geometry is Polygon polygon)
        {
            foreach (var vetor in polygon.OuterBoundary.LinearRing.Coordinates)
            {
                if (vetor is null) continue;
                field.Locations.Polygon.Add(new Point(vetor.Latitude, vetor.Longitude));
            }
        }

        return field;
    }
}
