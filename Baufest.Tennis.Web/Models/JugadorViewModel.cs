using Baufest.Tennis.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace Baufest.Tennis.Web.Models
{
    public class JugadorViewModel
    {
        public int IdJugador { get; set; }

        [Display(Name = "Nombre del Jugador")]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [StringLength(50, ErrorMessage = "El campo {0} debe tener menos de {1} caracteres")]
        public string Nombre { get; set; }

        [Display(Name = "Puntos del Jugador")]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Range(0, 5000, ErrorMessage = "Los puntos deben estar entre 0 y 5000")]
        public int Puntos { get; set; }

        public JugadorViewModel()
        {

        }

        public JugadorViewModel(Jugador jugadorEntity)
        {
            IdJugador = jugadorEntity.Id;
            Nombre = jugadorEntity.Nombre;
            Puntos = jugadorEntity.Puntos;
        }

        public Jugador ToEntity()
        {
            return new Jugador
            {
                Id = this.IdJugador,
                Nombre = this.Nombre,
                Puntos = this.Puntos
            };
        }
    }


}