using AutoMapper;
using FastFood.Core.Services.MenuItemCQRS.Commands;
using FastFood.Domain.Entities;
using FastFood.Domain.Interfaces;
using FastFood.Domain.RepoInterfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastFood.Core.Services.MenuItemCQRS.CommandHandler
{
    public class UpdateMenuItemCommandHandler : IRequestHandler<UpdateMenuItemCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateMenuItemCommandHandler(IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<bool> Handle(UpdateMenuItemCommand request, CancellationToken cancellationToken)
        {
            if (request.UpdateMenuItem == null)
                throw new Exception("Body cannot be null");

            if (!await _unitOfWork.MenuItem.Exists(request.MenuItemId))
                throw new Exception("Menu item already exists");

            if (request.MenuItemId != request.UpdateMenuItem.Id)
                throw new Exception("ID mismatch");

            var menuItemMap = _mapper.Map<MenuItem>(request.UpdateMenuItem);

            await _unitOfWork.MenuItem.Update(menuItemMap);
            return await _unitOfWork.CompleteAsync();

        }
    }
}
