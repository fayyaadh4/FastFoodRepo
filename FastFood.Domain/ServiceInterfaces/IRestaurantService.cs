
using FastFood.Domain.Entities;
using FastFood.Dto;

namespace FastFood.Domain.ServiceInterfaces
{
    public interface IRestaurantService
    {
        Task<ICollection<RestaurantDto>> GetRestaurants();
        Task<RestaurantDto> GetRestaurant(int id);
        Task<ICollection<MenuItemDto>> GetMenuItemsByRestaurant(int restaurantId);
        Task<bool> CreateRestaurant(RestaurantDto restaurant);


        Task<bool> UpdateRestaurant(int restaurantId, RestaurantDto restaurant);
        Task<bool> DeleteRestaurant(int restaurantId);
        Task<RestaurantDto> CheckDuplicateRestaurant(RestaurantDto restaurant);
    }
}
