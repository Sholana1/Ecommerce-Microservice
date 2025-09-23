using Catalog.Responses;
using Catalog.Specifications;
using MediatR;

namespace Catalog.Queries
{
    public record GetAllProductQuery(CatalogSpecParams CatalogSpecParams): IRequest<Pagination<ProductResponse>>;
}
