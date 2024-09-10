using AutoMapper;
using FastFood.Core.Services.EmployeeRoleCQRS.Commands;
using FastFood.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastFood.Core.Services.EmployeeRoleCQRS.CommandHandler
{
    public class DeleteRoleCommandHandler : IRequestHandler<DeleteRoleCommand, bool>
    {
        private readonly IEmployeeRoleRepository _roleRepository;
        private readonly IMapper _mapper;

        public DeleteRoleCommandHandler(IEmployeeRoleRepository roleRepository, IMapper mapper)
        {
            _roleRepository = roleRepository;
            _mapper = mapper;
        }
        public async Task<bool> Handle(DeleteRoleCommand request, CancellationToken cancellationToken)
        {
            if (!await _roleRepository.RoleExists(request.EmpRoleId))
                throw new Exception("Role not found");

            var roleToDelete = await _roleRepository.GetRole(request.EmpRoleId);


            return await _roleRepository.DeleteRole(roleToDelete);

        }
    }
}
