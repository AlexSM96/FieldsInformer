namespace FieldInformer.WebAPI.Controllers;

[ApiController]
[Route("fieldinfo/api/")]
public class FieldInfoController(IFieldInformerService service) : Controller
{
    [HttpGet("getfields")]
    public IActionResult GetFields()
    {
        var fields = service.GetFields();
        if(fields == null || !fields.Any())
        {
            return NoContent();
        }

        return Ok(fields);
    }

    [HttpGet("getfieldarea/{fieldId:long}")]
    public IActionResult GetFieldArea([FromRoute] long fieldId)
    {
        var result = service.GetFieldArea(fieldId);
        if(result.Exception is not null)
        {
            return BadRequest(new { Error = result.Exception.ToString() });    
        }

        return Ok(new { Size = result.Value });
    }

    [HttpGet("getdistancefromcentertopointinmeters/{fieldId:long}")]
    public IActionResult GetDistanceFromCenterToPointInMeters([FromRoute] long fieldId, [FromQuery] PointDto pointDto)
    {
        var result = service.GetDistanceFromCenterToPointInMeters(fieldId, pointDto);
        if (result.Exception is not null) 
        {
            return BadRequest(new { Error = result.Exception.ToString() });
        }

        return Ok(new { Distance = result.Value });
    }

    [HttpGet("checkpointinfield")]
    public IActionResult CheckPointInField([FromQuery] PointDto pointDto)
    {
        var result = service.CheckPointInField(pointDto);
        if (result.Exception is not null)
        {
            return BadRequest(new { Error = result.Exception.ToString() });
        }

        if (result.Value is null)
        {
            return Ok(new { Result = false });
        }

        return Ok(new { Field = result.Value });
    }
}
