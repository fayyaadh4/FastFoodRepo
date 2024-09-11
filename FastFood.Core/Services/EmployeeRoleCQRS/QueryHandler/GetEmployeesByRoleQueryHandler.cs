using AutoMapper;
using FastFood.Core.Services.EmployeeRoleCQRS.Queries;
using FastFood.Domain.Interfaces;
using FastFood.Domain.RepoInterfaces;
using FastFood.Dto;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastFood.Core.Services.EmployeeRoleCQRS.QueryHandler
{
    public class GetEmployeesByRoleQueryHandler : IRequestHandler<GetEmployeesByRoleQuery, ICollection<EmployeeDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetEmployeesByRoleQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<ICollection<EmployeeDto>> Handle(GetEmployeesByRoleQuery request, CancellationToken cancellationToken)
        {
            if (!await _unitOfWork.EmployeeRole.Exists(request.EmployeeRoleId))
                throw new Exception("Role not found");

            var employees = _mapper.Map<List<EmployeeDto>>(await _unitOfWork.EmployeeRole.GetEmployeesByRole(request.EmployeeRoleId));


            return employees;
        }
    }
}
