using FastFood.Domain.Entities;

namespace FastFood.Dto
{
    public class EmployeeLeaveDto
    {
        public int Id { get; set; }
        public int YearsAtCompany { get; set; }
        public int TotalLeave { get; set; }
        public int LeaveTaken { get; set; }
        public int LeaveAccruedPerMonth { get; set; }
        public int CurrentLeave { get; set; }
        public int EmployeeId { get; set; }
    }
}
