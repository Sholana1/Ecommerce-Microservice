using Discount.Grpc.Protos;

namespace Basket.GrpcService
{
    public class DiscountGrpcService
    {
        private readonly DiscountProtpService.DiscountProtpServiceClient _discountProtpServiceClient;

        public DiscountGrpcService(DiscountProtpService.DiscountProtpServiceClient discountProtpServiceClient)
        {
            _discountProtpServiceClient = discountProtpServiceClient;
        }

        public async Task<CouponModel> GetDiscount(string productName)
        {
            var discountRequst = new GetDiscountRequest { ProductName = productName };
            return await _discountProtpServiceClient.GetDiscountAsync(discountRequst);
        }
    }
}
