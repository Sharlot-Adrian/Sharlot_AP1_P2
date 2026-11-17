using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace _2doParcialSharlot.Models
{
    public class Productos
    {
        [Key]
        public int ProductoId { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public int Stock { get; set; } = 10;
        [Required(ErrorMessage ="Es obligatorio agregar el precio. ")]
        public double Precio { get; set; }

        [InverseProperty("Productos")]
        public virtual ICollection<VentasDetalle> Detalle { get; set; } = [];

    }
}
