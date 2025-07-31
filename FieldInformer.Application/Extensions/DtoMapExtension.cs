using FieldInformer.Application.DTOs;
using FieldInformer.Domain.Enums;
using FieldInformer.Domain.Models;
using System;
using Location = FieldInformer.Domain.Models.Location;
using Point = FieldInformer.Domain.Models.Point;

namespace FieldInformer.Application.Extensions;

public static class DtoMapExtension
{
    public static PointDto ToPointDto(this Point point) =>
         new PointDto(point.Latitude, point.Longitude);
    
    public static FieldDto ToFieldDto(this Field field, Centroid centroid) =>
        new FieldDto(field.Id, field.Name, field.Size, field.Locations.ToLocationDto(centroid), field.Organization.ToOrganizationDto());
    
    public static LocationDto ToLocationDto(this Location location, Centroid centroid) =>
        new LocationDto(centroid.Coordiantes.ToPointDto(), location.Polygon.ToPolygonDto());

    public static PointInFieldDto ToPointInFieldResultDto(this Field field) =>
        new PointInFieldDto(field.Id, field.Name);

    public static List<PointDto> ToPolygonDto(this List<Point> polygon) =>
        polygon.Select(x => x.ToPointDto()).ToList();

    public static OrganizationDto ToOrganizationDto(this Organization organization) =>
        new OrganizationDto(organization.Id, organization.Name, organization.Departments.ToDepartmentsDto());

    public static List<DepartmetDto> ToDepartmentsDto(this IEnumerable<Department> departments) =>
       departments.Select(dep => new DepartmetDto(dep.Id, dep.Name)).ToList();

    public static Organization ToOrganization(this Organizations organuzationEnum, Array departments)
    {
        var organization = new Organization();
        organization.Id = (long)organuzationEnum;
        organization.Name = organuzationEnum.ToString();

        foreach(var department in departments)
        {
            organization.Departments.Add(new Department()
            {
                Id = Array.IndexOf(departments, department) + 1,
                Name = department.ToString()!
            });
        }
       
        return organization;
    }
}
