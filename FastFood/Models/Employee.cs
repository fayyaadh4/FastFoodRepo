namespace FastFood.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string Gender {  get; set; } = string.Empty;
        public string? EmailAddress { get; set; }
        public string? PhoneNumber { get; set; }
        public Location? Location { get; set; } 
        public DateOnly? DateOfBirth { get; set; }
        public int RoleId { get; set; }
        public Role? Role { get; set; }
        public EmployeeLeave? EmployeeLeave { get; set; }
        public int RestaurantId { get; set; }
        public Restaurant? Restaurant { get; set; }

    }
}
