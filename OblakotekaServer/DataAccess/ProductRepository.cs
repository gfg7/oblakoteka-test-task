using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using OblakotekaServer.DataAccess.Exceptions;
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
            await Save();

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
            await Save();
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
            await Save();

            return obj.ToDomain();
        }

        public async Task<ProductDomain[]> FilterByName(string? search, CancellationToken token)
        {
            var result = _context.Products.AsNoTracking();

            if (!string.IsNullOrWhiteSpace(search))
            {
                search = search.ToLower().Trim();
                result = result.Where(x => x.Name.ToLower().Contains(search));
            }

            return await result.Select(x => x.ToDomain()).ToArrayAsync(token);
        }

        private async Task<Product?> FindById(Guid id)
        {
            return await _context.Products.FindAsync(id);
        }

        private async Task Save()
        {
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                if (ex.InnerException is SqlException e && (e.Number == 2601 || e.Number == 2627))
                {
                    var errorValue = ex.Entries.First()
                        .Properties.First(x =>
                            x.IsModified &&
                            !x.IsTemporary || 
                            x.EntityEntry.State == EntityState.Added &&
                            x.Metadata.IsUniqueIndex());

                    throw new NotUniqueValueException(errorValue!.CurrentValue!.ToString()!);
                }
            }
        }
    }
}