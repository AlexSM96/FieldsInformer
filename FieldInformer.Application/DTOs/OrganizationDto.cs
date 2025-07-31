namespace FieldInformer.Application.DTOs
{
    public record OrganizationDto(long Id, string Name, List<DepartmetDto> Departmets);

}