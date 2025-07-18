namespace FieldInformer.Domain.Models.Base;

public class BaseModel
{
    public long Id { get; set; }

    public string Name { get; set; } = null!;

    public double Size { get; set; }
}
