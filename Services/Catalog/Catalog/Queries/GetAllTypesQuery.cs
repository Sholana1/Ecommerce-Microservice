using Catalog.Responses;
using MediatR;

namespace Catalog.Queries
{
    public record class GetAllTypesQuery : IRequest<IList<TypesResponse>>;
}
