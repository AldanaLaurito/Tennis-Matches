using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Baufest.Tennis.Domain.Entities
{
    public class Cancha
    {
        [Key]
        public int Id { get; set; }

        [StringLength(50, ErrorMessage = "El campo {0} debe tener menos de {1} caracteres")]
        public string Nombre { get; set; }

        [StringLength(200, ErrorMessage = "El campo {0} debe tener menos de {1} caracteres")]
        public string Direccion { get; set; }

        public virtual ICollection<Partido> Partidoes { get; set; }
    }
}
