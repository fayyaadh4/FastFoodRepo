
using FastFood.Domain.Entities;
using FastFood.Dto;

namespace FastFood.Domain.ServiceInterfaces
{
    public interface IRoleService
    {
        Task<EmployeeRoleDto?> CheckDuplicateRole(EmployeeRoleDto role);
    }
}
