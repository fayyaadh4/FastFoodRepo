using AutoMapper;
using FastFood.Core.Services.EmployeeCQRS.Commands;
using FastFood.Domain.Entities;
using FastFood.Domain.Interfaces;
using FastFood.Domain.RepoInterfaces;
using FastFood.Domain.ServiceInterfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastFood.Core.Services.EmployeeCQRS.CommandHandler
{
    public class DeleteEmployeeCommandHandler : IRequestHandler<DeleteEmployeeCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DeleteEmployeeCommandHandler(
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<bool> Handle(DeleteEmployeeCommand request, CancellationToken cancellationToken)
        {
            if (!await _unitOfWork.Employee.Exists(request.EmployeeId))
                throw new Exception("Employee not found");

            var employeeToDelete = await _unitOfWork.Employee.GetById(request.EmployeeId);

            await _unitOfWork.Employee.Remove(employeeToDelete);
            return await _unitOfWork.CompleteAsync();

        }
    }
}
