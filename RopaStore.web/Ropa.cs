using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TiendaRopa.Modelos
{
    public class Ropa
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Tipo { get; set; }
        public string? Tela {  get; set; }
        public float Precio {  get; set; }
        public byte Stock {  get; set; }
        public string? UrlRopa { get; set; }

    }
}
