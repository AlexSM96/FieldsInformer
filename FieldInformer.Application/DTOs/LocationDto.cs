namespace FieldInformer.Application.DTOs;

public record LocationDto(PointDto Center, List<PointDto> Polygon);
