using AutoMapper;
using FakeItEasy;
using FastFood.Controllers;
using FastFood.Dto;
using FastFood.Interfaces;
using FastFood.Models;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastFood.Test.Controller
{
    public class RestaurantControllerTests
    {
        private readonly IMenuItemRepository _menuItemRepository;
        private readonly IRestaurantRepository _restaurantRepository;
        private readonly IMapper _mapper;

        // real repositories not brought in as we're faking them
        public RestaurantControllerTests()
        {
            _menuItemRepository = A.Fake<IMenuItemRepository>();
            _restaurantRepository = A.Fake<IRestaurantRepository>();
            _mapper = A.Fake<IMapper>();
        }

        [Fact]
        public void RestaurantController_GetRestaurants_ReturnsOk()
        {
            // Arrange: Get variables, functions, etc.
            var restaurantsRepo = A.Fake<ICollection<RestaurantDto>>();
            var restaurants = A.Fake<List<Restaurant>>();
            A.CallTo(() => _mapper.Map<List<Restaurant>>(restaurantsRepo))
                .Returns(restaurants);
            var controller = new RestaurantController
                (
                    _restaurantRepository,
                    _menuItemRepository,
                    _mapper
                );

            // Act: perform action
            var result = controller.GetRestaurants();

            // Assert: What outcome is expected
            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(OkObjectResult));
        }

        [Fact]
        public void RestaurantController_GetRestaurant_ReturnsOk()
        {
            // Arrange
            var id = 1;
            var restaurantRepo = A.Fake<RestaurantDto>();
            var restaurant = A.Fake<RestaurantDto>();
            A.CallTo(() => _restaurantRepository.RestaurantExists(id))
                .Returns(true);
            A.CallTo(() => _mapper.Map<RestaurantDto>(restaurantRepo))
                .Returns(restaurant);
            var controller = new RestaurantController
                (
                    _restaurantRepository,
                    _menuItemRepository,
                    _mapper
                );

            // Act
            var result = controller.GetRestaurant(id);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(OkObjectResult));


        }

        [Fact]
        public void RestaurantController_GetMenuItemsByRestaurant_ReturnsOk()
        {
            // Arrange
            var id = 1;
            var restaurantRepo = A.Fake<RestaurantDto>();
            var restaurant = A.Fake<RestaurantDto>();
            var menuItemsRepo = A.Fake<ICollection<MenuItem>>();
            var menuItems = A.Fake<List<MenuItem>>();
            A.CallTo(() => _restaurantRepository.RestaurantExists(id))
                .Returns(true);
            A.CallTo(() => _restaurantRepository.GetMenuItemsByRestaurant(id))
                .Returns(menuItems);

            var controller = new RestaurantController
                (
                    _restaurantRepository,
                    _menuItemRepository,
                    _mapper
                );

            // Act
            var result = controller.GetMenuItemsByRestaurant(id);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(OkObjectResult));


        }

        [Fact]
        public void RestaurantController_CreateRestaurant_ReturnsOk()
        {
            // arrange
            var createRestaurant = A.Fake<RestaurantDto>();
            // var restaurantExists = null;
            var restaurantMap = A.Fake<Restaurant>();
            A.CallTo(() => _restaurantRepository.CheckDuplicateRestaurant(createRestaurant))
            .Returns(null);
            A.CallTo(() => _mapper.Map<Restaurant>(createRestaurant))
            .Returns(restaurantMap);
            A.CallTo(() => _restaurantRepository.CreateRestaurant(restaurantMap))
            .Returns(true);
            var controller = new RestaurantController
                (
                    _restaurantRepository,
                    _menuItemRepository,
                    _mapper
                );

            // act
            var result = controller.CreateRestaurant(createRestaurant);

            //assert
            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(OkObjectResult));

        }

        [Fact]
        public void RestaurantController_UpdateRestaurant_ReturnsOk()
        {
            // arrange
            var id = 1;
            var restaurantUpdate = A.Fake<RestaurantDto>();
            var restaurantMap = A.Fake<Restaurant>();
            A.CallTo(() => _restaurantRepository.RestaurantExists(id))
                .Returns(true);
            restaurantUpdate.Id = 1;
            A.CallTo(() => _mapper.Map<Restaurant>(restaurantUpdate))
                .Returns(restaurantMap);
            A.CallTo(() => _restaurantRepository.UpdateRestaurant(restaurantMap))
                .Returns(true);
            var controller = new RestaurantController
                (
                    _restaurantRepository,
                    _menuItemRepository,
                    _mapper
                );

            // act
            var result = controller.UpdateRestaurant(id, restaurantUpdate);

            //asert
            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(NoContentResult));

        }


        [Fact]
        public void RestaurantController_DeleteRestaurant_ReturnsOk()
        {
            // arrange
            var id = 1;
            var restaurantToDelete = A.Fake<Restaurant>();
            var menuItemsToDelete = A.Fake<ICollection<MenuItem>>();
            var restaurantMap = A.Fake<Restaurant>();
            A.CallTo(() => _restaurantRepository.RestaurantExists(id))
                .Returns(true);
            A.CallTo(() => _menuItemRepository.GetMenuItemsByRestaurant(id))
                .Returns(menuItemsToDelete);
            A.CallTo(() => _restaurantRepository.GetRestaurant(id))
                .Returns(restaurantToDelete);
            A.CallTo(() => _menuItemRepository.DeleteMenuItems(menuItemsToDelete.ToList()))
                .Returns(true);
            A.CallTo(() => _restaurantRepository.DeleteRestaurant(restaurantToDelete))
                .Returns(true);
            var controller = new RestaurantController
                (
                    _restaurantRepository,
                    _menuItemRepository,
                    _mapper
                );

            // act
            var result = controller.DeleteRestaurant(id);

            //asert
            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(NoContentResult));

        }
    }
}
