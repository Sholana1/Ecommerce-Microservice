﻿using Discount.Dtos;
using MediatR;

namespace Discount.Commands
{
    public record UpdateDiscountCommand(int Id, string ProductName, string Description, int Amount ): IRequest<CouponDto>;
}
