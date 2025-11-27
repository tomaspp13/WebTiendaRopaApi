namespace WebTiendaRopa.DTOs
{
    public class CompraDto
    {  
        public float Precio { get; set; }
        public DateTime Fecha { get; set; } = DateTime.Now;
        public int IdUsuario { get; set; }
        public List<int>? IdCompras { get; set; }
    }
}
