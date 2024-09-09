using AutoMapper;
using FakeItEasy;
using FastFood.Application.Services;
using FastFood.Controllers;
using FastFood.Domain.Entities;
using FastFood.Domain.Interfaces;
using FastFood.Domain.ServiceInterfaces;
using FastFood.Dto;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastFood.Test.Services
{
    public class RestaurantServiceTests
    {
        private readonly IMenuItemRepository _menuItemRepository;
        private readonly IRestaurantRepository _restaurantRepository;
        private readonly IRestaurantService _restaurantService;
        private readonly IMapper _mapper;

        // real repositories not brought in as we're faking them
        public RestaurantServiceTests()
        {
            _menuItemRepository = A.Fake<IMenuItemRepository>();
            _restaurantRepository = A.Fake<IRestaurantRepository>();
            _restaurantService = A.Fake<IRestaurantService>();
            _mapper = A.Fake<IMapper>();
        }

        [Fact]
        public async Task RestaurantService_GetRestaurants_ReturnsRestaurants()
        {
            // Arrange: Get variables, functions, etc.
            var restaurantsRepo = A.Fake<ICollection<RestaurantDto>>();
            var restaurants = A.Fake<List<Restaurant>>();
            A.CallTo(() => _mapper.Map<List<Restaurant>>(restaurantsRepo))
                .Returns(restaurants);
            var controller = new RestaurantService
                (
                    _restaurantRepository,
                    _menuItemRepository,
                    _mapper
                );

            // Act: perform action
            var result = await controller.GetRestaurants();

            // Assert: What outcome is expected
            result.Should().NotBeNull();
        }

        [Fact]
        public async Task RestaurantService_GetRestaurant_ReturnsRestaurant()
        {
            // Arrange
            var id = 1;
            var restaurantRepo = A.Fake<RestaurantDto>();
            var restaurant = A.Fake<RestaurantDto>();
            A.CallTo(() => _restaurantRepository.RestaurantExists(id))
                .Returns(true);
            A.CallTo(() => _mapper.Map<RestaurantDto>(restaurantRepo))
                .Returns(restaurant);
            var controller = new RestaurantService
                (
                    _restaurantRepository,
                    _menuItemRepository,
                    _mapper
                );

            // Act
            var result = await controller.GetRestaurant(id);

            // Assert
            result.Should().NotBeNull();


        }

        [Fact]
        public async Task RestaurantService_GetMenuItemsByRestaurant_ReturnsMenuItems()
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

            var controller = new RestaurantService
                (
                    _restaurantRepository,
                    _menuItemRepository,
                    _mapper
                );

            // Act
            var result = await controller.GetMenuItemsByRestaurant(id);

            // Assert
            result.Should().NotBeNull();


        }

        [Fact]
        public async Task RestaurantService_CreateRestaurant_ReturnsTrue()
        {
            // arrange
            var createRestaurant = A.Fake<RestaurantDto>();
            // var restaurantExists = null;
            var restaurantMap = A.Fake<Restaurant>();
            RestaurantDto nullRestaurant = null;
            A.CallTo(() => _restaurantService.CheckDuplicateRestaurant(createRestaurant))
            .Returns(nullRestaurant);
            A.CallTo(() => _mapper.Map<Restaurant>(createRestaurant))
            .Returns(restaurantMap);
            A.CallTo(() => _restaurantRepository.CreateRestaurant(restaurantMap))
            .Returns(true);
            var controller = new RestaurantService
                (
                    _restaurantRepository,
                    _menuItemRepository,
                    _mapper
                );

            // act
            var result = await controller.CreateRestaurant(createRestaurant);

            //assert
            result.Should().BeTrue();

        }

        [Fact]
        public async Task RestaurantService_UpdateRestaurant_ReturnsTrue()
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
            var controller = new RestaurantService
                (
                    _restaurantRepository,
                    _menuItemRepository,
                    _mapper
                );

            // act
            var result = await controller.UpdateRestaurant(id, restaurantUpdate);

            //asert
            result.Should().BeTrue();

        }


        [Fact]
        public async Task RestaurantService_DeleteRestaurant_ReturnsTrue()
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
            var controller = new RestaurantService
                (
                    _restaurantRepository,
                    _menuItemRepository,
                    _mapper
                );

            // act
            var result = await controller.DeleteRestaurant(id);

            //asert
            result.Should().BeTrue();

        }
    }
}
