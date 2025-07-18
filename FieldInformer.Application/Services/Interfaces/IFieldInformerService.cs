namespace FieldInformer.Application.Services.Interfaces;

public interface IFieldInformerService
{
    public IEnumerable<FieldDto> GetFields();

    public Result<double> GetFieldArea(long fieldId);

    public Result<double> GetDistanceFromCenterToPointInMeters(long fieldId, PointDto pointDto);

    public Result<PointInFieldDto> CheckPointInField(PointDto pointDto);
}
