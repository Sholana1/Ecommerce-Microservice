using Discount.Grpc.Protos;

namespace Discount.Dtos
{
    public record CouponDto(
        int Id, string ProductName, string Description, int Amount
    );
}
