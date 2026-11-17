using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace _2doParcialSharlot.Models
{
    public class Ventas
    {
        [Key]
        public int VentaId { get; set; }
        public DateTime Fecha { get; set; }
        public string Observacion { get; set; } = string.Empty;
        public double Total { get; set; }

        [InverseProperty("Ventas")]
        public virtual ICollection<VentasDetalle> Detalle { get; set; } = [];
    }
}
