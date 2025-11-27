using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TiendaRopa.Modelos
{
    public class FacturaCompras
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdFacturaCompras {  get; set; }
        public int IdFactura { get; set; }
        public int IdRopa {  get; set; }
        [ForeignKey("IdRopa")]
        public Ropa? Ropa { get; set; }

    }
}
