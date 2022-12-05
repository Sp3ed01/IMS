using IMS.CoreBusiness;
using IMS.UseCases.PluginInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.Plugins.InMemory
{
    public class ProductRepository : IProductRepository
    {
        private List<Product> _products;

        public ProductRepository()
        {
            _products = new List<Product>
            {
                new Product(){ProductID =1, ProductName= "Bike", Quantity=1, Price=200},
                new Product(){ProductID =2, ProductName= "PC", Quantity=15, Price=300},
                new Product(){ProductID =3, ProductName= "Car", Quantity=4, Price=1000}
            };
        }



        public Task AddProductAsync(Product product)
        {
            if (_products.Any(x => x.ProductName.Equals(product.ProductName, StringComparison.OrdinalIgnoreCase)))
                return Task.CompletedTask;

            var maxId = _products.Max(x => x.ProductID);
            product.ProductID = maxId + 1;

            _products.Add(product);

            return Task.CompletedTask;
        }

        public async Task<Product?> GetProductByIdAsync(int productId)
        {
            var prod = _products.FirstOrDefault(x => x.ProductID == productId);
            var newProd = new Product();
            if(prod != null)
            {
                newProd.ProductID = prod.ProductID;
                newProd.ProductName = prod.ProductName;
                newProd.Price = prod.Price;
                newProd.Quantity = prod.Quantity;
                newProd.ProductInventories = new List<ProductInventory>();
                if(prod.ProductInventories != null && prod.ProductInventories.Count >0)
                {
                    foreach(var prodInv in prod.ProductInventories)
                    {
                        var newProdInv = new ProductInventory
                        {
                            InventoryID = prodInv.InventoryID,
                            ProductID = prodInv.ProductID,
                            Product = prod,
                            Inventory = new Inventory(),
                            InventoryQuantity = prodInv.InventoryQuantity
                        };
                        if(prodInv.Inventory != null)
                        {
                            newProdInv.Inventory.InventoryID = prodInv.Inventory.InventoryID;
                            newProdInv.Inventory.InventoryName = prodInv.Inventory.InventoryName;
                            newProdInv.Inventory.Price = prodInv.Inventory.Price;
                            newProdInv.Inventory.Quantity = prodInv.Inventory.Quantity;
                        }
                        newProd.ProductInventories.Add(newProdInv);
                    }
                }

            }
            return await Task.FromResult(newProd);
        }

        public async Task<IEnumerable<Product>> GetProductsByNameAsync(string name)
        {
            if (string.IsNullOrWhiteSpace(name)) return await Task.FromResult(_products);
            return _products.Where(x => x.ProductName.Contains(name, StringComparison.OrdinalIgnoreCase));
        }

        public Task UpdateProductAsync(Product product)
        {
            //Prevent different product from having the same name
            if (_products.Any(x => x.ProductID != product.ProductID &&
                    x.ProductName.ToLower() == product.ProductName.ToLower())) return Task.CompletedTask;

            var prod = _products.FirstOrDefault(x => x.ProductID == product.ProductID);
            if(prod != null)
            {
                prod.ProductName = product.ProductName;
                prod.Price = product.Price;
                prod.Quantity = product.Quantity;
                prod.ProductInventories = product.ProductInventories;
            }

            return Task.CompletedTask;

        }
    }
}
