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
    public class GetRolesQueryHandler : IRequestHandler<GetRolesQuery, ICollection<EmployeeRoleDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetRolesQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<ICollection<EmployeeRoleDto>> Handle(GetRolesQuery request, CancellationToken cancellationToken)
        {
            var roles = _mapper.Map<List<EmployeeRoleDto>>(await _unitOfWork.EmployeeRole.GetAll());

            return roles;
        }
    }
}
