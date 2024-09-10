
using FastFood.Domain.Entities;
using FastFood.Dto;

namespace FastFood.Domain.ServiceInterfaces
{
    public interface IRestaurantService
    {
        Task<RestaurantDto?> CheckDuplicateRestaurant(RestaurantDto restaurant);
    }
}
