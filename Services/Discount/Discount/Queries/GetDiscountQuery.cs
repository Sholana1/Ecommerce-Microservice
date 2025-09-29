using Discount.Dtos;
using MediatR;

namespace Discount.Queries
{
    public record GetDiscountQuery(string productName) : IRequest<CouponDto>;
}
