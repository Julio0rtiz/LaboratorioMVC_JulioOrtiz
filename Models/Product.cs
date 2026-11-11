using System.ComponentModel.DataAnnotations;

namespace TiendaMVC.Models
{
    public class Product
    {
        public int Id { get; set; }
        
        [Required(ErrorMessage = "El nombre es obligatorio")]
        public string Name { get; set; } = null!;
        
        public decimal Price { get; set; }
        
        public int Inventory { get; set; }
        
        public int CategoryId { get; set; }
        public Category? Category { get; set; }

        // Nuevo campo agregado
        [MaxLength(200, ErrorMessage = "La descripción no puede superar los 200 caracteres.")]
        public string? Description { get; set; }
    }
}