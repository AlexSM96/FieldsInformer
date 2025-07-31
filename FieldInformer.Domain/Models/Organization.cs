namespace FieldInformer.Domain.Models
{
    public class Organization
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public ICollection<Department> Departments { get; set; } = new List<Department>();
    }
}
