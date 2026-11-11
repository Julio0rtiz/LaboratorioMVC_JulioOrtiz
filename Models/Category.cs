using System.ComponentModel.DataAnnotations;

namespace TiendaMVC.Models
{
public class Category
{
   public int Id { get; set; }

    [Required(ErrorMessage = "El nombre de la categoría es obligatorio.")]
    public string Name { get; set; } ="";
}
}
