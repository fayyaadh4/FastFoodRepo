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
    public class MenuItemControllerTests
    {
        private readonly IMenuItemService _menuItemService;

        // real repositories not brought in as we're faking them
        public MenuItemControllerTests()
        {
            _menuItemService = A.Fake<IMenuItemService>();
        }

        [Fact]
        public async Task MenuItemController_GetMenuItems_ReturnsOk()
        {
            // Arrange: Get variables, functions, etc.
            var controller = new MenuItemController
                (
                    _menuItemService
                );

            // Act: perform action
            var result = await controller.GetMenuItems();

            // Assert: What outcome is expected
            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(OkObjectResult));
        }

        [Fact]
        public async Task MenuItemController_GetMenuItem_ReturnsOk()
        {
            // Arrange
            var menuItemId = 1;
            var controller = new MenuItemController
                (
                    _menuItemService
                );

            // Act
            var result = await controller.GetMenuItem(menuItemId);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(OkObjectResult));
            

        }

        [Fact]
        public async Task MenuController_CreateMenuItem_ReturnsOk()
        {
            // arrange
            var createMenuItem = A.Fake<MenuItemDto>();
            var controller = new MenuItemController
                (
                    _menuItemService
                );

            // act
            var result = await controller.CreateMenuItem(createMenuItem);

            //assert
            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(OkObjectResult));

        }

        [Fact]
        public async Task MenuItemController_UpdateMenuItem_ReturnsOk()
        {
            // arrange
            var id = 1;
            var menuItemUpdate = A.Fake<MenuItemDto>();
            var controller = new MenuItemController
                (
                    _menuItemService
                );

            // act
            var result = await controller.UpdateMenuItem(id, menuItemUpdate);

            //asert
            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(NoContentResult));

        }


        [Fact]
        public async Task MenuItemController_DeleteMenuItem_ReturnsOk()
        {
            // arrange
            var id = 1;
            var menuItemToDelete = A.Fake<MenuItemDto>();
            var controller = new MenuItemController
                (
                    _menuItemService
                );

            // act
            var result = await controller.DeleteMenuItem(id);

            //asert
            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(NoContentResult));

        }
    }
}
