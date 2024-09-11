using AutoMapper;
using FastFood.Core.Services.EmployeeRoleCQRS.Commands;
using FastFood.Domain.Entities;
using FastFood.Domain.Interfaces;
using FastFood.Domain.RepoInterfaces;
using FastFood.Domain.ServiceInterfaces;
using FastFood.Dto;
using MediatR;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastFood.Core.Services.EmployeeRoleCQRS.CommandHandler
{
    public class CreateRoleCommandHandler : IRequestHandler<CreateRoleCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRoleService _roleService;
        private readonly IMapper _mapper;

        public CreateRoleCommandHandler(IUnitOfWork unitOfWork,
            IRoleService roleService,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _roleService = roleService;
            _mapper = mapper;
        }
        public async Task<bool> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
        {
            if (request.EmployeeRole == null)
                throw new Exception("Body cannot be null");

            var roleExists = await _roleService.CheckDuplicateRole(request.EmployeeRole);

            if (roleExists != null)
            {
                throw new Exception("Role already exists");
            }

            var roleMap = _mapper.Map<EmployeeRole>(request.EmployeeRole);

            await _unitOfWork.EmployeeRole.Update(roleMap);
            return await _unitOfWork.CompleteAsync();

        }
    }
}
