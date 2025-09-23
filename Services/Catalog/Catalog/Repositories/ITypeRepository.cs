using Catalog.Entities;

namespace Catalog.Repositories
{
    public interface ITypeRepository
    {
        Task<IEnumerable<ProductType>> GetAllType();
        Task<ProductType> GetByIdAsync(string id);
    }
}
