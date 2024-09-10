using AutoMapper;
using FastFood.Core.Services.EmployeeRoleCQRS.Commands;
using FastFood.Domain.Entities;
using FastFood.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastFood.Core.Services.EmployeeRoleCQRS.CommandHandler
{
    public class UpdateRoleCommandHandler : IRequestHandler<UpdateRoleCommand, bool>
    {
        private readonly IEmployeeRoleRepository _roleRepository;
        private readonly IMapper _mapper;

        public UpdateRoleCommandHandler(IEmployeeRoleRepository roleRepository, IMapper mapper)
        {
            _roleRepository = roleRepository;
            _mapper = mapper;
        }
        public async Task<bool> Handle(UpdateRoleCommand request, CancellationToken cancellationToken)
        {
            if (request.UpdateEmployeeRole == null)
                throw new Exception("Body cannot be null");

            if (request.EmpRoleId != request.UpdateEmployeeRole.Id)
                throw new Exception("ID mismatch");

            if (!await _roleRepository.RoleExists(request.EmpRoleId))
                throw new Exception("Role already exists");

            var roleMap = _mapper.Map<EmployeeRole>(request.UpdateEmployeeRole);


            return await _roleRepository.UpdateRole(roleMap);

        }
    }
}
