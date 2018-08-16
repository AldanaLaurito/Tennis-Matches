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
    public class PartidoBusinessTest
    {
        [TestMethod]
        public void ListPartidosOrdenCorrectoTest()
        {
            var partidos = new List<Partido>
                               {
                                   new Partido
                                       {
                                           Id = 1,
                                           Estado = EstadoPartido.NoIniciado,
                                           FechaComienzo = new DateTime(2016, 10, 1)
                                       },
                                   new Partido
                                       {
                                           Id = 2,
                                           Estado = EstadoPartido.EnCurso,
                                           FechaComienzo = new DateTime(2016, 10, 2)
                                       },
                                   new Partido
                                       {
                                           Id = 3,
                                           Estado = EstadoPartido.EnCurso,
                                           FechaComienzo = new DateTime(2016, 10, 3)
                                       },
                                   new Partido
                                       {
                                           Id = 4,
                                           Estado = EstadoPartido.Finalizado,
                                           FechaComienzo = new DateTime(2016, 10, 5)
                                       },
                                   new Partido
                                       {
                                           Id = 5,
                                           Estado = EstadoPartido.Finalizado,
                                           FechaComienzo = new DateTime(2016, 10, 4)
                                       }
                               };
            var partidoRepositoryMock = new Mock<IPartidoRepository>();
            partidoRepositoryMock.Setup(x => x.ListPartidos()).Returns(partidos);
            var target = new PartidoBusiness(partidoRepositoryMock.Object);
            var res = target.ListPartidos().ToList();
            Assert.AreEqual(5, res.Count);
            var expectedOrder = new[] { 2, 3, 1, 5, 4 };
            CollectionAssert.AreEqual(expectedOrder, res.Select(x => x.Id).ToArray());
        }

        [TestMethod]
        public void UpdatePartidoExitoTest()
        {
            var partido = new Partido
            {
                Id = 1,
            };
            var errores = new List<string>();
            var partidoRepositoryMock = new Mock<IPartidoRepository>();
            partidoRepositoryMock.Setup(x => x.GetPartido(1)).Returns(partido);
            var target = new PartidoBusiness(partidoRepositoryMock.Object);
            var res = target.UpdatePartido(1, DateTime.Now.AddDays(1), 11, 12, 1, errores);
            Assert.IsTrue(res);
            partidoRepositoryMock.Verify(x => x.UpdatePartido(partido), Times.Once);
        }

        [TestMethod]
        public void UpdatePartidoIgualJugadorTest()
        {
            var partido = new Partido
            {
                Id = 1,
            };
            var errores = new List<string>();
            var partidoRepositoryMock = new Mock<IPartidoRepository>();
            partidoRepositoryMock.Setup(x => x.GetPartido(1)).Returns(partido);
            var target = new PartidoBusiness(partidoRepositoryMock.Object);
            var res = target.UpdatePartido(1, DateTime.Now.AddDays(1), 11, 11, 1, errores);
            Assert.IsFalse(res);
            Assert.AreEqual(1, errores.Count);
            partidoRepositoryMock.Verify(x => x.UpdatePartido(It.IsAny<Partido>()), Times.Never);
        }

        [TestMethod]
        public void UpdatePartidoFechaAnteriorTest()
        {
            var partido = new Partido
            {
                Id = 1,
            };
            var errores = new List<string>();
            var partidoRepositoryMock = new Mock<IPartidoRepository>();
            partidoRepositoryMock.Setup(x => x.GetPartido(1)).Returns(partido);
            var target = new PartidoBusiness(partidoRepositoryMock.Object);
            var res = target.UpdatePartido(1, DateTime.Now.AddDays(-1), 11, 12, 1, errores);
            Assert.IsFalse(res);
            Assert.AreEqual(1, errores.Count);
            partidoRepositoryMock.Verify(x => x.UpdatePartido(It.IsAny<Partido>()), Times.Never);
        }

        [TestMethod]
        public void AddPartidoExitoTest()
        {
            var fecha = DateTime.Now.AddDays(1);
            var errores = new List<string>();
            var partidoRepositoryMock = new Mock<IPartidoRepository>();
            var target = new PartidoBusiness(partidoRepositoryMock.Object);
            var res = target.AddPartido(fecha, 11, 12, 1, errores);
            Assert.IsTrue(res);
            partidoRepositoryMock.Verify(
                x =>
                x.AddPartido(
                    It.Is<Partido>(
                        p => p.JugadorLocalId == 11 && p.JugadorVisitanteId == 12 && p.FechaComienzo == fecha)),
                Times.Once);
        }

        [TestMethod]
        public void AddPartidoIgualJugadorTest()
        {
            var errores = new List<string>();
            var partidoRepositoryMock = new Mock<IPartidoRepository>();
            var target = new PartidoBusiness(partidoRepositoryMock.Object);
            var res = target.AddPartido(DateTime.Now.AddDays(1), 11, 11, 1, errores);
            Assert.IsFalse(res);
            Assert.AreEqual(1, errores.Count);
            partidoRepositoryMock.Verify(x => x.AddPartido(It.IsAny<Partido>()), Times.Never);
        }

        [TestMethod]
        public void AddPartidoFechaAnteriorTest()
        {
            var errores = new List<string>();
            var partidoRepositoryMock = new Mock<IPartidoRepository>();
            var target = new PartidoBusiness(partidoRepositoryMock.Object);
            var res = target.AddPartido(DateTime.Now.AddDays(-1), 11, 12, 1, errores);
            Assert.IsFalse(res);
            Assert.AreEqual(1, errores.Count);
            partidoRepositoryMock.Verify(x => x.AddPartido(It.IsAny<Partido>()), Times.Never);
        }

        [TestMethod]
        public void DeletePartidoExitoTest()
        {
            var partido = new Partido
            {
                Id = 1,
                Estado = EstadoPartido.NoIniciado
            };
            var errores = new List<string>();
            var partidoRepositoryMock = new Mock<IPartidoRepository>();
            partidoRepositoryMock.Setup(x => x.GetPartido(1)).Returns(partido);
            var target = new PartidoBusiness(partidoRepositoryMock.Object);
            var res = target.DeletePartido(1, errores);
            Assert.IsTrue(res);
            partidoRepositoryMock.Verify(x => x.DeletePartido(partido), Times.Once);
        }

        [TestMethod]
        public void DeletePartidoEstadoIncorrectoTest()
        {
            var partido = new Partido
            {
                Id = 1,
                Estado = EstadoPartido.EnCurso
            };
            var errores = new List<string>();
            var partidoRepositoryMock = new Mock<IPartidoRepository>();
            partidoRepositoryMock.Setup(x => x.GetPartido(1)).Returns(partido);
            var target = new PartidoBusiness(partidoRepositoryMock.Object);
            var res = target.DeletePartido(1, errores);
            Assert.IsFalse(res);
            Assert.AreEqual(1, errores.Count);
            partidoRepositoryMock.Verify(x => x.DeletePartido(It.IsAny<Partido>()), Times.Never);
        }

        [TestMethod]
        [TestCategory("Integrador")]
        public void ValidarCanchaPartidosEnIntervaloDe4Horas()
        {
            DateTime fechaComienzo = DateTime.Now;

            Cancha cancha = new Cancha
            {
                Id = 1
            };

            var partidoRepository = new Mock<IPartidoRepository>();
            partidoRepository.Setup(x => x.ListPartidos()).Returns(new List<Partido>
            {
                new Partido
                {
                    FechaComienzo = fechaComienzo,
                    Cancha = cancha,
                    CanchaId = cancha.Id
                }
            });

            var partidoBussiness = new PartidoBusiness(partidoRepository.Object);

            List<string> errors = new List<string>();
            partidoBussiness.AddPartido(fechaComienzo.AddHours(4).AddMinutes(-1), 0, 1, cancha.Id, errors);

            partidoRepository.Verify(x => x.ListPartidos(), Times.Once);
            Assert.AreEqual(true, errors.Count > 0);

            errors.Clear();

            partidoBussiness.AddPartido(fechaComienzo.AddHours(-4).AddMinutes(1), 0, 1, cancha.Id, errors);

            Assert.AreEqual(true, errors.Count > 0);

            errors.Clear();

            partidoBussiness.AddPartido(fechaComienzo.AddHours(5), 0, 1, cancha.Id, errors);

            Assert.AreEqual(true, errors.Count == 0);
        }
    }
}