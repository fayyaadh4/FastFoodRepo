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
    public class MenuItemControllerTests
    {
        private readonly IMenuItemRepository _menuItemRepository;
        private readonly IRestaurantRepository _restaurantRepository;
        private readonly IMapper _mapper;

        // real repositories not brought in as we're faking them
        public MenuItemControllerTests()
        {
            _menuItemRepository = A.Fake<IMenuItemRepository>();
            _restaurantRepository = A.Fake<IRestaurantRepository>();
            _mapper = A.Fake<IMapper>();
        }

        [Fact]
        public async Task MenuItemController_GetMenuItems_ReturnsOk()
        {
            // Arrange: Get variables, functions, etc.
            var menuItemsRepo = A.Fake<ICollection<MenuItemDto>>();
            var menuItemsListReturned = A.Fake<List<MenuItemDto>>();
            A.CallTo(() => _mapper.Map<List<MenuItemDto>>(menuItemsRepo))
                .Returns(menuItemsListReturned);
            var controller = new MenuItemController
                (
                    _menuItemRepository,
                    _restaurantRepository,
                    _mapper
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
            var menuItemRepo = A.Fake<MenuItemDto>();
            var menuItemReturned = A.Fake<MenuItemDto> ();
            A.CallTo(() => _menuItemRepository.MenuItemExists(menuItemId))
                .Returns(true);
            A.CallTo(() => _mapper.Map<MenuItemDto>(menuItemRepo))
                .Returns(menuItemReturned);
            var controller = new MenuItemController
                (
                    _menuItemRepository,
                    _restaurantRepository,
                    _mapper
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
            // var menuItemExists = null;
            var menuItemMap = A.Fake<MenuItem>();
            MenuItem nullMenuItem = null;
            A.CallTo(() => _restaurantRepository.RestaurantExists(createMenuItem.Id))
            .Returns(true);
            A.CallTo(() => _menuItemRepository.CheckDuplicateMenuItem(createMenuItem))
            .Returns(nullMenuItem);
            A.CallTo(() => _mapper.Map<MenuItem>(createMenuItem))
            .Returns(menuItemMap);
            A.CallTo(() => _menuItemRepository.CreateMenuItem(menuItemMap))
            .Returns(true);
            var controller = new MenuItemController
                (
                    _menuItemRepository,
                    _restaurantRepository,
                    _mapper
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
            var menuItemMap = A.Fake<MenuItem>();
            A.CallTo(() => _menuItemRepository.MenuItemExists(id))
                .Returns(true);
            menuItemUpdate.Id = 1;
            A.CallTo(() => _mapper.Map<MenuItem>(menuItemUpdate))
                .Returns(menuItemMap);
            A.CallTo(() => _menuItemRepository.UpdateMenuItem(menuItemMap))
                .Returns(true);
            var controller = new MenuItemController
                (
                    _menuItemRepository,
                    _restaurantRepository,
                    _mapper
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
            var menuItemToDelete = A.Fake<MenuItem>();
            var menuItemMap = A.Fake<MenuItem>();
            A.CallTo(() => _menuItemRepository.MenuItemExists(id))
                .Returns(true);
            A.CallTo(() => _menuItemRepository.GetMenuItem(id))
                .Returns(menuItemToDelete);
            A.CallTo(() => _menuItemRepository.DeleteMenuItem(menuItemToDelete))
                .Returns(true);
            var controller = new MenuItemController
                (
                    _menuItemRepository,
                    _restaurantRepository,
                    _mapper
                );

            // act
            var result = await controller.DeleteMenuItem(id);

            //asert
            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(NoContentResult));

        }
    }
}
