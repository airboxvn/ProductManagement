using ProductManagement.Categories;
using ProductManagement.Products;
using System;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;

namespace ProductManagement.Data
{
    public class ProductManagementDataSeedContributor : IDataSeedContributor, ITransientDependency
    {
        private readonly IRepository<Category, Guid> _categotyRepository;
        private readonly IRepository<Product, Guid> _productRepository;

        public ProductManagementDataSeedContributor(
            IRepository<Category, Guid> categotyRepository,
            IRepository<Product, Guid> productRepository)
        {
            _categotyRepository = categotyRepository;
            _productRepository = productRepository;
        }

        public async Task SeedAsync(DataSeedContext context)
        {
            if (await _categotyRepository.CountAsync() > 0)
            {
                return;
            }

            var monitors = new Category { Name = "Monitors" };
            var printers = new Category { Name = "Printers" };

            await _categotyRepository.InsertManyAsync(new[] { monitors, printers });

            var monitor1 = new Product
            {
                Category = monitors,
                Name = "XP VH240a 23.8-Inch Full HD 1080p IPS LED Monitor",
                Price = 163,
                ReleaseDate = new DateTime(2019, 05, 24),
                StockState = ProductStockState.InStock
            };

            var monitor2 = new Product
            {
                Category = monitors,
                Name = "Clips 328E1CA 32-Inch Curved Monitor, 4K UHD",
                Price = 349,
                IsFreeCargo = true,
                ReleaseDate = new DateTime(2022, 02, 01),
                StockState = ProductStockState.PreOrder
            };

            var printer1 = new Product
            {
                Category = printers,
                Name = "Acme Monochrome Laser Printer, Compact All-In One",
                Price = 199,
                ReleaseDate = new DateTime(2020, 11, 16),
                StockState = ProductStockState.NotAvailable
            };

            await _productRepository.InsertManyAsync(new[] { monitor1, monitor2, printer1 });
        }
    }
}
