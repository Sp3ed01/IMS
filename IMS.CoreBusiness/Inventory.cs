using System.ComponentModel.DataAnnotations;
using System.Reflection.PortableExecutable;

namespace IMS.CoreBusiness
{
    public class Inventory
    {
        public int InventoryID { get; set; }

        [Required]
        [StringLength(150)]
        public string InventoryName { get; set; } = String.Empty;

        [Range(0, int.MaxValue, ErrorMessage= "Quantity must be grater or equal to 0")]
        public int Quantity { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Price must be grater or equal to 0")]
        public double Price { get; set; }

    }
}