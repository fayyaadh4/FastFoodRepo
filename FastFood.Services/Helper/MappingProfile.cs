using AutoMapper;
using FastFood.Domain.Entities;
using FastFood.Dto;

namespace FastFood.Helper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Restaurant, RestaurantDto>()
                .ReverseMap();
            CreateMap<MenuItem, MenuItemDto>()
                .ReverseMap();
            CreateMap<Location,  LocationDto>()
                .ReverseMap();
            CreateMap<Employee, EmployeeDto>()
                .ReverseMap();
            CreateMap<EmployeeRole, EmployeeRoleDto>()
                .ReverseMap();
        }
    }
}
