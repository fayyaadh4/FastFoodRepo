using FastFood.Domain.Entities;

namespace FastFood.Dto
{
    public class EmployeeRoleDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public decimal Salary { get; set; }
    }
}
