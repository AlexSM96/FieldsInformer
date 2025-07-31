namespace FieldInformer.Application.DTOs;

public record FieldDto(long Id, string Name, double Size, LocationDto Locations, OrganizationDto Organization);
