using Catalog.Queries;
using Catalog.Responses;
using MediatR;

namespace Catalog.Handlers
{
    public class GetAllBrandsHandler : IRequestHandler<GetAllBrandsQuery, IList<BrandResponse>>
    {
        public Task<IList<BrandResponse>> Handle(GetAllBrandsQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
