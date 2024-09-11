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
    public class GetRoleQueryHandler : IRequestHandler<GetRoleQuery, EmployeeRoleDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetRoleQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<EmployeeRoleDto> Handle(GetRoleQuery request, CancellationToken cancellationToken)
        {
            if (!await _unitOfWork.EmployeeRole.Exists(request.EmployeeRoleId))
                throw new Exception("Role not found");

            var role = _mapper.Map<EmployeeRoleDto>(await _unitOfWork.EmployeeRole.GetById(request.EmployeeRoleId));


            return role;
        }
    }
}
