using AutoMapper;
using FastFood.Core.Services.EmployeeRoleCQRS.Commands;
using FastFood.Domain.Entities;
using FastFood.Domain.Interfaces;
using FastFood.Domain.RepoInterfaces;
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
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateRoleCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<bool> Handle(UpdateRoleCommand request, CancellationToken cancellationToken)
        {
            if (request.UpdateEmployeeRole == null)
                throw new Exception("Body cannot be null");

            if (request.EmpRoleId != request.UpdateEmployeeRole.Id)
                throw new Exception("ID mismatch");

            if (!await _unitOfWork.EmployeeRole.Exists(request.EmpRoleId))
                throw new Exception("Role already exists");

            var roleMap = _mapper.Map<EmployeeRole>(request.UpdateEmployeeRole);


            await _unitOfWork.EmployeeRole.Update(roleMap);
            return await _unitOfWork.CompleteAsync();

        }
    }
}
