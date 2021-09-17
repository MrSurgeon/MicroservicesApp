using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Catalog.API.Entities;
using MongoDB.Driver;

namespace Catalog.API.Data
{
    public class CatalogContextSeed
    {
        public static void SeedData(IMongoCollection<Product> productCollection)
        {
            bool existProduct = productCollection.Find(p => true).Any();
            if (!existProduct)
            {
                productCollection.InsertMany(GetPerConfiguredProducts());
            }
        }

        private static IEnumerable<Product> GetPerConfiguredProducts()
        {
            return new List<Product>()
            {
                new Product()
                {
                    Name = "IPhone X",
                    Summary = "Iphone",
                    Description = "lorem asdasdasd",
                    ImageFile = "product-1.png",
                    Price = 950.00M,
                    Category = "smart-phone"
                },
                new Product()
                {
                    Name = "Samsung S10",
                    Summary = "Samsung Phone",
                    Description = "lorem asdasdasd",
                    ImageFile = "product-2.png",
                    Price = 500.00M,
                    Category = "smart-phone"
                },new Product()
                {
                    Name = "LG G5",
                    Summary = "LG",
                    Description = "lorem asdasdasd",
                    ImageFile = "product-3.png",
                    Price = 460.00M,
                    Category = "smart-phone"
                },new Product()
                {
                    Name = "Sony Xperia Z5",
                    Summary = "Sony",
                    Description = "lorem lorem yaylalar",
                    ImageFile = "product-4.png",
                    Price = 950.00M,
                    Category = "smart-phone"
                },
            };
        }
    }
}
