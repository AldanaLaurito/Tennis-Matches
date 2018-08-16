using System;
using System.Collections.Generic;
using System.Linq;

using Baufest.Tennis.Domain.Entities;
using Baufest.Tennis.Domain.Enums;
using Baufest.Tennis.Domain.Interfaces;
using Baufest.Tennis.Persistence;

namespace Baufest.Tennis.Business
{
    public class PartidoBusiness : IPartidoBusiness
    {
        private readonly IPartidoRepository partidoRepository;

        public PartidoBusiness()
        {
            this.partidoRepository = new PartidoRepository();
        }

        public PartidoBusiness(IPartidoRepository partidoRepository)
        {
            this.partidoRepository = partidoRepository;
        }

        public IEnumerable<Partido> ListPartidos()
        {
            var partidos = this.partidoRepository.ListPartidos();
            return partidos.OrderBy(x => x.Estado).ThenBy(x => x.FechaComienzo);
        }

        public Partido GetPartido(int id)
        {
            return this.partidoRepository.GetPartido(id);
        }

        public bool UpdatePartido(int id, DateTime fechaComienzo, int idJugadorLocal, int idJugadorVisitante, int idCancha, List<string> errores)
        {
            if (!this.ValidarPartido(fechaComienzo, idJugadorLocal, idJugadorVisitante, idCancha, errores))
            {
                return false;
            }

            var partido = this.partidoRepository.GetPartido(id);
            partido.FechaComienzo = fechaComienzo;
            partido.JugadorLocalId = idJugadorLocal;
            partido.JugadorVisitanteId = idJugadorVisitante;
            partido.CanchaId = idCancha;

            this.partidoRepository.UpdatePartido(partido);

            return true;
        }

        public bool AddPartido(DateTime fechaComienzo, int idJugadorLocal, int idJugadorVisitante, int idCancha, List<string> errores)
        {
            if (!this.ValidarPartido(fechaComienzo, idJugadorLocal, idJugadorVisitante, idCancha, errores))
            {
                return false;
            }

            var partido = new Partido
            {
                FechaComienzo = fechaComienzo,
                JugadorLocalId = idJugadorLocal,
                JugadorVisitanteId = idJugadorVisitante,
                Estado = EstadoPartido.NoIniciado,
                CanchaId = idCancha
            };

            this.partidoRepository.AddPartido(partido);

            return true;
        }

        public bool DeletePartido(int id, List<string> errores)
        {
            var partido = this.partidoRepository.GetPartido(id);
            if (partido.Estado != EstadoPartido.NoIniciado)
            {
                errores.Add("No se puede eliminar un partido ya iniciado");
                return false;
            }

            this.partidoRepository.DeletePartido(partido);

            return true;
        }

        private bool ValidarPartido(DateTime fechaComienzo, int idJugadorLocal, int idJugadorVisitante, int idCancha, List<string> errores)
        {
            if (fechaComienzo < DateTime.Now)
            {
                errores.Add("La fecha y hora de comienzo debe ser mayor a la actual");
            }

            if (idJugadorLocal == idJugadorVisitante)
            {
                errores.Add("El partido no puede contener dos veces el mismo jugador");
            }

            if (!ValidarCancha(fechaComienzo, idCancha))
            {
                errores.Add("El partido no puede utilizar la misma cancha de otro partido en un intervalo de 4 horas");
            }

            return !errores.Any();
        }

        private bool ValidarCancha(DateTime fechaComienzo, int idCancha)
        {
            // Define una constante para el intervalo horario de un partido para utilizar la misma cancha de otro
            const int INDICE_DIFERENCIA_HORAS = 4;

            // Obtiene todos los partidos para esa cancha
            var partidosParaCancha = this.partidoRepository
                .ListPartidos()
                .Where(x => x.CanchaId == idCancha);

            // Busca todos los partidos que comiencen en un intervalo de 4 horas antes y 4 horas despues
            var partidosEntreHorarios = partidosParaCancha
                .Where(x =>
                    x.FechaComienzo > fechaComienzo.AddHours(-INDICE_DIFERENCIA_HORAS) &&
                    x.FechaComienzo < fechaComienzo.AddHours(INDICE_DIFERENCIA_HORAS)
                );

            // Si hay algun partido entonces no valida la cancha
            return !partidosEntreHorarios.Any();
        }

        public IEnumerable<Partido> ListPartidosSinComenzar()
        {
            var partidos = this.partidoRepository.ListPartidos();
            return partidos.OrderBy(x => x.Estado).ThenBy(x => x.FechaComienzo);
        }

        public IEnumerable<Partido> ListPartidosGanadosDeJugador(int jugadorId)
        {
            // Obtiene todos los partidos finalizados discriminando al jugador ganador
            var partidosConJugadorGanador = this.partidoRepository
                .ListPartidos()
                .Select(x => new
                {
                    Partido = x,
                    Ganador = (x.Estado == EstadoPartido.Finalizado
                        ? (x.GamesLocal > x.GamesVisitante
                            ? x.JugadorLocal
                            : x.JugadorVisitante
                        )
                        : null
                    )
                });

            // Filtra la lista con solo los partidos donde el jugador parametrizado es el ganador
            var partidosGanadosPorJugadorParametrizado = partidosConJugadorGanador
                .Where(x => x.Ganador != null && x.Ganador.Id == jugadorId)
                .Select(x => x.Partido);

            // Devuelve los partidos ganados por el jugador parametrizado
            return partidosGanadosPorJugadorParametrizado;
        }

        public IEnumerable<Partido> ListPartidosPerdidosDeJugador(int jugadorId)
        {
            // Obtiene todos los partidos finalizados discriminando al jugador perdedor
            var partidosConJugadorPerdedor = this.partidoRepository
                .ListPartidos()
                .Select(x => new
                {
                    Partido = x,
                    Perdedor = (x.Estado == EstadoPartido.Finalizado
                        ? (x.GamesLocal > x.GamesVisitante
                            ? x.JugadorVisitante
                            : x.JugadorLocal
                        )
                        : null
                    )
                });

            // Filtra la lista con solo los partidos donde el jugador parametrizado es el perdedor
            var partidosPerdidosPorJugadorParametrizado = partidosConJugadorPerdedor
                .Where(x => x.Perdedor != null && x.Perdedor.Id == jugadorId)
                .Select(x => x.Partido);

            // Devuelve los partidos perdidos por el jugador parametrizado
            return partidosPerdidosPorJugadorParametrizado;
        }
    }
}