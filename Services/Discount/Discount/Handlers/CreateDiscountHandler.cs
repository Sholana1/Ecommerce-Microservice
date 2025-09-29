using Discount.Commands;
using Discount.Dtos;
using Discount.Extensions;
using Discount.Mappers;
using Discount.Repositories;
using Grpc.Core;
using MediatR;

namespace Discount.Handlers
{
    public class CreateDiscountHandler : IRequestHandler<CreateDiscountCommand, CouponDto>
    {
        private readonly IDiscountRepository _discountRepository;

        public CreateDiscountHandler(IDiscountRepository discountRepository)
        {
            _discountRepository = discountRepository;
        }
        public async Task<CouponDto> Handle(CreateDiscountCommand request, CancellationToken cancellationToken)
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
            if(validationError.Any())
            {
                throw GrpcErrorHelper.CreateValidationException(validationError);
            }

            //map to entity
            var coupon = request.ToEntity();

            //save to db
            var created = await _discountRepository.CreateDiscount(coupon);
            if (!created)
            {
                throw new RpcException(new Status(StatusCode.Internal, $"Could not create discount for product: {request.ProductName}"));
            }
            return coupon.ToDto();
        }
    }
}
