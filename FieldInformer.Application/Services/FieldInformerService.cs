namespace FieldInformer.Application.Services;

public class FieldInformerService : IFieldInformerService
{
    private readonly BaseModelParser<Field> _fields;
    private readonly BaseModelParser<Centroid> _centroids;
    private readonly IConfiguration _configuration;

    public FieldInformerService(IConfiguration configuration)
    {
        _configuration = configuration;
        _fields = new FieldParser(_configuration.GetRequiredSection(typeof(Field).Name).Value!);
        _centroids = new CentroidParser(_configuration.GetRequiredSection(typeof(Centroid).Name).Value!);
    }

    public IEnumerable<FieldDto> GetFields()
    {
        foreach (var field in _fields.ParsedItems)
        {
            var centroid = _centroids.ParsedItems.FirstOrDefault(c => c.Id == field.Id);
            if(centroid == null)
            {
                continue;
            }

            yield return field.ToFieldDto(centroid);
        }
    }

    public Result<double> GetFieldArea(long fieldId)
    {
        var result = new Result<double>();
        try
        {
            var field = _fields.ParsedItems.FirstOrDefault(x => x.Id == fieldId);
            if (field == null)
            {
                throw new NotFoundException($"{nameof(Field)} ID = {fieldId}");
            }

            result.Value = field.Size;
            return result;
        }
        catch(Exception ex) 
        {
            result.Exception = ex;
            return result;
        }
    }

    public Result<PointInFieldDto> CheckPointInField(PointDto pointDto)
    {
        var result = new Result<PointInFieldDto>();
        try
        {
            var point = new Domain.Models.Point(pointDto.Latitude, pointDto.Longitude);
            foreach (var field in _fields.ParsedItems)
            {
                if (point.IsInside(field))
                {
                    result.Value = field.ToPointInFieldResultDto();
                    return result;
                }
            }

            return result;
        }
        catch(Exception ex)
        {
            result.Exception = ex;
            return result;
        }
    }

    public Result<double> GetDistanceFromCenterToPointInMeters(long fieldId, PointDto pointDto)
    {
        var result = new Result<double>();
        try
        {
            var field = _fields.ParsedItems.FirstOrDefault(x => x.Id == fieldId);
            if (field == null)
            {
                throw new NotFoundException($"{nameof(Field)} ID = {fieldId}");
            }

            result.Value = field.Locations.Center
                .GetDistanceToPointInMeters(new Domain.Models.Point(pointDto.Latitude, pointDto.Longitude));

            return result;
        }
        catch (Exception ex) 
        {
            result.Exception = ex;
            return result;
        }
    }
}
