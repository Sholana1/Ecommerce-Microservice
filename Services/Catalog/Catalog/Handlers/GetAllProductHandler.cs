using Catalog.Extensions;
using Catalog.Queries;
using Catalog.Repositories;
using Catalog.Responses;
using Catalog.Specifications;
using MediatR;

namespace Catalog.Handlers
{
    public class GetAllProductHandler : IRequestHandler<GetAllProductQuery, Pagination<ProductResponse>>
    {
        private readonly IProductRepository _productRepository;

        public GetAllProductHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        public async Task<Pagination<ProductResponse>> Handle(GetAllProductQuery request, CancellationToken cancellationToken)
        {
            var productList = await _productRepository.GetProducts(request.CatalogSpecParams);
            var productResponseList = productList.ToResponse();
            return productResponseList;
        }
    }
}
