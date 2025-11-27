namespace WebTiendaRopa.DTOs
{
    public class RopaIngresoFiltroDto
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Tipo { get; set; }
        public byte Stock { get; set; }
        public string? Tela { get;set; }
        public float PrecioMax {  get; set; }
        public float PrecioMin { get; set; }
        public string? UrlRopa { get; set; }
    }
}
