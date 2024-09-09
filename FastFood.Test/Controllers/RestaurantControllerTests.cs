using AutoMapper;
using FakeItEasy;
using FastFood.Controllers;
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

namespace FastFood.Test.Controller
{
    public class RestaurantControllerTests
    {/*
        private readonly IRestaurantService _restaurantService;

        // real repositories not brought in as we're faking them
        public RestaurantControllerTests()
        {
            _restaurantService = A.Fake<IRestaurantService>();
        }

        [Fact]
        public async Task RestaurantController_GetRestaurants_ReturnsOk()
        {
            // Arrange: Get variables, functions, etc.
            var controller = new RestaurantController
                (
                    _restaurantService
                );

            // Act: perform action
            var result = await controller.GetRestaurants();

            // Assert: What outcome is expected
            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(OkObjectResult));
        }

        [Fact]
        public async Task RestaurantController_GetRestaurant_ReturnsOk()
        {
            // Arrange
            var id = 1;
            var controller = new RestaurantController
                (
                    _restaurantService
                );

            // Act
            var result = await controller.GetRestaurant(id);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(OkObjectResult));


        }

        [Fact]
        public async Task RestaurantController_GetMenuItemsByRestaurant_ReturnsOk()
        {
            // Arrange
            var id = 1;
            var controller = new RestaurantController
                (
                    _restaurantService
                );

            // Act
            var result = await controller.GetMenuItemsByRestaurant(id);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(OkObjectResult));


        }

        [Fact]
        public async Task RestaurantController_CreateRestaurant_ReturnsOk()
        {
            // arrange
            var createRestaurant = A.Fake<RestaurantDto>();
            var controller = new RestaurantController
                (
                    _restaurantService
                );

            // act
            var result = await controller.CreateRestaurant(createRestaurant);

            //assert
            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(OkObjectResult));

        }

        [Fact]
        public async Task RestaurantController_UpdateRestaurant_ReturnsOk()
        {
            // arrange
            var id = 1;
            var restaurantUpdate = A.Fake<RestaurantDto>();
            var controller = new RestaurantController
                (
                    _restaurantService
                );

            // act
            var result = await controller.UpdateRestaurant(id, restaurantUpdate);

            //asert
            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(NoContentResult));

        }


        [Fact]
        public async Task RestaurantController_DeleteRestaurant_ReturnsOk()
        {
            // arrange
            var id = 1;
            var restaurantToDelete = A.Fake<RestaurantDto>();
            var controller = new RestaurantController
                (
                    _restaurantService
                );

            // act
            var result = await controller.DeleteRestaurant(id);

            //asert
            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(NoContentResult));

        }
   */ }
}
