using FieldInformer.Domain.Models.Base;

namespace FieldInformer.Domain.Models;

public class Centroid : BaseModel
{
    public Point Coordiantes { get; set; }
}
