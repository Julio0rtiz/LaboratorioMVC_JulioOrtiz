using TiendaMVC.Models;

namespace TiendaMVC.Models.ViewModels
{
    public class HomeSummaryViewModel
    {
        public int TotalProducts { get; set; }
        public int TotalCategories { get; set; }
        public decimal InventoryValue { get; set; }
        public int LowInventoryCount { get; set; }
        public List<Product> LowInventoryProducts { get; set; } = new List<Product>();
    }
}