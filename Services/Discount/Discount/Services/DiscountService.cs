using Discount.Commands;
using Discount.Grpc.Protos;
using Discount.Mappers;
using Discount.Queries;
using Grpc.Core;
using MediatR;

namespace Discount.Services
{
    public class DiscountService : DiscountProtoService.DiscountProtoServiceBase
    {
        private readonly IMediator _mediator;

        public DiscountService(IMediator mediator)
        {
            _mediator = mediator;
        }

        //public override async Task<CouponModel> GetDiscount(GetDiscountRequest request, ServerCallContext context)
        //{
        //    var query = new GetDiscountQuery(request.ProductName);
        //    var coupon = await _mediator.Send(query);
        //    if (coupon == null)
        //    {
        //        throw new RpcException(new Status(StatusCode.NotFound, $"Discount with ProductName={request.ProductName} is not found."));
        //    }
        //    return coupon.ToModel();
        //}

        public override async Task<CouponModel> GetDiscount(GetDiscountRequest request, ServerCallContext context)
        {
            try
            {
                var query = new GetDiscountQuery(request.ProductName);
                var coupon = await _mediator.Send(query);

                if (coupon == null)
                {
                    throw new RpcException(new Status(StatusCode.NotFound,
                        $"Discount with ProductName={request.ProductName} is not found."));
                }

                return coupon.ToModel();
            }
            catch (RpcException) // already well-formed → rethrow
            {
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in GetDiscount: {ex}");
                throw new RpcException(new Status(StatusCode.Internal,
                    $"Unexpected error while getting discount for {request.ProductName}: {ex.Message}"));
            }
        }


        public override async Task<CouponModel> CreateDiscount(CreateDiscountRequest request, ServerCallContext context)
        {
            var command = request.Coupon.ToCreateCommand();
            var coupon = await _mediator.Send(command);
            return coupon.ToModel();
        }

        public override async Task<CouponModel> UpdateDiscount(UpdateDiscountRequest request, ServerCallContext context)
        {
            var command = request.Coupon.ToUpdateCommand();
            var coupon = await _mediator.Send(command);
            if (coupon == null)
            {
                throw new RpcException(new Status(StatusCode.NotFound, $"Discount with Id={request.Coupon.Id} is not found."));
            }
            return coupon.ToModel();
        }

        public override async Task<DeleteDiscountResponse> DeleteDiscount(DeleteDiscountRequest request, ServerCallContext context)
        {
            var command = new DeleteDiscountCommand(request.ProductName);
            var success = await _mediator.Send(command);
            var response = new DeleteDiscountResponse
            {
                Success = success
            };
            return response;
        }
    }
}
