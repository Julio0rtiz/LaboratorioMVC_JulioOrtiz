using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace TiendaMVC.Models.VirewModels
{
    public class ProductFormViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre del producto es obligatorio.")]
        public string Name { get; set; } = "";

        [Required(ErrorMessage = "El precio del producto es obligatorio.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "El precio debe ser mayor que cero.")]
        public decimal Price { get; set; }

        [Range(0,100000,ErrorMessage = "El inventario debe estar entre 0 y 100000.")]
        public int Stock { get; set; }

        [Required(ErrorMessage = "Seleccione una categoría")]
        [Display(Name = "Categoría")]
        public int? CategoryId { get; set; }

        [ValidateNever]
        public IEnumerable<Category> Categories { get; set; } = new List<Category>();
    }
}