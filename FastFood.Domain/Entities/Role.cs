namespace FastFood.Domain.Entities
{
    public class EmployeeRole
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public decimal Salary  { get; set; }
        public required ICollection<Employee> Employees { get; set; }
    }
}
