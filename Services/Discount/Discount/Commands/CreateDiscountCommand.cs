using Discount.Dtos;
using MediatR;

namespace Discount.Commands
{
    public record CreateDiscountCommand(string ProductName, string Description, int Amount) : IRequest<CouponDto>;
}
