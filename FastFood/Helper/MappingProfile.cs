using AutoMapper;
using FastFood.Dto;
using FastFood.Models;

namespace FastFood.Helper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Restaurant, RestaurantDto>();
            CreateMap<RestaurantDto, Restaurant>();
            CreateMap<MenuItem, MenuItemDto>();
            CreateMap<MenuItemDto, MenuItem>();
            CreateMap<Location,  LocationDto>();
        }
    }
}
