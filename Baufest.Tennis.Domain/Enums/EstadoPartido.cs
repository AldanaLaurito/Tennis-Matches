using System.ComponentModel.DataAnnotations;

namespace Baufest.Tennis.Domain.Enums
{
    public enum EstadoPartido
    {
        [Display(Name = "En curso")]
        EnCurso = 1,
        [Display(Name = "No iniciado")]
        NoIniciado = 2,
        [Display(Name = "Finalizado")]
        Finalizado = 3
    }
}