using FastFood.Dto;
using FastFood.Models;

namespace FastFood.Interfaces
{
    public interface IRestaurantRepository
    {
        ICollection<Restaurant> GetRestaurants();
        Restaurant GetRestaurant(int id);
        bool RestaurantExists(int id);
        ICollection<MenuItem> GetMenuItemsByRestaurant(int restaurantId);
        bool CreateRestaurant (Restaurant restaurant);

        bool Save();

        bool UpdateRestaurant(Restaurant restaurant);
        bool DeleteRestaurant(Restaurant restaurant);
        Restaurant CheckDuplicateRestaurant(RestaurantDto restaurant);
    }
}
