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
    public class GetEmployeesQueryHandler : IRequestHandler<GetEmployeesQuery, ICollection<EmployeeDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetEmployeesQueryHandler(
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<ICollection<EmployeeDto>> Handle(GetEmployeesQuery request, CancellationToken cancellationToken)
        {
            var employees = _mapper.Map<List<EmployeeDto>>(await _unitOfWork.Employee.GetAll());


            return employees;
        }
    }
}
