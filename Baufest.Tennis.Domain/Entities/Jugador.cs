using System.ComponentModel.DataAnnotations;

namespace Baufest.Tennis.Domain.Entities
{
    public class Jugador
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [StringLength(50, ErrorMessage = "El campo {0} debe tener menos de {1} caracteres")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Range(0, 5000, ErrorMessage = "Los puntos deben estar entre 0 y 5000")]
        public int Puntos { get; set; }
    }
}
