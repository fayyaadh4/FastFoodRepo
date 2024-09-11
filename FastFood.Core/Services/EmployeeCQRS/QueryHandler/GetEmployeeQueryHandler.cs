using AutoMapper;
using FastFood.Core.Services.EmployeeCQRS.Queries;
using FastFood.Domain.Interfaces;
using FastFood.Domain.RepoInterfaces;
using FastFood.Dto;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastFood.Core.Services.EmployeeCQRS.QueryHandler
{
    public class GetEmployeeQueryHandler : IRequestHandler<GetEmployeeQuery, EmployeeDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetEmployeeQueryHandler(
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<EmployeeDto> Handle(GetEmployeeQuery request, CancellationToken cancellationToken)
        {
            if (!await _unitOfWork.Employee.Exists(request.EmployeeId))
                throw new Exception("Employee not found");

            var employee = _mapper.Map<EmployeeDto>(await _unitOfWork.Employee.GetById(request.EmployeeId));


            return employee;
        }
    }
}
