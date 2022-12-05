using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.CoreBusiness
{
    public class ProductInventory
    {
        public int ProductID { get; set; }
        public Product? Product { get; set; }//may be null

        public int InventoryID { get; set; }
        public Inventory? Inventory { get; set; }
        public int InventoryQuantity { get; set; }

    }
}
