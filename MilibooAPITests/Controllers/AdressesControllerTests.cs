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
    public class AdressesControllerTests
    {
        private MilibooDBContext context;
        private AdressesController controller;
        private IDataRepository<Adresse> dataRepository;

        private Mock<IDataRepository<Adresse>> mockRepository;
        private AdressesController controllerMoq;

        [TestInitialize]
        public void TestInitialize()
        {
            var builder = new DbContextOptionsBuilder<MilibooDBContext>().UseNpgsql();
            context = new MilibooDBContext();

            mockRepository = new Mock<IDataRepository<Adresse>>();
            controllerMoq = new AdressesController(mockRepository.Object);

            dataRepository = new Mock<IDataRepository<Adresse>>().Object;
            controller = new AdressesController(dataRepository);
        }

        [TestMethod()]
        public void AdressesControllerTest()
        {
            // Assert
            Assert.IsNotNull(controller, "Le controller est nul.");
        }

        [TestMethod()]
        public async Task GetAllAdressesTest()
        {
            // Arrange
            var adresses = new List<Adresse>
            {
                new Adresse { AdresseId = 1, Rue = "123 Rue de Paris", CodePostal = 75001, NumeroInsee = "75001", PaysId = "FR" },
                new Adresse { AdresseId = 2, Rue = "456 Avenue des Champs", CodePostal = 75008, NumeroInsee = "75008", PaysId = "FR" }
            };
            var actionResult = new ActionResult<IEnumerable<Adresse>>(adresses);
            mockRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(actionResult);

            // Act
            var result = await controllerMoq.GetAllAdresses();

            // Assert
            Assert.IsNotNull(result, "Le résultat est nul.");
            Assert.IsInstanceOfType(result, typeof(ActionResult<IEnumerable<Adresse>>), "Pas un ActionResult.");
            CollectionAssert.AreEqual(adresses.ToList(), result.Value.ToList(), "Adresses pas identiques.");
        }

        [TestMethod]
        public async Task GetAllAdresses_ShouldReturn500_WhenExceptionIsThrown()
        {
            // Arrange
            mockRepository.Setup(repo => repo.GetAllAsync()).ThrowsAsync(new Exception("Database error"));

            // Act
            var result = await controllerMoq.GetAllAdresses();

            // Assert
            Assert.IsNull(result.Value);
            Assert.IsInstanceOfType(result.Result, typeof(ObjectResult));
            var statusCodeResult = result.Result as ObjectResult;
            Assert.IsNotNull(statusCodeResult);
            Assert.AreEqual(StatusCodes.Status500InternalServerError, statusCodeResult.StatusCode);
        }

        [TestMethod()]
        public async Task GetAdresseByIdTest_ExistingIdPassed_ReturnsRightItem()
        {
            // Arrange
            var adresse = new Adresse { AdresseId = 1, Rue = "123 Rue de Paris", CodePostal = 75001, NumeroInsee = "75001", PaysId = "FR" };
            var actionResult = new ActionResult<Adresse>(adresse);
            mockRepository.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(actionResult);

            // Act
            var result = await controllerMoq.GetAdresseById(1);

            // Assert
            Assert.IsNotNull(result, "Aucun résultat.");
            Assert.IsInstanceOfType(result, typeof(ActionResult<Adresse>), "Pas un ActionResult.");
            Assert.IsNull(result.Result, "Il y a une erreur.");
            Assert.IsInstanceOfType(result.Value, typeof(Adresse), "Pas une Adresse");
            Assert.AreEqual(adresse, result.Value, "Adresses pas identiques.");
        }

        [TestMethod]
        public async Task GetAdresseById_ShouldReturn404_WhenAdresseNotFound()
        {
            // Arrange
            Adresse adresseNull = new Adresse();
            adresseNull = null;
            var actionResult = new ActionResult<Adresse>(adresseNull);
            mockRepository.Setup(repo => repo.GetByIdAsync(999)).ReturnsAsync(actionResult);

            // Act
            var result = await controllerMoq.GetAdresseById(999);

            // Assert
            Assert.IsInstanceOfType(result.Result, typeof(NotFoundResult));
        }

        [TestMethod]
        public async Task GetAdresseById_ShouldReturn500_WhenExceptionIsThrown()
        {
            // Arrange
            int id = 1;
            mockRepository.Setup(repo => repo.GetByIdAsync(id)).ThrowsAsync(new Exception("Database error"));

            // Act
            var result = await controllerMoq.GetAdresseById(id);

            // Assert
            Assert.IsInstanceOfType(result.Result, typeof(ObjectResult));
            var statusCodeResult = result.Result as ObjectResult;
            Assert.IsNotNull(statusCodeResult);
            Assert.AreEqual(StatusCodes.Status500InternalServerError, statusCodeResult.StatusCode);
        }

        [TestMethod()]
        public async Task GetAdresseByRueTest_ExistingRuePassed_ReturnsRightItem()
        {
            // Arrange
            string rue = "123 Rue de Paris";
            var adresse = new Adresse { AdresseId = 1, Rue = rue, CodePostal = 75001, NumeroInsee = "75001", PaysId = "FR" };
            var actionResult = new ActionResult<Adresse>(adresse);
            mockRepository.Setup(repo => repo.GetByStringAsync(rue)).ReturnsAsync(actionResult);

            // Act
            var result = await controllerMoq.GetAdresseByRue(rue);

            // Assert
            Assert.IsNotNull(result, "Aucun résultat.");
            Assert.IsInstanceOfType(result, typeof(ActionResult<Adresse>), "Pas un ActionResult.");
            Assert.IsNull(result.Result, "Il y a une erreur.");
            Assert.IsInstanceOfType(result.Value, typeof(Adresse), "Pas une Adresse");
            Assert.AreEqual(adresse, result.Value, "Adresses pas identiques.");
        }

        [TestMethod]
        public async Task GetAdresseByRue_ShouldReturn404_WhenAdresseNotFound()
        {
            // Arrange
            string rue = "Rue inexistante";
            Adresse adresseNull = new Adresse();
            adresseNull = null;
            var actionResult = new ActionResult<Adresse>(adresseNull);
            mockRepository.Setup(repo => repo.GetByStringAsync(rue)).ReturnsAsync(actionResult);

            // Act
            var result = await controllerMoq.GetAdresseByRue(rue);

            // Assert
            Assert.IsInstanceOfType(result.Result, typeof(NotFoundResult));
        }

        [TestMethod]
        public async Task GetAdresseByRue_ShouldReturn500_WhenExceptionIsThrown()
        {
            // Arrange
            string rue = "123 Rue de Paris";
            mockRepository.Setup(repo => repo.GetByStringAsync(rue)).ThrowsAsync(new Exception("Database error"));

            // Act
            var result = await controllerMoq.GetAdresseByRue(rue);

            // Assert
            Assert.IsInstanceOfType(result.Result, typeof(ObjectResult));
            var statusCodeResult = result.Result as ObjectResult;
            Assert.IsNotNull(statusCodeResult);
            Assert.AreEqual(StatusCodes.Status500InternalServerError, statusCodeResult.StatusCode);
        }

        [TestMethod]
        public async Task PostAdresseTest_ValidCreate()
        {
            // Arrange
            var adresse = new Adresse { AdresseId = 1, Rue = "123 Rue de Paris", CodePostal = 75001, NumeroInsee = "75001", PaysId = "FR" };
            mockRepository.Setup(repo => repo.AddAsync(adresse)).Returns(Task.CompletedTask);

            // Act
            var result = await controllerMoq.PostAdresse(adresse);

            // Assert
            var createdResult = result.Result as CreatedAtActionResult;
            Assert.IsNotNull(createdResult);
            Assert.AreEqual("GetAdresseById", createdResult.ActionName);
            Assert.AreEqual(adresse.AdresseId, ((Adresse)createdResult.Value).AdresseId);
        }

        [TestMethod]
        public async Task PostAdresse_ShouldReturnBadRequest_WhenModelStateIsInvalid()
        {
            // Arrange
            var adresse = new Adresse { AdresseId = 1, Rue = "123 Rue de Paris", PaysId = "FR" };
            controllerMoq.ModelState.AddModelError("CodePostal", "Required");

            // Act
            var result = await controllerMoq.PostAdresse(adresse);

            // Assert
            Assert.IsInstanceOfType(result.Result, typeof(BadRequestObjectResult));
        }

        [TestMethod]
        public async Task PostAdresse_ShouldReturn500_WhenExceptionIsThrown()
        {
            // Arrange
            var adresse = new Adresse { AdresseId = 1, Rue = "123 Rue de Paris", CodePostal = 75001, NumeroInsee = "75001", PaysId = "FR" };
            mockRepository.Setup(repo => repo.AddAsync(adresse)).ThrowsAsync(new Exception("Database error"));

            // Act
            var result = await controllerMoq.PostAdresse(adresse);

            // Assert
            Assert.IsInstanceOfType(result.Result, typeof(ObjectResult));
            var statusCodeResult = result.Result as ObjectResult;
            Assert.IsNotNull(statusCodeResult);
            Assert.AreEqual(StatusCodes.Status500InternalServerError, statusCodeResult.StatusCode);
        }

        [TestMethod]
        public async Task PutAdresseTest_ValidUpdate()
        {
            // Arrange
            int id = 1;
            var adresse = new Adresse { AdresseId = id, Rue = "123 Rue de Paris modifiée", CodePostal = 75001, NumeroInsee = "75001", PaysId = "FR" };
            var existingAdresse = new Adresse { AdresseId = id, Rue = "123 Rue de Paris", CodePostal = 75001, NumeroInsee = "75001", PaysId = "FR" };

            var actionResult = new ActionResult<Adresse>(existingAdresse);
            mockRepository.Setup(repo => repo.GetByIdAsync(id)).ReturnsAsync(actionResult);
            mockRepository.Setup(repo => repo.UpdateAsync(existingAdresse, adresse)).Returns(Task.CompletedTask);

            // Act
            var result = await controllerMoq.PutAdresse(id, adresse);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NoContentResult));
        }

        [TestMethod]
        public async Task PutAdresse_ShouldReturnBadRequest_WhenIdsDoNotMatch()
        {
            // Arrange
            int id = 1;
            var adresse = new Adresse { AdresseId = 2, Rue = "123 Rue de Paris", CodePostal = 75001, NumeroInsee = "75001", PaysId = "FR" };

            // Act
            var result = await controllerMoq.PutAdresse(id, adresse);

            // Assert
            Assert.IsInstanceOfType(result, typeof(BadRequestResult));
        }

        [TestMethod]
        public async Task PutAdresse_ShouldReturnBadRequest_WhenModelStateIsInvalid()
        {
            // Arrange
            int id = 1;
            var adresse = new Adresse { AdresseId = id, Rue = "123 Rue de Paris", PaysId = "FR" };
            controllerMoq.ModelState.AddModelError("CodePostal", "Required");

            // Act
            var result = await controllerMoq.PutAdresse(id, adresse);

            // Assert
            Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult));
        }

        [TestMethod]
        public async Task PutAdresse_ShouldReturnNotFound_WhenAdresseDoesNotExist()
        {
            // Arrange
            int id = 999;
            var adresse = new Adresse { AdresseId = id, Rue = "123 Rue de Paris", CodePostal = 75001, NumeroInsee = "75001", PaysId = "FR" };
            Adresse adresseNull = new Adresse();
            adresseNull = null;
            var actionResult = new ActionResult<Adresse>(adresseNull);
            mockRepository.Setup(repo => repo.GetByIdAsync(id)).ReturnsAsync(actionResult);

            // Act
            var result = await controllerMoq.PutAdresse(id, adresse);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        [TestMethod]
        public async Task PutAdresse_ShouldReturn500_WhenExceptionIsThrown()
        {
            // Arrange
            int id = 1;
            var adresse = new Adresse { AdresseId = id, Rue = "123 Rue de Paris", CodePostal = 75001, NumeroInsee = "75001", PaysId = "FR" };
            mockRepository.Setup(repo => repo.GetByIdAsync(id)).ThrowsAsync(new Exception("Database error"));

            // Act
            var result = await controllerMoq.PutAdresse(id, adresse);

            // Assert
            Assert.IsInstanceOfType(result, typeof(ObjectResult));
            var statusCodeResult = result as ObjectResult;
            Assert.IsNotNull(statusCodeResult);
            Assert.AreEqual(StatusCodes.Status500InternalServerError, statusCodeResult.StatusCode);
        }

        [TestMethod]
        public async Task DeleteAdresseTest_ValidDelete()
        {
            // Arrange
            int id = 1;
            var adresse = new Adresse { AdresseId = id, Rue = "123 Rue de Paris", CodePostal = 75001, NumeroInsee = "75001", PaysId = "FR" };
            var actionResult = new ActionResult<Adresse>(adresse);
            mockRepository.Setup(repo => repo.GetByIdAsync(id)).ReturnsAsync(actionResult);
            mockRepository.Setup(repo => repo.DeleteAsync(adresse)).Returns(Task.CompletedTask);

            // Act
            var result = await controllerMoq.DeleteAdresse(id);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NoContentResult));
        }

        [TestMethod]
        public async Task DeleteAdresse_ShouldReturnNotFound_WhenAdresseDoesNotExist()
        {
            // Arrange
            int id = 999;
            Adresse adresseNull = new Adresse();
            adresseNull = null;
            var actionResult = new ActionResult<Adresse>(adresseNull);
            mockRepository.Setup(repo => repo.GetByIdAsync(id)).ReturnsAsync(actionResult);

            // Act
            var result = await controllerMoq.DeleteAdresse(id);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        [TestMethod]
        public async Task DeleteAdresse_ShouldReturn500_WhenExceptionIsThrown()
        {
            // Arrange
            int id = 1;
            mockRepository.Setup(repo => repo.GetByIdAsync(id)).ThrowsAsync(new Exception("Database error"));

            // Act
            var result = await controllerMoq.DeleteAdresse(id);

            // Assert
            Assert.IsInstanceOfType(result, typeof(ObjectResult));
            var statusCodeResult = result as ObjectResult;
            Assert.IsNotNull(statusCodeResult);
            Assert.AreEqual(StatusCodes.Status500InternalServerError, statusCodeResult.StatusCode);
        }
    }
}