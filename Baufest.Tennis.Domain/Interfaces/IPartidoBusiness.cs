using System;
using System.Collections.Generic;
using Baufest.Tennis.Domain.Entities;

namespace Baufest.Tennis.Domain.Interfaces
{
    public interface IPartidoBusiness
    {
        IEnumerable<Partido> ListPartidos();

        Partido GetPartido(int id);

        bool UpdatePartido(int id, DateTime fechaComienzo, int idJugadorLocal, int idJugadorVisitante, int idCancha, List<string> errores);

        bool AddPartido(DateTime fechaComienzo, int idJugadorLocal, int idJugadorVisitante, int idCancha, List<string> errores);

        bool DeletePartido(int id, List<string> errores);

        IEnumerable<Partido> ListPartidosSinComenzar();

        IEnumerable<Partido> ListPartidosGanadosDeJugador(int jugadorId);

        IEnumerable<Partido> ListPartidosPerdidosDeJugador(int jugadorId);
    }
}
