using Baufest.Tennis.Domain.Entities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Baufest.Tennis.Web.Models
{
    public class CanchaViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Nombre")]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [StringLength(50, ErrorMessage = "El campo {0} debe tener menos de {1} caracteres")]
        public string Nombre { get; set; }

        [Display(Name = "Dirección")]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [StringLength(200, ErrorMessage = "El campo {0} debe tener menos de {1} caracteres")]
        public string Direccion { get; set; }

        public int PartidosEnElDia { get; set; }

        public virtual ICollection<Partido> Partidoes { get; set; }

        public CanchaViewModel() { }

        public CanchaViewModel(Cancha canchaEntity)
        {
            Id = canchaEntity.Id;
            Nombre = canchaEntity.Nombre;
            Direccion = canchaEntity.Direccion;
            Partidoes = canchaEntity.Partidoes;
        }

        public Cancha ToEntity()
        {
            return new Cancha
            {
                Id = this.Id,
                Nombre = this.Nombre,
                Direccion = this.Direccion
            };
        }
    }
}