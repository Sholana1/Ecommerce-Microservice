using Catalog.Extensions;
using Catalog.Queries;
using Catalog.Repositories;
using Catalog.Responses;
using MediatR;

namespace Catalog.Handlers
{
    public class GetProductByNameHandler : IRequestHandler<GetProductByNameQuery, IList<ProductResponse>>
    {
        private readonly IProductRepository _productRepository;

        public GetProductByNameHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        public async Task<IList<ProductResponse>> Handle(GetProductByNameQuery request, CancellationToken cancellationToken)
        {
            var productList = await _productRepository.GetProductByName(request.Name);
            var productResposeList = productList.ToResponseList().ToList();
            return productResposeList;
        }
    }
}
