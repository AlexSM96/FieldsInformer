using Point = FieldInformer.Domain.Models.Point;
using Location = FieldInformer.Domain.Models.Location;

namespace FieldInformer.Application.Extensions;

public static class DtoMapExtension
{
    public static PointDto ToPointDto(this Point point) =>
         new PointDto(point.Latitude, point.Longitude);
    
    public static FieldDto ToFieldDto(this Field field, Centroid centroid) =>
        new FieldDto(field.Id, field.Name, field.Size, field.Locations.ToLocationDto(centroid));
    
    public static LocationDto ToLocationDto(this Location location, Centroid centroid) =>
        new LocationDto(centroid.Coordiantes.ToPointDto(), location.Polygon.ToPolygonDto());

    public static PointInFieldDto ToPointInFieldResultDto(this Field field) =>
        new PointInFieldDto(field.Id, field.Name);

    public static List<PointDto> ToPolygonDto(this List<Point> polygon) =>
        polygon.Select(x => x.ToPointDto()).ToList();
}
