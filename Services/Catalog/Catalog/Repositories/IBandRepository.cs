using Catalog.Entities;

namespace Catalog.Repositories
{
    public interface IBandRepository
    {
        Task<IEnumerable<ProductBrand>> GetAllBrands();
        Task<ProductBrand> GetByIdAsync(string id);
    }
}
