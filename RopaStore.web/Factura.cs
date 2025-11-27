using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TiendaRopa.Modelos
{
    public class Factura
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdFactura {  get; set; }
        public float Precio {  get; set; }
        public DateTime Fecha {  get; set; } = DateTime.Now;
        public int IdUsuario {  get; set; }
        [ForeignKey("IdUsuario")]
        public Usuario? Usuario { get; set; }
        public List<FacturaCompras> Compras { get; set; } = new List<FacturaCompras>();

    }
}
