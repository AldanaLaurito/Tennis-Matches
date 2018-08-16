using Baufest.Tennis.Domain.Entities;

namespace Baufest.Tennis.Domain.Interfaces
{
    public interface ITableroBusiness
    {
        Partido IniciarPartido(int id);

        Partido SumarPuntoLocal(int id);

        Partido SumarPuntoVisitante(int id);
    }
}
