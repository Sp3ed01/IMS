using IMS.CoreBusiness.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.CoreBusiness
{
    public class Product
    {
        public int ProductID { get; set; }

        [Required]
        [StringLength(150)]

        public string ProductName { get; set; } = String.Empty;

        [Range(0, int.MaxValue, ErrorMessage = "Quantity must be grater or equal to 0")]
        public int Quantity { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Price must be grater or equal to 0")]
        public double Price { get; set; }

        [Product_EnsurePriceIsGreaterThanInventoriesCost]
        public List<ProductInventory> ProductInventories { get; set; } = new List<ProductInventory>();

        public void AddInventory(Inventory inventory)
        {
            if (!this.ProductInventories.Any(x => x.Inventory != null &&
                x.Inventory.InventoryName.Equals(inventory.InventoryName)))
            {
                this.ProductInventories.Add(new ProductInventory
                {
                    InventoryID = inventory.InventoryID,
                    Inventory = inventory,
                    InventoryQuantity = 1,
                    ProductID=this.ProductID
                });
            }
        }
    }
}
