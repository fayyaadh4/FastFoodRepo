
using AutoMapper;
using FastFood.Domain.Entities;
using FastFood.Domain.Interfaces;
using FastFood.Domain.RepoInterfaces;
using FastFood.Domain.ServiceInterfaces;
using FastFood.Dto;
using Serilog;

namespace FastFood.Application.Services
{
    public class MenuItemService : IMenuItemService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public MenuItemService(IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }


        public async Task<MenuItemDto?> CheckDuplicateMenuItem(MenuItemDto menuItem)
        {
            var menuItemExists = _mapper.Map<List<MenuItemDto>>(await _unitOfWork.MenuItem.GetAll())
                .Where(m => m.Name.Trim().ToUpper() == menuItem.Name.Trim().ToUpper())
                .FirstOrDefault();
            return menuItemExists;
        }

    }
}
