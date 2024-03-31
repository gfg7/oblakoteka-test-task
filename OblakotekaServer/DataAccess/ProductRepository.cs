using Microsoft.EntityFrameworkCore;
using OblakotekaServer.DataAccess.Models;
using OblakotekaServer.Domain;
using OblakotekaServer.Domain.Models;

namespace OblakotekaServer.DataAccess
{
    public class ProductRepository : IProductRepository
    {
        private readonly TestDbContext _context;
        public ProductRepository(TestDbContext context)
        {
            _context = context;
        }

        public async Task<ProductDomain> Create(ProductCreateParams @params)
        {
            var obj = @params.BuildDbModel();

            obj = _context.Products.Add(obj).Entity;
            await _context.SaveChangesAsync();

            return obj.ToDomain();
        }

        public async Task<ProductDomain?> DeleteById(Guid id)
        {
            var obj = await FindById(id);
            if (obj is null)
            {
                return null;
            }

            _context.Products.Remove(obj);
            await _context.SaveChangesAsync();
            return obj.ToDomain();
        }

        public async Task<ProductDomain?> Edit(Guid id, ProductEditParams @params)
        {
            var obj = await FindById(id);
            if (obj is null)
            {
                return null;
            }

            obj = obj.ApplyChanges(@params);
            _context.Products.Update(obj);
            await _context.SaveChangesAsync();

            return obj.ToDomain();
        }

        public async Task<ProductDomain[]> FilterByName(string search)
        {
            search = search.ToLower().Trim();
            var result = _context.Products.Where(x=> x.Name.ToLower().Contains(search));
            return await result.Select(x => x.ToDomain()).ToArrayAsync();
        }

        private async Task<Product?> FindById(Guid id)
        {
            return await _context.Products.FindAsync(id);
        }
    }
}