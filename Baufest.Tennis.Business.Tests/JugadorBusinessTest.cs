using Baufest.Tennis.Business;
using System;
using System.Collections.Generic;
using System.Linq;

using Baufest.Tennis.Domain.Entities;
using Baufest.Tennis.Domain.Enums;
using Baufest.Tennis.Domain.Interfaces;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

namespace Baufest.Tennis.Business.Tests
{
    [TestClass]
    public class JugadorBusinessTest
    {
        [TestMethod]
        [TestCategory("Integrador")]
        public void CalculoRankingGano1PartidoLocal()
        {
            const int ID_JUGADOR = 1;

            var jugador = new Jugador
            {
                Id = ID_JUGADOR,
                Puntos = 0
            };

            var partidos = new List<Partido>
            {
                GetPartidoGanadoLocal(jugador)
            };

            var jugadorBusiness = GetJugadorBusiness(jugador, partidos);

            jugadorBusiness.ReCalculateRanking(jugador.Id);

            Assert.AreEqual(10, jugador.Puntos);
        }

        [TestMethod]
        [TestCategory("Integrador")]
        public void CalculoRankingGano3PartidosLocal2PartidosVisitante()
        {
            const int ID_JUGADOR = 1;

            var jugador = new Jugador
            {
                Id = ID_JUGADOR,
                Puntos = 0
            };

            var partidos = new List<Partido>
            {
                GetPartidoGanadoLocal(jugador), // +10
                GetPartidoGanadoLocal(jugador), // +10
                GetPartidoGanadoLocal(jugador), // +10

                GetPartidoGanadoVisitante(jugador), // +15
                GetPartidoGanadoVisitante(jugador), // +15
            };

            var jugadorRepository = new Mock<IJugadorRepository>();
            jugadorRepository.Setup(x => x.GetJugador(jugador.Id)).Returns(jugador);

            var partidoBussiness = new Mock<IPartidoBusiness>();
            partidoBussiness.Setup(x => x.ListPartidos()).Returns(partidos);

            //var partidoBussiness = new PartidoBusiness(partidoRepository.Object);

            var jugadorBusiness = new JugadorBusiness(jugadorRepository.Object, partidoBussiness.Object);

            jugadorBusiness.ReCalculateRanking(jugador.Id);

            Assert.AreEqual(60, jugador.Puntos);
        }

        [TestMethod]
        [TestCategory("Integrador")]
        public void CalculoRankingPerdio2PartidosLocalConRainking0()
        {
            const int ID_JUGADOR = 1;

            var jugador = new Jugador
            {
                Id = ID_JUGADOR,
                Puntos = 0
            };

            var partidos = new List<Partido>
            {
                GetPartidoPerdidoLocal(jugador), // -5
                GetPartidoPerdidoLocal(jugador), // -5
            };

            var jugadorBusiness = GetJugadorBusiness(jugador, partidos);

            jugadorBusiness.ReCalculateRanking(jugador.Id);

            Assert.AreEqual(0, jugador.Puntos);
        }

        [TestMethod]
        [TestCategory("Integrador")]
        public void CalculoRankingPerdio2PartidosLocalConRainking50()
        {
            const int ID_JUGADOR = 1;

            var jugador = new Jugador
            {
                Id = ID_JUGADOR,
                Puntos = 50
            };

            var partidos = new List<Partido>
            {
                GetPartidoPerdidoLocal(jugador), // -5
                GetPartidoPerdidoLocal(jugador), // -5
            };

            var jugadorBusiness = GetJugadorBusiness(jugador, partidos);

            jugadorBusiness.ReCalculateRanking(jugador.Id);

            Assert.AreEqual(0, jugador.Puntos);
        }

        [TestMethod]
        [TestCategory("Integrador")]
        public void CalculoRankingPerdio2PartidosVisitanteConRanking50()
        {
            const int ID_JUGADOR = 1;

            var jugador = new Jugador
            {
                Id = ID_JUGADOR,
                Puntos = 50
            };

            var partidos = new List<Partido>
            {
                GetPartidoPerdidoVisitante(jugador), // -0
                GetPartidoPerdidoVisitante(jugador), // -0
            };

            var jugadorBusiness = GetJugadorBusiness(jugador, partidos);

            jugadorBusiness.ReCalculateRanking(jugador.Id);

            Assert.AreEqual(0, jugador.Puntos);
        }

        [TestMethod]
        [TestCategory("Integrador")]
        public void CalculoRankingGano2Local1VisitantePerdio3Local2Visitante()
        {
            const int ID_JUGADOR = 1;

            var jugador = new Jugador
            {
                Id = ID_JUGADOR,
                Puntos = 0
            };

            var partidos = new List<Partido>
            {
                GetPartidoGanadoLocal(jugador), // +10
                GetPartidoGanadoLocal(jugador), // +10

                GetPartidoGanadoVisitante(jugador), // +15

                GetPartidoPerdidoLocal(jugador), // -5
                GetPartidoPerdidoLocal(jugador), // -5
                GetPartidoPerdidoLocal(jugador), // -5

                GetPartidoPerdidoVisitante(jugador), // -0
                GetPartidoPerdidoVisitante(jugador), // -0
            };

            var jugadorBusiness = GetJugadorBusiness(jugador, partidos);

            jugadorBusiness.ReCalculateRanking(jugador.Id);

            Assert.AreEqual(20, jugador.Puntos);
        }

        #region [Helpers]

        private Partido GetPartidoGanadoLocal(Jugador jugador)
        {
            return new Partido
            {
                Estado = EstadoPartido.Finalizado,

                GamesLocal = 6,
                GamesVisitante = 5,

                JugadorLocal = jugador,
                JugadorLocalId = jugador.Id
            };
        }

        private Partido GetPartidoPerdidoLocal(Jugador jugador)
        {
            return new Partido
            {
                Estado = EstadoPartido.Finalizado,

                GamesLocal = 5,
                GamesVisitante = 6,

                JugadorLocal = jugador,
                JugadorLocalId = jugador.Id
            };
        }

        private Partido GetPartidoPerdidoVisitante(Jugador jugador)
        {
            return new Partido
            {
                Estado = EstadoPartido.Finalizado,

                GamesLocal = 6,
                GamesVisitante = 5,

                JugadorVisitante = jugador,
                JugadorVisitanteId = jugador.Id
            };
        }

        private Partido GetPartidoGanadoVisitante(Jugador jugador)
        {
            return new Partido
            {
                Estado = EstadoPartido.Finalizado,

                GamesLocal = 5,
                GamesVisitante = 6,

                JugadorVisitante = jugador,
                JugadorVisitanteId = jugador.Id
            };
        }

        private JugadorBusiness GetJugadorBusiness(Jugador jugador, List<Partido> partidos)
        {
            var jugadorRepository = new Mock<IJugadorRepository>();
            jugadorRepository.Setup(x => x.GetJugador(It.IsAny<int>())).Returns(jugador);

            var partidoRepository = new Mock<IPartidoRepository>();
            partidoRepository.Setup(x => x.ListPartidos()).Returns(partidos);

            var partidoBussiness = new PartidoBusiness(partidoRepository.Object);

            var jugadorBusiness = new JugadorBusiness(jugadorRepository.Object, partidoBussiness);

            return jugadorBusiness;
        }

        #endregion
    }
}