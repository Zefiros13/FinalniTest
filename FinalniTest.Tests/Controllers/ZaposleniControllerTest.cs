using FinalniTest.Controllers;
using FinalniTest.Interfaces;
using FinalniTest.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Results;

namespace FinalniTest.Tests.Controllers
{
    [TestClass]
    public class ZaposleniControllerTest
    {
        [TestClass]
        public class DrzaveControllerTest
        {
            [TestMethod]
            public void GetReturnsObjectsWithSameId()
            {
                // Arrange
                var mockRepository = new Mock<IZaposleniRepository>();
                mockRepository.Setup(x => x.GetById(1)).Returns(new Zaposleni() { Id = 1, ImeIPrezime = "Pera Peric", Rola = "Direktor", GodinaRodjenja = 1980, GodinaZaposlenja = 2010, Plata = 3000 });

                var controller = new ZaposleniController(mockRepository.Object);

                // Act
                IHttpActionResult actionResult = controller.GetById(1);
                var contentResult = actionResult as OkNegotiatedContentResult<Zaposleni>;

                // Assert
                Assert.IsNotNull(contentResult);
                Assert.IsNotNull(contentResult.Content);
                Assert.AreEqual(1, contentResult.Content.Id);
            }

            [TestMethod]
            public void PutReturnsBadRequest()
            {
                // Arrange
                var mockRepository = new Mock<IZaposleniRepository>();
                var controller = new ZaposleniController(mockRepository.Object);

                // Act
                IHttpActionResult actionResult = controller.Put(2, new Zaposleni() { Id = 1, ImeIPrezime = "Pera Peric", Rola = "Direktor", GodinaRodjenja = 1980, GodinaZaposlenja = 2010, Plata = 3000 });

                // Assert
                Assert.IsInstanceOfType(actionResult, typeof(BadRequestResult));
            }

            [TestMethod]
            public void GetReturnsMultipleObjects()
            {
                // Arrange
                List<Zaposleni> zaposleni = new List<Zaposleni>();
                zaposleni.Add(new Zaposleni() { Id = 1, ImeIPrezime = "Pera Peric", Rola = "Direktor", GodinaRodjenja = 1980, GodinaZaposlenja = 2010, Plata = 3000 });
                zaposleni.Add(new Zaposleni() { Id = 2, ImeIPrezime = "Mika Mikic", Rola = "Sekretar", GodinaRodjenja = 1985, GodinaZaposlenja = 2011, Plata = 1000 });

                var mockRepository = new Mock<IZaposleniRepository>();
                mockRepository.Setup(x => x.GetAll()).Returns(zaposleni.AsEnumerable());
                var controller = new ZaposleniController(mockRepository.Object);

                // Act
                IEnumerable<Zaposleni> result = controller.GetAll();

                // Assert
                Assert.IsNotNull(result);
                Assert.AreEqual(zaposleni.Count, result.ToList().Count);
                Assert.AreEqual(zaposleni.ElementAt(0), result.ElementAt(0));
                Assert.AreEqual(zaposleni.ElementAt(1), result.ElementAt(1));
            }
        }
    }
}
