using Baufest.Tennis.Domain.Entities;
using Baufest.Tennis.Domain.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Baufest.Tennis.Business.Tests
{
    [TestClass]
    public class CanchaBusinessTest
    {
        [TestMethod]
        [TestCategory("Integrador")]
        public void OperacionesCanchaAddCancha()
        {
            DateTime fecha = DateTime.Now;
            DateTime fechaSinHora = DateTime.Now.Date;


            var canchaRepository = new Mock<ICanchaRepository>();
            canchaRepository.Setup(x => x.AddCancha(It.IsAny<Cancha>())).Callback<Cancha>(c =>
            {
                c.Id = 1;
            });

            var canchaBussiness = new CanchaBusiness(canchaRepository.Object);

            Cancha cancha = new Cancha
            {
                Nombre = "Cancha test"
            };

            canchaBussiness.AddCancha(cancha);

            canchaRepository.Verify(x => x.AddCancha(cancha), Times.Once);
            Assert.AreEqual(1, cancha.Id);
        }

        [TestMethod]
        public void AddCanchaExitoTest()
        {
            var fecha = DateTime.Now.AddDays(1);
            var errores = new List<string>();
            var canchaRepositoryMock = new Mock<ICanchaRepository>();
            var target = new CanchaBusiness(canchaRepositoryMock.Object);
            target.AddCancha(new Cancha { Nombre = "cancha1" });

            canchaRepositoryMock.Verify(
                x =>
                x.AddCancha(
                    It.Is<Cancha>(
                        p => p.Nombre == "cancha1")),
                Times.Once);
        }

        [TestMethod]
        [TestCategory("Integrador")]
        public void OperacionesCanchaListCanchas()
        {
            const int ID_CANCHA_1 = 1;
            const int ID_CANCHA_2 = 2;

            List<Cancha> canchas = new List<Cancha>
            {
                new Cancha { Id = ID_CANCHA_1 },
                new Cancha { Id = ID_CANCHA_2 }
            };

            var canchaRepository = new Mock<ICanchaRepository>();
            canchaRepository.Setup(x => x.ListCanchas()).Returns(canchas);

            var canchaBussiness = new CanchaBusiness(canchaRepository.Object);

            var list = canchaBussiness.ListCanchas();

            canchaRepository.Verify(x => x.ListCanchas(), Times.Once);
            Assert.AreEqual(canchas.Count, list.Count());
            Assert.AreEqual(ID_CANCHA_1, list.First().Id);
            Assert.AreEqual(ID_CANCHA_2, list.Last().Id);
        }
    }
}