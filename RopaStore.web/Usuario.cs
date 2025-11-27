using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.InteropServices.JavaScript;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TiendaRopa.Modelos
{
    public class Usuario
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdUsuario {  get; set; }
        public string? Email { get; set; }
        public string? Contraseña {  get; set; }
        public string? Nombre { get; set; }
        [JsonIgnore]
        public List<Factura>?Facturas { get; set; }

    }
}
