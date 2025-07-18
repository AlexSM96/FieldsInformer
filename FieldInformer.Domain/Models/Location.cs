namespace FieldInformer.Domain.Models;

public class Location
{
    public Point Center { get; set; } 

    public List<Point> Polygon { get; set; } = [];
}
