using TiendaRopa.Modelos;

namespace WebTiendaRopa.DTOs
{
    public class FacturaRespuestaDto
    {
        public float Precio { get; set; }
        public DateTime Fecha { get; set; } = DateTime.Now;
        public string? Email { get; set; }
        public string? Nombre { get; set; }
        public List<RopaRespuestaListadoDto>? Compras { get; set; }

    }
}
