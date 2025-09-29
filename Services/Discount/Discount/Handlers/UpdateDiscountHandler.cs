using Discount.Commands;
using Discount.Dtos;
using Discount.Extensions;
using Discount.Mappers;
using Discount.Repositories;
using Grpc.Core;
using MediatR;

namespace Discount.Handlers
{
    public record UpdateDiscountHandler : IRequestHandler<UpdateDiscountCommand, CouponDto>
    {
        private readonly IDiscountRepository _discountRepository;

        public UpdateDiscountHandler(IDiscountRepository discountRepository)
        {
            _discountRepository = discountRepository;
        }
        public async Task<CouponDto> Handle(UpdateDiscountCommand request, CancellationToken cancellationToken)
        {
            //Input Validation
            var validationError = new Dictionary<string, string>();
            if (string.IsNullOrWhiteSpace(request.ProductName))
            {
                validationError["ProductName"] = "Product Name must not be empty";
            }
            if (string.IsNullOrWhiteSpace(request.Description))
            {
                validationError["Description"] = "Product Description must not be empty";
            }
            if (request.Amount <= 0)
            {
                validationError["Amount"] = "Product Amount must be greater than zer0";
            }
            if (validationError.Any())
            {
                throw GrpcErrorHelper.CreateValidationException(validationError);
            }

            var coupon = request.ToEntity();
            var updated = await _discountRepository.UpdateDiscount(coupon);
            if (!updated)
            {
                throw new RpcException(new Status(StatusCode.NotFound, $"Discount update failed for product = {request.ProductName}"));
            }

            return coupon.ToDto();
        }
    }
}
