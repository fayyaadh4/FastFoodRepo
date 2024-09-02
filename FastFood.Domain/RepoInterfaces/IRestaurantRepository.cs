using FastFood.Dto;
using FastFood.Domain.Entities;

namespace FastFood.Domain.Interfaces
{
    public interface IRestaurantRepository
    {
        Task<ICollection<Restaurant>> GetRestaurants();
        Task<Restaurant> GetRestaurant(int id);
        Task<bool> RestaurantExists(int id);
        Task<ICollection<MenuItem>> GetMenuItemsByRestaurant(int restaurantId);
        Task<bool> CreateRestaurant (Restaurant restaurant);

        Task<bool> Save();

        Task<bool> UpdateRestaurant(Restaurant restaurant);
        Task<bool> DeleteRestaurant(Restaurant restaurant);
    }
}
