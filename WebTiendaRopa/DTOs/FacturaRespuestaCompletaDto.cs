using System.ComponentModel.DataAnnotations.Schema;
using TiendaRopa.Modelos;

namespace WebTiendaRopa.DTOs
{
    public class FacturaRespuestaCompletaDto
    {
        public float Precio { get; set; }
        public DateTime Fecha { get; set; } = DateTime.Now;
        public int IdUsuario { get; set; }  
        public List<FacturaCompras> Compras { get; set; } = new List<FacturaCompras>();

    }
}
