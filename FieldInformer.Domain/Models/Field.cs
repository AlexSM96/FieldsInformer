using FieldInformer.Domain.Models.Base;

namespace FieldInformer.Domain.Models;

public class Field : BaseModel
{
    public Location Locations { get; set; } = new();
}
