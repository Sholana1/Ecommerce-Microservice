using Catalog.Commands;
using Catalog.Extensions;
using Catalog.Repositories;
using MediatR;

namespace Catalog.Handlers
{
    public class UpdateProductHandler : IRequestHandler<UpdateProductCommand, bool>
    {
        private readonly IProductRepository _productRepository;

        public UpdateProductHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        public async Task<bool> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var existing = await _productRepository.GetProductById(request.Id);
            if (existing == null)
            {
                throw new KeyNotFoundException($"Product with Id {request.Id} not found");
            }
            var brand = await _productRepository.GetBrandByIdAsync(request.Id);
            var type = await _productRepository.GetTypeByIdAsync(request.Id);
            if(brand == null || type == null)
            {
                throw new ApplicationException("Invalid Brand or Type specified");
            }

            var updatedProduct = request.ToUpdateEntity(existing, brand, type);

            return await _productRepository.UpdateProduct(updatedProduct);
        }
    }
}
