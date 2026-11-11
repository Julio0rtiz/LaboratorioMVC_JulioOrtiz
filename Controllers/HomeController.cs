using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TiendaMVC.Data;
using TiendaMVC.Models.ViewModels;

namespace TiendaMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;

        public HomeController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var model = new HomeSummaryViewModel();

            // Cálculos en la base de datos
            model.TotalProducts = _context.Products.Count();
            model.TotalCategories = _context.Categories.Count();
            
            // Usamos decimal? para evitar errores si la tabla está vacía
            model.InventoryValue = _context.Products.Sum(p => (decimal?)(p.Price * p.Inventory)) ?? 0m;

            var lowInventoryQuery = _context.Products
                .Include(p => p.Category)
                .Where(p => p.Inventory < 5);

            model.LowInventoryCount = lowInventoryQuery.Count();
            
            // Ordenado por inventario
            model.LowInventoryProducts = lowInventoryQuery
                .OrderBy(p => p.Inventory)
                .ThenBy(p => p.Name)
                .ToList();

            return View(model);
        }
    }
}