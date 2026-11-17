using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace _2doParcialSharlot.Models
{
    public class Ventas
    {
        [Key]
        public int VentaId { get; set; }
        [Required(ErrorMessage = "Es obligatorio agregar la fecha. ")]
        public DateTime Fecha { get; set; }
        public string Observacion { get; set; } = string.Empty;
        [Required(ErrorMessage = "Es obligatorio agregar el Concepto. ")]
        public string Concepto { get; set; } = string.Empty;
        public double Total { get; set; }

        [InverseProperty("Ventas")]
        public virtual ICollection<VentasDetalle> Detalle { get; set; } = [];
    }
}
