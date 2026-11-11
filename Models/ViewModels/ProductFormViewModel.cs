using System.ComponentModel.DataAnnotations;

namespace TiendaMVC.Models.ViewModels
{
    public class ProductFormViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio")]
        public string Name { get; set; } = null!;

        [Range(0.01, 10000, ErrorMessage = "El precio debe ser mayor a 0")]
        public decimal Price { get; set; }

        [Range(0, 10000, ErrorMessage = "El inventario no puede ser negativo")]
        public int Inventory { get; set; }

        [Required(ErrorMessage = "Debe seleccionar una categoría")]
        public int CategoryId { get; set; }

        // Nuevo campo agregado
        [MaxLength(200, ErrorMessage = "La descripción no puede superar los 200 caracteres.")]
        public string? Description { get; set; }
    }
}