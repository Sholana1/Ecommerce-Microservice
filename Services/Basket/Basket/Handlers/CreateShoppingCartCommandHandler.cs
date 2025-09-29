using Basket.Commands;
using Basket.GrpcService;
using Basket.Mappers;
using Basket.Repositories;
using Basket.Responses;
using MediatR;

namespace Basket.Handlers
{
    public class CreateShoppingCartCommandHandler : IRequestHandler<CreateShoppingCartCommand, ShoppingCartResponse>
    {
        private readonly IBasketRepository _basketRepository;
        private readonly DiscountGrpcService _discountGrpcService;

        public CreateShoppingCartCommandHandler(IBasketRepository basketRepository, DiscountGrpcService discountGrpcService)
        {
            _basketRepository = basketRepository;
            _discountGrpcService = discountGrpcService;
        }
        public async Task<ShoppingCartResponse> Handle(CreateShoppingCartCommand request, CancellationToken cancellationToken)
        {
            //apply discount using grpc call
            foreach(var item in request.Items)
            {
                var coupon = await _discountGrpcService.GetDiscount(item.ProductName);
                item.Price -= coupon.Amount;
            }
            var shoppingCartEntity = request.ToEntity();
            var updatedCart = await _basketRepository.UpdateBasket(shoppingCartEntity);
            return updatedCart.ToResponse();
        }
    }
}
