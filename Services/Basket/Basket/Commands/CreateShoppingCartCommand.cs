﻿using Basket.Dtos;
using Basket.Responses;
using MediatR;

namespace Basket.Commands
{
    public record CreateShoppingCartCommand(string UserName, List<CreateShoppingCartDto> Items): IRequest<ShoppingCartResponse>
    {
    }
}
