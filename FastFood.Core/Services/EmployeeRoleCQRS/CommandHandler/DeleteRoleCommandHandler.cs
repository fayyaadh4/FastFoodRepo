using AutoMapper;
using FastFood.Core.Services.EmployeeRoleCQRS.Commands;
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
    public class DeleteRoleCommandHandler : IRequestHandler<DeleteRoleCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DeleteRoleCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<bool> Handle(DeleteRoleCommand request, CancellationToken cancellationToken)
        {
            if (!await _unitOfWork.EmployeeRole.Exists(request.EmpRoleId))
                throw new Exception("Role not found");

            var roleToDelete = await _unitOfWork.EmployeeRole.GetById(request.EmpRoleId);


            await _unitOfWork.EmployeeRole.Remove(roleToDelete);
            return await _unitOfWork.CompleteAsync();

        }
    }
}
