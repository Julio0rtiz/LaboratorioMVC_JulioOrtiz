using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TiendaMvc.Data;
using TiendaMVC.Models;
using TiendaMVC.Models.ViewModels;

namespace TiendaMVC.Controllers
{
    public class ProductController : Controller
    {
        private readonly AppDbContext _context;

        public ProductController(AppDbContext context)
        {
            _context = context;
        }

        // PARTE 1: Buscador y Filtro
        public IActionResult Index(string? searchName, int? categoryId)
        {
            // Consulta base diferida (Filtrado en base de datos)
            var query = _context.Products.Include(p => p.Category).AsQueryable();

            if (!string.IsNullOrEmpty(searchName))
            {
                query = query.Where(p => p.Name.Contains(searchName));
            }

            if (categoryId.HasValue)
            {
                query = query.Where(p => p.CategoryId == categoryId.Value);
            }

            // Conservar valores en la vista
            ViewBag.SearchName = searchName;
            ViewBag.CategoryId = categoryId;
            ViewBag.Categories = new SelectList(_context.Categories, "Id", "Name", categoryId);

            var products = query.ToList();
            return View(products);
        }

        public IActionResult Create()
        {
            ViewBag.Categories = new SelectList(_context.Categories, "Id", "Name");
            return View(new ProductFormViewModel());
        }

        // PARTE 2: Regla de unicidad y Descripción en Create
        [HttpPost]
        public IActionResult Create(ProductFormViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Validación de unicidad
                bool exists = _context.Products.Any(p => p.CategoryId == model.CategoryId && p.Name.ToLower() == model.Name.ToLower());
                if (exists)
                {
                    ModelState.AddModelError("Name", "Ya existe un producto con ese nombre en la categoría seleccionada");
                }

                if (ModelState.IsValid)
                {
                    var product = new Product
                    {
                        Name = model.Name,
                        Price = model.Price,
                        Inventory = model.Inventory,
                        CategoryId = model.CategoryId,
                        Description = model.Description
                    };

                    _context.Products.Add(product);
                    _context.SaveChanges();
                    return RedirectToAction(nameof(Index));
                }
            }

            ViewBag.Categories = new SelectList(_context.Categories, "Id", "Name", model.CategoryId);
            return View(model);
        }

        public IActionResult Edit(int id)
        {
            var product = _context.Products.Find(id);
            if (product == null) return NotFound();

            var model = new ProductFormViewModel
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                Inventory = product.Inventory,
                CategoryId = product.CategoryId,
                Description = product.Description
            };

            ViewBag.Categories = new SelectList(_context.Categories, "Id", "Name", product.CategoryId);
            return View(model);
        }

        // PARTE 2: Regla de unicidad y Descripción en Edit
        [HttpPost]
        public IActionResult Edit(ProductFormViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Validación de unicidad ignorando el ID actual (caso de conservar el propio nombre)
                bool exists = _context.Products.Any(p => p.CategoryId == model.CategoryId && p.Name.ToLower() == model.Name.ToLower() && p.Id != model.Id);
                if (exists)
                {
                    ModelState.AddModelError("Name", "Ya existe un producto con ese nombre en la categoría seleccionada");
                }

                if (ModelState.IsValid)
                {
                    var product = _context.Products.Find(model.Id);
                    if (product == null) return NotFound();

                    product.Name = model.Name;
                    product.Price = model.Price;
                    product.Inventory = model.Inventory;
                    product.CategoryId = model.CategoryId;
                    product.Description = model.Description;

                    _context.SaveChanges();
                    return RedirectToAction(nameof(Index));
                }
            }

            ViewBag.Categories = new SelectList(_context.Categories, "Id", "Name", model.CategoryId);
            return View(model);
        }
        
        // GET: Product/Delete
public IActionResult Delete(int id)
{
    var product = _context.Products.Include(p => p.Category).FirstOrDefault(p => p.Id == id);
    if (product == null) return NotFound();
    
    return View(product);
}

// POST: Product/Delete
[HttpPost, ActionName("Delete")]
public IActionResult DeleteConfirmed(int id)
{
    var product = _context.Products.Find(id);
    if (product != null)
    {
        _context.Products.Remove(product);
        _context.SaveChanges();
    }
    return RedirectToAction(nameof(Index));
}
    }
}