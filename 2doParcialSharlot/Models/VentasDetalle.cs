using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace _2doParcialSharlot.Models
{
    public class VentasDetalle
    {
        [Key]
        public int DetalleId { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Debe Elegir una Id de venta valido. ")]
        public int VentaId { get; set; }
        [Range(1,int.MaxValue,ErrorMessage ="Debe Elegir un producto valido. ")]
        public int ProductoId { get; set; }
        public int Cantidad { get; set; }
        public double Precio { get; set; }

        [ForeignKey("VentaId")]
        [InverseProperty("Detalle")]
        public virtual Ventas Ventas { get; set; } = null!;

        [ForeignKey("ProductoId")]
        [InverseProperty("Detalle")]
        public virtual Productos Productos { get; set; } = null!;
    }
}
