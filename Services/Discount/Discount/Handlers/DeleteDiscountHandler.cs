using Discount.Commands;
using Discount.Extensions;
using Discount.Repositories;
using MediatR;

namespace Discount.Handlers
{
    public class DeleteDiscountHandler : IRequestHandler<DeleteDiscountCommand, bool>
    {
        private readonly IDiscountRepository _discountRepository;

        public DeleteDiscountHandler(IDiscountRepository discountRepository)
        {
            _discountRepository = discountRepository;
        }
        public Task<bool> Handle(DeleteDiscountCommand request, CancellationToken cancellationToken)
        {
            if(string.IsNullOrWhiteSpace(request.ProductName))
            {
                var validationError = new Dictionary<string, string>
                {
                    { "ProductName", "Product Name must not be empty" }
                };
                throw GrpcErrorHelper.CreateValidationException(validationError);
            }

            var deleted = _discountRepository.DeleteDiscount(request.ProductName);
            return deleted;
        }
    }
}
