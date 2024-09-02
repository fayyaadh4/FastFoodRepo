using AutoMapper;
using FastFood.Dto;
using FastFood.Domain.Entities;

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
            CreateMap<Role, RoleDto>()
                .ReverseMap();
        }
    }
}
