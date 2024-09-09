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
    public class MenuItemServiceTests
    {
        private readonly IMenuItemRepository _menuItemRepository;
        private readonly IMenuItemService _menuItemService;
        private readonly IRestaurantRepository _restaurantRepository;
        private readonly IMapper _mapper;

        // real repositories not brought in as we're faking them
        public MenuItemServiceTests()
        {
            _menuItemRepository = A.Fake<IMenuItemRepository>();
            _menuItemService = A.Fake<IMenuItemService>();
            _restaurantRepository = A.Fake<IRestaurantRepository>();
            _mapper = A.Fake<IMapper>();
        }

        [Fact]
        public async Task MenuItemService_GetMenuItems_ReturnsMenuItems()
        {
            // Arrange: Get variables, functions, etc.
            var menuItemsRepo = A.Fake<ICollection<MenuItemDto>>();
            var menuItemsListReturned = A.Fake<List<MenuItemDto>>();
            A.CallTo(() => _mapper.Map<List<MenuItemDto>>(menuItemsRepo))
                .Returns(menuItemsListReturned);
            var service = new MenuItemService
                (
                    _menuItemRepository,
                    _restaurantRepository,
                    _mapper
                );

            // Act: perform action
            var result = await service.GetMenuItems();

            // Assert: What outcome is expected
            result.Should().NotBeNull();
        }

        [Fact]
        public async Task MenuItemService_GetMenuItem_ReturnsMenuItem()
        {
            // Arrange
            var menuItemId = 1;
            var menuItemRepo = A.Fake<MenuItemDto>();
            var menuItemReturned = A.Fake<MenuItemDto>();
            A.CallTo(() => _menuItemRepository.MenuItemExists(menuItemId))
                .Returns(true);
            A.CallTo(() => _mapper.Map<MenuItemDto>(menuItemRepo))
                .Returns(menuItemReturned);
            var service = new MenuItemService
                (
                    _menuItemRepository,
                    _restaurantRepository,
                    _mapper
                );

            // Act
            var result = await service.GetMenuItem(menuItemId);

            // Assert
            result.Should().NotBeNull();


        }

        [Fact]
        public async Task MenuService_CreateMenuItem_ReturnsTrue()
        {
            // arrange
            var createMenuItem = A.Fake<MenuItemDto>();
            // var menuItemExists = null;
            var menuItemMap = A.Fake<MenuItem>();
            MenuItemDto? nullMenuItem = null;
            A.CallTo(() => _restaurantRepository.RestaurantExists(createMenuItem.Id))
            .Returns(true);
            A.CallTo(() => _menuItemService.CheckDuplicateMenuItem(createMenuItem))
            .Returns(nullMenuItem);
            A.CallTo(() => _mapper.Map<MenuItem>(createMenuItem))
            .Returns(menuItemMap);
            A.CallTo(() => _menuItemRepository.CreateMenuItem(menuItemMap))
            .Returns(true);
            var controller = new MenuItemService
                (
                    _menuItemRepository,
                    _restaurantRepository,
                    _mapper
                );

            // act
            var result = await controller.CreateMenuItem(createMenuItem);

            //assert
            result.Should().BeTrue();

        }

        [Fact]
        public async Task MenuItemService_UpdateMenuItem_ReturnsTrue()
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
            var controller = new MenuItemService
                (
                    _menuItemRepository,
                    _restaurantRepository,
                    _mapper
                );

            // act
            var result = await controller.UpdateMenuItem(id, menuItemUpdate);

            //asert
            result.Should().BeTrue();

        }


        [Fact]
        public async Task MenuItemService_DeleteMenuItem_ReturnsTrue()
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
            var controller = new MenuItemService
                (
                    _menuItemRepository,
                    _restaurantRepository,
                    _mapper
                );

            // act
            var result = await controller.DeleteMenuItem(id);

            //asert
            result.Should().BeTrue();

        }
    }
}
