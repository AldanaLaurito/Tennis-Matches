using Baufest.Tennis.Domain.Entities;
using Baufest.Tennis.Domain.Enums;
using Baufest.Tennis.Domain.Interfaces;
using Baufest.Tennis.Persistence;
using System.Collections.Generic;
using System.Linq;

namespace Baufest.Tennis.Business
{
    public class JugadorBusiness : IJugadorBusiness
    {
        private readonly IJugadorRepository repository;
        private readonly IPartidoBusiness partidoBusiness;

        public JugadorBusiness()
        {
            this.repository = new JugadorRepository();
            this.partidoBusiness = new PartidoBusiness();
        }

        public JugadorBusiness(IJugadorRepository repository, IPartidoBusiness partidoBusiness)
        {
            this.repository = repository;
            this.partidoBusiness = partidoBusiness;
        }

        public IEnumerable<Jugador> ListJugadores()
        {
            return this.repository.ListJugadores();
        }

        public Jugador GetJugador(int id)
        {
            return this.repository.GetJugador(id);
        }

        public void AddJugador(Jugador jugador)
        {
            this.repository.AddJugador(jugador);
        }

        public void UpdateJugador(Jugador jugador)
        {
            this.repository.UpdateJugador(jugador);
        }

        /// <summary>
        /// Realiza el re-calculo del ranking de un jugador
        /// </summary>
        /// <param name="id">Id del jugador a realizar el re-calculo</param>
        public void ReCalculateRanking(int id)
        {
            // Define las constantes con los indices definidos para el calculo
            const int INDICADOR_GANADOS_LOCAL = 10;
            const int INDICADOR_GANADOS_VISITANTE = 15;
            const int INDICADOR_PERDIDOS_LOCAL = -5;
            //const int INDICADOR_PERDIDOS_VISITANTE = 0;

            // Obtiene al jugador
            var jugador = this.GetJugador(id);

            // Inicializa el ranking inicial
            int ranking = 0;

            // Obtiene los partidos ganados por el jugador y los particiona
            var partidosGanadosLocal = this.partidoBusiness.ListPartidos().Where(m =>
                m.JugadorLocalId == id && m.GamesLocal >= 6
                && m.Estado == EstadoPartido.Finalizado
            );

            var partidosPerdidosLocal = this.partidoBusiness.ListPartidos().Where(m =>
                m.JugadorLocalId == id && m.GamesLocal < 6
                && m.Estado == EstadoPartido.Finalizado
            );

            // Obtiene los partidos perdidos por el jugador y los particiona
            var partidosGanadosVisitante = this.partidoBusiness.ListPartidos().Where(m =>
                m.JugadorVisitanteId == id && m.GamesVisitante >= 6
                && m.Estado == EstadoPartido.Finalizado
            );
            //var partidosPerdidosVisitante = this.partidoBusiness.ListPartidos().Where(m =>
            //  m.JugadorVisitanteId == id && m.GamesVisitante < 6
            //  && m.Estado == EstadoPartido.Finalizado
            //);

            // Realiza el calculo por cada indicador
            ranking += partidosGanadosLocal.Count() * INDICADOR_GANADOS_LOCAL;
            ranking += partidosGanadosVisitante.Count() * INDICADOR_GANADOS_VISITANTE;
            ranking += partidosPerdidosLocal.Count() * INDICADOR_PERDIDOS_LOCAL;
            //ranking += partidosPerdidosVisitante.Count() * INDICADOR_PERDIDOS_VISITANTE;

            // Si el ranking obtenido es menor a 0 se setea en 0
            if (ranking < 0)
            {
                ranking = 0;
            }

            // Se retean los puntos en el jugador
            jugador.Puntos = ranking;

            // Se actualizan los datos en la base
            this.UpdateJugador(jugador);
        }
    }
}