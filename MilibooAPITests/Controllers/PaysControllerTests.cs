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
    public class PaysControllerTests
    {
        private MilibooDBContext context;
        private PaysController controller;
        private IDataRepository<Pays> dataRepository;

        private Mock<IDataRepository<Pays>> mockRepository;
        private PaysController controllerMoq;

        [TestInitialize]
        public void TestInitialize()
        {
            var builder = new DbContextOptionsBuilder<MilibooDBContext>().UseNpgsql();
            context = new MilibooDBContext();

            mockRepository = new Mock<IDataRepository<Pays>>();
            controllerMoq = new PaysController(mockRepository.Object);

            dataRepository = new Mock<IDataRepository<Pays>>().Object;
            controller = new PaysController(dataRepository);
        }

        [TestMethod()]
        public void PaysControllerTest()
        {
            // Assert
            Assert.IsNotNull(controller, "Le controller est nul.");
        }

        [TestMethod()]
        public async Task GetAllPaysTest()
        {
            // Arrange
            var pays = new List<Pays>
            {
                new Pays { PaysId = "FR", Libellepays = "France" },
                new Pays { PaysId = "CH", Libellepays = "Suisse" }
            };
            var actionResult = new ActionResult<IEnumerable<Pays>>(pays);
            mockRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(actionResult);

            // Act
            var result = await controllerMoq.GetAllPays();

            // Assert
            Assert.IsNotNull(result, "Le résultat est nul.");
            Assert.IsInstanceOfType(result, typeof(ActionResult<IEnumerable<Pays>>), "Pas un ActionResult.");
            var okResult = result.Result as OkObjectResult;
            Assert.IsNotNull(okResult, "Pas un OkObjectResult");
            var returnValue = okResult.Value as IEnumerable<Pays>;
            Assert.IsNotNull(returnValue, "Aucune liste de pays retournée");
            Assert.AreEqual(pays.Count, returnValue.Count(), "Nombre de pays incorrect");
        }

        [TestMethod]
        public async Task GetAllPays_ShouldReturn500_WhenExceptionIsThrown()
        {
            // Arrange
            mockRepository.Setup(repo => repo.GetAllAsync()).ThrowsAsync(new Exception("Database error"));

            // Act
            var result = await controllerMoq.GetAllPays();

            // Assert
            Assert.IsNull(result.Value);
            Assert.IsInstanceOfType(result.Result, typeof(ObjectResult));
            var statusCodeResult = result.Result as ObjectResult;
            Assert.IsNotNull(statusCodeResult);
            Assert.AreEqual(StatusCodes.Status500InternalServerError, statusCodeResult.StatusCode);
        }

        [TestMethod()]
        public async Task GetPaysByIdTest_ExistingIdPassed_ReturnsRightItem()
        {
            // Arrange
            string paysId = "FR";
            var pays = new Pays { PaysId = paysId, Libellepays = "France" };
            var actionResult = new ActionResult<Pays>(pays);
            mockRepository.Setup(repo => repo.GetByStringAsync(paysId)).ReturnsAsync(actionResult);

            // Act
            var result = await controllerMoq.GetPaysById(paysId);

            // Assert
            Assert.IsNotNull(result, "Aucun résultat.");
            Assert.IsInstanceOfType(result, typeof(ActionResult<Pays>), "Pas un ActionResult.");
            Assert.IsNull(result.Result, "Il y a une erreur.");
            Assert.IsInstanceOfType(result.Value, typeof(Pays), "Pas un Pays");
            Assert.AreEqual(pays.PaysId, result.Value.PaysId, "PaysId pas identiques.");
            Assert.AreEqual(pays.Libellepays, result.Value.Libellepays, "Libellepays pas identiques.");
        }

        [TestMethod]
        public async Task GetPaysById_ShouldReturn404_WhenPaysNotFound()
        {
            // Arrange
            string paysId = "XX";
            mockRepository.Setup(repo => repo.GetByStringAsync(paysId)).ReturnsAsync((ActionResult<Pays>)null);

            // Act
            var result = await controllerMoq.GetPaysById(paysId);

            // Assert
            Assert.IsInstanceOfType(result.Result, typeof(NotFoundResult));
        }

        [TestMethod]
        public async Task GetPaysById_ShouldReturn500_WhenExceptionIsThrown()
        {
            // Arrange
            string paysId = "FR";
            mockRepository.Setup(repo => repo.GetByStringAsync(paysId)).ThrowsAsync(new Exception("Database error"));

            // Act
            var result = await controllerMoq.GetPaysById(paysId);

            // Assert
            Assert.IsInstanceOfType(result.Result, typeof(ObjectResult));
            var statusCodeResult = result.Result as ObjectResult;
            Assert.IsNotNull(statusCodeResult);
            Assert.AreEqual(StatusCodes.Status500InternalServerError, statusCodeResult.StatusCode);
        }

        [TestMethod]
        public async Task PostPaysTest_ValidCreate()
        {
            // Arrange
            var pays = new Pays { PaysId = "BE", Libellepays = "Belgique" };
            mockRepository.Setup(repo => repo.AddAsync(pays)).Returns(Task.CompletedTask);

            // Act
            var result = await controllerMoq.PostPays(pays);

            // Assert
            var createdResult = result.Result as CreatedAtActionResult;
            Assert.IsNotNull(createdResult);
            Assert.AreEqual("GetPaysById", createdResult.ActionName);
            Assert.AreEqual(pays.PaysId, createdResult.RouteValues["id"]);
            Assert.AreEqual(pays, createdResult.Value);
        }

        [TestMethod]
        public async Task PostPays_ShouldReturnBadRequest_WhenModelStateIsInvalid()
        {
            // Arrange
            var pays = new Pays { PaysId = "BE", Libellepays = "Belgique" };
            controllerMoq.ModelState.AddModelError("PaysId", "Required");

            // Act
            var result = await controllerMoq.PostPays(pays);

            // Assert
            Assert.IsInstanceOfType(result.Result, typeof(BadRequestObjectResult));
        }

        [TestMethod]
        public async Task PostPays_ShouldReturn500_WhenExceptionIsThrown()
        {
            // Arrange
            var pays = new Pays { PaysId = "BE", Libellepays = "Belgique" };
            mockRepository.Setup(repo => repo.AddAsync(pays)).ThrowsAsync(new Exception("Database error"));

            // Act
            var result = await controllerMoq.PostPays(pays);

            // Assert
            Assert.IsInstanceOfType(result.Result, typeof(ObjectResult));
            var statusCodeResult = result.Result as ObjectResult;
            Assert.IsNotNull(statusCodeResult);
            Assert.AreEqual(StatusCodes.Status500InternalServerError, statusCodeResult.StatusCode);
        }

        [TestMethod]
        public async Task PutPaysTest_ValidUpdate()
        {
            // Arrange
            string paysId = "FR";
            var pays = new Pays { PaysId = paysId, Libellepays = "France (Updated)" };
            var existingPays = new Pays { PaysId = paysId, Libellepays = "France" };

            var actionResult = new ActionResult<Pays>(existingPays);
            mockRepository.Setup(repo => repo.GetByStringAsync(paysId)).ReturnsAsync(actionResult);
            mockRepository.Setup(repo => repo.UpdateAsync(existingPays, pays)).Returns(Task.CompletedTask);

            // Act
            var result = await controllerMoq.PutPays(paysId, pays);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NoContentResult));
        }

        [TestMethod]
        public async Task PutPays_ShouldReturnBadRequest_WhenIdsDoNotMatch()
        {
            // Arrange
            string paysId = "FR";
            var pays = new Pays { PaysId = "CH", Libellepays = "Suisse" };

            // Act
            var result = await controllerMoq.PutPays(paysId, pays);

            // Assert
            Assert.IsInstanceOfType(result, typeof(BadRequestResult));
        }

        [TestMethod]
        public async Task PutPays_ShouldReturnBadRequest_WhenModelStateIsInvalid()
        {
            // Arrange
            string paysId = "FR";
            var pays = new Pays { PaysId = paysId, Libellepays = "France" };
            controllerMoq.ModelState.AddModelError("Libellepays", "Required");

            // Act
            var result = await controllerMoq.PutPays(paysId, pays);

            // Assert
            Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult));
        }

        [TestMethod]
        public async Task PutPays_ShouldReturnNotFound_WhenPaysDoesNotExist()
        {
            // Arrange
            string paysId = "XX";
            var pays = new Pays { PaysId = paysId, Libellepays = "Unknown" };
            mockRepository.Setup(repo => repo.GetByStringAsync(paysId)).ReturnsAsync((ActionResult<Pays>)null);

            // Act
            var result = await controllerMoq.PutPays(paysId, pays);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        [TestMethod]
        public async Task PutPays_ShouldReturn500_WhenExceptionIsThrown()
        {
            // Arrange
            string paysId = "FR";
            var pays = new Pays { PaysId = paysId, Libellepays = "France (Updated)" };
            var existingPays = new Pays { PaysId = paysId, Libellepays = "France" };

            var actionResult = new ActionResult<Pays>(existingPays);
            mockRepository.Setup(repo => repo.GetByStringAsync(paysId)).ReturnsAsync(actionResult);
            mockRepository.Setup(repo => repo.UpdateAsync(existingPays, pays)).ThrowsAsync(new Exception("Database error"));

            // Act
            var result = await controllerMoq.PutPays(paysId, pays);

            // Assert
            Assert.IsInstanceOfType(result, typeof(ObjectResult));
            var statusCodeResult = result as ObjectResult;
            Assert.IsNotNull(statusCodeResult);
            Assert.AreEqual(StatusCodes.Status500InternalServerError, statusCodeResult.StatusCode);
        }

        [TestMethod]
        public async Task DeletePaysTest_ValidDelete()
        {
            // Arrange
            string paysId = "FR";
            var pays = new Pays { PaysId = paysId, Libellepays = "France" };
            var actionResult = new ActionResult<Pays>(pays);
            mockRepository.Setup(repo => repo.GetByStringAsync(paysId)).ReturnsAsync(actionResult);
            mockRepository.Setup(repo => repo.DeleteAsync(pays)).Returns(Task.CompletedTask);

            // Act
            var result = await controllerMoq.DeletePays(paysId);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NoContentResult));
        }

        [TestMethod]
        public async Task DeletePays_ShouldReturnNotFound_WhenPaysDoesNotExist()
        {
            // Arrange
            string paysId = "XX";
            mockRepository.Setup(repo => repo.GetByStringAsync(paysId)).ReturnsAsync((ActionResult<Pays>)null);

            // Act
            var result = await controllerMoq.DeletePays(paysId);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        [TestMethod]
        public async Task DeletePays_ShouldReturn500_WhenExceptionIsThrown()
        {
            // Arrange
            string paysId = "FR";
            var pays = new Pays { PaysId = paysId, Libellepays = "France" };
            var actionResult = new ActionResult<Pays>(pays);
            mockRepository.Setup(repo => repo.GetByStringAsync(paysId)).ReturnsAsync(actionResult);
            mockRepository.Setup(repo => repo.DeleteAsync(pays)).ThrowsAsync(new Exception("Database error"));

            // Act
            var result = await controllerMoq.DeletePays(paysId);

            // Assert
            Assert.IsInstanceOfType(result, typeof(ObjectResult));
            var statusCodeResult = result as ObjectResult;
            Assert.IsNotNull(statusCodeResult);
            Assert.AreEqual(StatusCodes.Status500InternalServerError, statusCodeResult.StatusCode);
        }
    }
}