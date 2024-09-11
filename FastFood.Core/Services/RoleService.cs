using AutoMapper;
using FastFood.Domain.Entities;
using FastFood.Domain.Interfaces;
using FastFood.Domain.RepoInterfaces;
using FastFood.Domain.ServiceInterfaces;
using FastFood.Dto;

namespace FastFood.Application.Services
{
    public class RoleService : IRoleService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public RoleService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<EmployeeRoleDto?> CheckDuplicateRole(EmployeeRoleDto role)
        {
            return _mapper.Map<List<EmployeeRoleDto>>(await _unitOfWork.EmployeeRole.GetAll())
                .Where(r => r.Name == role.Name)
                .FirstOrDefault();
        }
    }
}
