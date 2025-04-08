using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MilibooAPI.Controllers;
using MilibooAPI.Models.EntityFramework;
using MilibooAPI.Models.Repository;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MilibooAPI.Controllers.Tests
{
    [TestClass()]
    public class VillesControllerTests
    {
        private MilibooDBContext context;
        private VillesController controller;
        private IDataRepository<Ville> dataRepository;

        private Mock<IDataRepository<Ville>> mockRepository;
        private VillesController controllerMoq;

        [TestInitialize]
        public void TestInitialize()
        {
            var builder = new DbContextOptionsBuilder<MilibooDBContext>().UseNpgsql();
            context = new MilibooDBContext();

            mockRepository = new Mock<IDataRepository<Ville>>();
            controllerMoq = new VillesController(mockRepository.Object);

            dataRepository = new Mock<IDataRepository<Ville>>().Object;
            controller = new VillesController(dataRepository);
        }

        [TestMethod()]
        public void VillesControllerTest()
        {
            // Assert
            Assert.IsNotNull(controller, "Le controller est nul.");
        }

        [TestMethod()]
        public async Task GetAllVillesTest()
        {
            // Arrange
            var villes = new List<Ville>
            {
                new Ville { NumeroInsee = "75056", Libelleville = "Paris" },
                new Ville { NumeroInsee = "13055", Libelleville = "Marseille" }
            };
            var actionResult = new ActionResult<IEnumerable<Ville>>(villes);
            mockRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(actionResult);

            // Act
            var result = await controllerMoq.GetAllVilles();

            // Assert
            Assert.IsNotNull(result, "Le résultat est nul.");
            Assert.IsInstanceOfType(result, typeof(ActionResult<IEnumerable<Ville>>), "Pas un ActionResult.");
            var okResult = result.Result as OkObjectResult;
            Assert.IsNotNull(okResult, "Pas un OkObjectResult");
            var returnValue = okResult.Value as IEnumerable<Ville>;
            Assert.IsNotNull(returnValue, "Aucune liste de villes retournée");
            Assert.AreEqual(villes.Count, returnValue.Count(), "Nombre de villes incorrect");
        }

        [TestMethod]
        public async Task GetAllVilles_ShouldReturn500_WhenExceptionIsThrown()
        {
            // Arrange
            mockRepository.Setup(repo => repo.GetAllAsync()).ThrowsAsync(new Exception("Database error"));

            // Act
            var result = await controllerMoq.GetAllVilles();

            // Assert
            Assert.IsNull(result.Value);
            Assert.IsInstanceOfType(result.Result, typeof(ObjectResult));
            var statusCodeResult = result.Result as ObjectResult;
            Assert.IsNotNull(statusCodeResult);
            Assert.AreEqual(StatusCodes.Status500InternalServerError, statusCodeResult.StatusCode);
        }

        [TestMethod()]
        public async Task GetVilleByIdTest_ExistingIdPassed_ReturnsRightItem()
        {
            // Arrange
            string numeroInsee = "75056";
            var ville = new Ville { NumeroInsee = numeroInsee, Libelleville = "Paris" };
            var actionResult = new ActionResult<Ville>(ville);
            mockRepository.Setup(repo => repo.GetByStringAsync(numeroInsee)).ReturnsAsync(actionResult);

            // Act
            var result = await controllerMoq.GetVilleById(numeroInsee);

            // Assert
            Assert.IsNotNull(result, "Aucun résultat.");
            Assert.IsInstanceOfType(result, typeof(ActionResult<Ville>), "Pas un ActionResult.");
            Assert.IsNull(result.Result, "Il y a une erreur.");
            Assert.IsInstanceOfType(result.Value, typeof(Ville), "Pas une Ville");
            Assert.AreEqual(ville.NumeroInsee, result.Value.NumeroInsee, "NumeroInsee pas identiques.");
            Assert.AreEqual(ville.Libelleville, result.Value.Libelleville, "Libelleville pas identiques.");
        }

        [TestMethod]
        public async Task GetVilleById_ShouldReturn404_WhenVilleNotFound()
        {
            // Arrange
            string numeroInsee = "99999";
            mockRepository.Setup(repo => repo.GetByStringAsync(numeroInsee)).ReturnsAsync((ActionResult<Ville>)null);

            // Act
            var result = await controllerMoq.GetVilleById(numeroInsee);

            // Assert
            Assert.IsInstanceOfType(result.Result, typeof(NotFoundResult));
        }

        [TestMethod]
        public async Task GetVilleById_ShouldReturn500_WhenExceptionIsThrown()
        {
            // Arrange
            string numeroInsee = "75056";
            mockRepository.Setup(repo => repo.GetByStringAsync(numeroInsee)).ThrowsAsync(new Exception("Database error"));

            // Act
            var result = await controllerMoq.GetVilleById(numeroInsee);

            // Assert
            Assert.IsInstanceOfType(result.Result, typeof(ObjectResult));
            var statusCodeResult = result.Result as ObjectResult;
            Assert.IsNotNull(statusCodeResult);
            Assert.AreEqual(StatusCodes.Status500InternalServerError, statusCodeResult.StatusCode);
        }

        [TestMethod]
        public async Task PostVilleTest_ValidCreate()
        {
            // Arrange
            var ville = new Ville { NumeroInsee = "69123", Libelleville = "Lyon" };
            mockRepository.Setup(repo => repo.AddAsync(ville)).Returns(Task.CompletedTask);

            // Act
            var result = await controllerMoq.PostVille(ville);

            // Assert
            var createdResult = result.Result as CreatedAtActionResult;
            Assert.IsNotNull(createdResult);
            Assert.AreEqual("GetVilleById", createdResult.ActionName);
            Assert.AreEqual(ville.NumeroInsee, createdResult.RouteValues["id"]);
            Assert.AreEqual(ville, createdResult.Value);
        }

        [TestMethod]
        public async Task PostVille_ShouldReturnBadRequest_WhenModelStateIsInvalid()
        {
            // Arrange
            var ville = new Ville { NumeroInsee = "69123", Libelleville = "Lyon" };
            controllerMoq.ModelState.AddModelError("NumeroInsee", "Required");

            // Act
            var result = await controllerMoq.PostVille(ville);

            // Assert
            Assert.IsInstanceOfType(result.Result, typeof(BadRequestObjectResult));
        }

        [TestMethod]
        public async Task PostVille_ShouldReturn500_WhenExceptionIsThrown()
        {
            // Arrange
            var ville = new Ville { NumeroInsee = "69123", Libelleville = "Lyon" };
            mockRepository.Setup(repo => repo.AddAsync(ville)).ThrowsAsync(new Exception("Database error"));

            // Act
            var result = await controllerMoq.PostVille(ville);

            // Assert
            Assert.IsInstanceOfType(result.Result, typeof(ObjectResult));
            var statusCodeResult = result.Result as ObjectResult;
            Assert.IsNotNull(statusCodeResult);
            Assert.AreEqual(StatusCodes.Status500InternalServerError, statusCodeResult.StatusCode);
        }

        [TestMethod]
        public async Task PutVilleTest_ValidUpdate()
        {
            // Arrange
            string numeroInsee = "75056";
            var ville = new Ville { NumeroInsee = numeroInsee, Libelleville = "Paris (Updated)" };
            var existingVille = new Ville { NumeroInsee = numeroInsee, Libelleville = "Paris" };

            var actionResult = new ActionResult<Ville>(existingVille);
            mockRepository.Setup(repo => repo.GetByStringAsync(numeroInsee)).ReturnsAsync(actionResult);
            mockRepository.Setup(repo => repo.UpdateAsync(existingVille, ville)).Returns(Task.CompletedTask);

            // Act
            var result = await controllerMoq.PutVille(numeroInsee, ville);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NoContentResult));
        }

        [TestMethod]
        public async Task PutVille_ShouldReturnBadRequest_WhenIdsDoNotMatch()
        {
            // Arrange
            string numeroInsee = "75056";
            var ville = new Ville { NumeroInsee = "69123", Libelleville = "Lyon" };

            // Act
            var result = await controllerMoq.PutVille(numeroInsee, ville);

            // Assert
            Assert.IsInstanceOfType(result, typeof(BadRequestResult));
        }

        [TestMethod]
        public async Task PutVille_ShouldReturnBadRequest_WhenModelStateIsInvalid()
        {
            // Arrange
            string numeroInsee = "75056";
            var ville = new Ville { NumeroInsee = numeroInsee, Libelleville = "Paris" };
            controllerMoq.ModelState.AddModelError("Libelleville", "Required");

            // Act
            var result = await controllerMoq.PutVille(numeroInsee, ville);

            // Assert
            Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult));
        }

        [TestMethod]
        public async Task PutVille_ShouldReturnNotFound_WhenVilleDoesNotExist()
        {
            // Arrange
            string numeroInsee = "99999";
            var ville = new Ville { NumeroInsee = numeroInsee, Libelleville = "Unknown" };
            mockRepository.Setup(repo => repo.GetByStringAsync(numeroInsee)).ReturnsAsync((ActionResult<Ville>)null);

            // Act
            var result = await controllerMoq.PutVille(numeroInsee, ville);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        [TestMethod]
        public async Task PutVille_ShouldReturn500_WhenExceptionIsThrown()
        {
            // Arrange
            string numeroInsee = "75056";
            var ville = new Ville { NumeroInsee = numeroInsee, Libelleville = "Paris (Updated)" };
            var existingVille = new Ville { NumeroInsee = numeroInsee, Libelleville = "Paris" };

            var actionResult = new ActionResult<Ville>(existingVille);
            mockRepository.Setup(repo => repo.GetByStringAsync(numeroInsee)).ReturnsAsync(actionResult);
            mockRepository.Setup(repo => repo.UpdateAsync(existingVille, ville)).ThrowsAsync(new Exception("Database error"));

            // Act
            var result = await controllerMoq.PutVille(numeroInsee, ville);

            // Assert
            Assert.IsInstanceOfType(result, typeof(ObjectResult));
            var statusCodeResult = result as ObjectResult;
            Assert.IsNotNull(statusCodeResult);
            Assert.AreEqual(StatusCodes.Status500InternalServerError, statusCodeResult.StatusCode);
        }

        [TestMethod]
        public async Task DeleteVilleTest_ValidDelete()
        {
            // Arrange
            string numeroInsee = "75056";
            var ville = new Ville { NumeroInsee = numeroInsee, Libelleville = "Paris" };
            var actionResult = new ActionResult<Ville>(ville);
            mockRepository.Setup(repo => repo.GetByStringAsync(numeroInsee)).ReturnsAsync(actionResult);
            mockRepository.Setup(repo => repo.DeleteAsync(ville)).Returns(Task.CompletedTask);

            // Act
            var result = await controllerMoq.DeleteVille(numeroInsee);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NoContentResult));
        }

        [TestMethod]
        public async Task DeleteVille_ShouldReturnNotFound_WhenVilleDoesNotExist()
        {
            // Arrange
            string numeroInsee = "99999";
            mockRepository.Setup(repo => repo.GetByStringAsync(numeroInsee)).ReturnsAsync((ActionResult<Ville>)null);

            // Act
            var result = await controllerMoq.DeleteVille(numeroInsee);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        [TestMethod]
        public async Task DeleteVille_ShouldReturn500_WhenExceptionIsThrown()
        {
            // Arrange
            string numeroInsee = "75056";
            var ville = new Ville { NumeroInsee = numeroInsee, Libelleville = "Paris" };
            var actionResult = new ActionResult<Ville>(ville);
            mockRepository.Setup(repo => repo.GetByStringAsync(numeroInsee)).ReturnsAsync(actionResult);
            mockRepository.Setup(repo => repo.DeleteAsync(ville)).ThrowsAsync(new Exception("Database error"));

            // Act
            var result = await controllerMoq.DeleteVille(numeroInsee);

            // Assert
            Assert.IsInstanceOfType(result, typeof(ObjectResult));
            var statusCodeResult = result as ObjectResult;
            Assert.IsNotNull(statusCodeResult);
            Assert.AreEqual(StatusCodes.Status500InternalServerError, statusCodeResult.StatusCode);
        }
    }
}