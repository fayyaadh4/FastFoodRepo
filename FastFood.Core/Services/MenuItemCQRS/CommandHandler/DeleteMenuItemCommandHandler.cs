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
    public class DeleteMenuItemCommandHandler: IRequestHandler<DeleteMenuItemCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DeleteMenuItemCommandHandler(IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<bool> Handle(DeleteMenuItemCommand request, CancellationToken cancellationToken)
        {
            if (!await _unitOfWork.MenuItem.Exists(request.MenuItemId))
                throw new Exception("Menu item already exists");


            var menuItemToDelete = await _unitOfWork.MenuItem.GetById(request.MenuItemId);

            await _unitOfWork.MenuItem.Remove(menuItemToDelete);
            return await _unitOfWork.CompleteAsync();

        }
    }
}
