using System.ComponentModel.DataAnnotations;

namespace IMS.WebApp.ViewModels
{
    public class PurchaseViewModel
    {
        [Required]
        public string PONumber { get; set; } = String.Empty;

        [Required]
        [Range(minimum: 1, maximum:int.MaxValue, ErrorMessage = "You have to select an inventory.")]
        public int InventoryID { get; set; }

        [Required]
        [Range(minimum: 1, maximum: int.MaxValue, ErrorMessage = "Quantity has to be grater than 1.")]
        public int QuantityToPurchase { get; set; }


        public double InventoryPrice { get; set; }
    }
}
