using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MilibooAPI.Controllers;
using MilibooAPI.Models.DataManager;
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
    public class TypePaiementsControllerTests
    {
        private MilibooDBContext context;
        private TypePaiementsController controller;
        private IDataRepository<TypePaiement> dataRepository;

        private Mock<IDataRepository<TypePaiement>> mockRepository;
        private TypePaiementsController controllerMoq;


        [TestInitialize]
        public void TestInitialize()
        {
            var builder = new DbContextOptionsBuilder<MilibooDBContext>().UseNpgsql();
            context = new MilibooDBContext();

            dataRepository = new TypePaiementManager(context);
            controller = new TypePaiementsController(dataRepository);

            mockRepository = new Mock<IDataRepository<TypePaiement>>();
            controllerMoq = new TypePaiementsController(mockRepository.Object);
        }


        [TestMethod()]
        public void TypePaiementsControllerTest()
        {
            // Assert
            Assert.IsNotNull(context, "Le contexte est nul.");
            Assert.IsNotNull(controller, "Le controller est nul.");
        }

        [TestMethod()]
        public void GetAllTypePaiementsTest()
        {
            // Arrange
            var lesTypePaiements = context.TypePaiements.ToList();

            // Act
            var result = controller.GetAllTypePaiements().Result;
            var listeTypePaiments = result.Value.ToList();

            // Assert
            Assert.IsNotNull(result, "Le résultat est nul.");
            Assert.IsInstanceOfType(result, typeof(ActionResult<IEnumerable<TypePaiement>>), "Pas un ActionResult.");
            CollectionAssert.AreEqual(lesTypePaiements, listeTypePaiments, "TypePaiement pas identiques.");
        }

        [TestMethod]
        public async Task GetAllTypePaiements_ShouldReturn500_WhenExceptionIsThrown()
        {
            // Arrange
            mockRepository.Setup(repo => repo.GetAllAsync()).ThrowsAsync(new Exception("Database error"));

            // Act
            var result = await controllerMoq.GetAllTypePaiements();

            // Assert
            var statusCodeResult = result.Result as ObjectResult;
            Assert.IsNotNull(statusCodeResult);
            Assert.AreEqual(StatusCodes.Status500InternalServerError, statusCodeResult.StatusCode);
        }

        [TestMethod()]
        public void GetTypePaiementById_ExistingIdPassed_ReturnsRightItem()
        {
            // Arrange
            var typePaiement1 = context.TypePaiements.Where(u => u.TypePaiementId == 3).FirstOrDefault();

            // Act
            var result = controller.GetTypePaiementById(3).Result;

            // Assert
            Assert.IsNotNull(result, "Aucun résultat.");
            Assert.IsInstanceOfType(result, typeof(ActionResult<TypePaiement>), "Pas un ActionResult.");
            Assert.IsNull(result.Result, "Il y a une erreur.");
            Assert.IsInstanceOfType(result.Value, typeof(TypePaiement), "Pas un TypePaiement");
            Assert.AreEqual(typePaiement1, result.Value, "TypePaiements pas identiques.");
        }

        [TestMethod]
        public void GetTypePaiementById_ShouldReturn404_WhenTypePaiementNotFound()
        {
            // Arrange
            var mockRepository = new Mock<IDataRepository<TypePaiement>>();
            var userController = new TypePaiementsController(mockRepository.Object);

            // Act
            var actionResult = userController.GetTypePaiementById(999).Result;

            // Assert
            Assert.IsInstanceOfType(actionResult.Result, typeof(NotFoundResult));
        }

        [TestMethod]
        public async Task GetTypePaiementById_ShouldReturn500_WhenExceptionIsThrown()
        {
            // Arrange
            int id = 1;
            mockRepository.Setup(repo => repo.GetByIdAsync(id)).ThrowsAsync(new Exception("Database error"));

            // Act
            var result = await controllerMoq.GetTypePaiementById(id);

            // Assert
            var statusCodeResult = result.Result as ObjectResult;
            Assert.IsNotNull(statusCodeResult);
            Assert.AreEqual(StatusCodes.Status500InternalServerError, statusCodeResult.StatusCode);
        }

        [TestMethod()]
        public void GetTypePaiementByNom_ExistingNomPassed_ReturnsRightItem()
        {
            // Arrange
            var typePaiement = context.TypePaiements.Where(u => u.Libelletypepaiement == "PayPal").FirstOrDefault();

            // Act
            var result = controller.GetTypePaiementByLibelle("Paypal").Result;

            // Assert
            Assert.IsNotNull(result, "Aucun résultat.");
            Assert.IsInstanceOfType(result, typeof(ActionResult<TypePaiement>), "Pas un ActionResult.");
            Assert.IsNull(result.Result, "Il y a une erreur.");
            Assert.IsInstanceOfType(result.Value, typeof(TypePaiement), "Pas un TypePaiement");
            Assert.AreEqual(typePaiement, result.Value, "TypePaiements pas identiques.");
        }

        [TestMethod]
        public void GetTypePaiementByNom_ShouldReturn404_WhenTypePaiementNotFound()
        {
            // Arrange
            var mockRepository = new Mock<IDataRepository<TypePaiement>>();
            var userController = new TypePaiementsController(mockRepository.Object);

            // Act
            var actionResult = userController.GetTypePaiementByLibelle("azdazédaz").Result;

            // Assert
            Assert.IsInstanceOfType(actionResult.Result, typeof(NotFoundResult));
        }

        [TestMethod]
        public async Task GetTypePaiementByNom_ShouldReturn500_WhenExceptionIsThrown()
        {
            // Arrange
            string nom = "PayPal";
            mockRepository.Setup(repo => repo.GetByStringAsync(nom)).ThrowsAsync(new Exception("Database error"));

            // Act
            var result = await controllerMoq.GetTypePaiementByLibelle(nom);

            // Assert
            var statusCodeResult = result.Result as ObjectResult;
            Assert.IsNotNull(statusCodeResult);
            Assert.AreEqual(StatusCodes.Status500InternalServerError, statusCodeResult.StatusCode);
        }

        [TestMethod]
        public async Task PutTypePaiement_ShouldReturnNoContent_WhenUpdateIsSuccessful()
        {
            // Arrange
            var id = 1;
            var typePaiement = new TypePaiement { TypePaiementId = id, Libelletypepaiement = "Test" };
            var existingTypePaiement = new TypePaiement { TypePaiementId = id, Libelletypepaiement = "OldName" };

            mockRepository.Setup(repo => repo.GetByIdAsync(id)).ReturnsAsync(existingTypePaiement);
            mockRepository.Setup(repo => repo.UpdateAsync(existingTypePaiement, typePaiement)).Returns(Task.CompletedTask);

            // Act
            var result = await controller.PutTypePaiement(id, typePaiement);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NoContentResult));
        }

        [TestMethod]
        public async Task PutTypePaiement_ShouldReturnBadRequest_WhenIdsDoNotMatch()
        {
            // Arrange
            var id = 1;
            var typePaiement = new TypePaiement { TypePaiementId = 2, Libelletypepaiement = "Test" };

            // Act
            var result = await controller.PutTypePaiement(id, typePaiement);

            // Assert
            Assert.IsInstanceOfType(result, typeof(BadRequestResult));
        }

        [TestMethod]
        public async Task PostTypePaiement_ShouldReturnCreatedAtAction_WhenSuccessful()
        {
            // Arrange
            int produitId = context.TypePaiements.OrderByDescending(c => c.TypePaiementId)
                      .Select(c => c.TypePaiementId)
                      .FirstOrDefault();
            var typePaiement = new TypePaiement { TypePaiementId = produitId + 1, Libelletypepaiement = "Test" };
            mockRepository.Setup(repo => repo.AddAsync(typePaiement)).Returns(Task.CompletedTask);

            // Act
            var result = await controller.PostTypePaiement(typePaiement);

            // Assert
            var createdResult = result.Result as CreatedAtActionResult;
            Assert.IsNotNull(createdResult);
            Assert.AreEqual("GetTypePaiementById", createdResult.ActionName);
            Assert.AreEqual(typePaiement.TypePaiementId, ((TypePaiement)createdResult.Value).TypePaiementId);
        }

        [TestMethod]
        public async Task DeleteTypePaiement_ShouldReturnNoContent_WhenSuccessful()
        {
            // Arrange
            var id = 1;
            var typePaiement = new TypePaiement { TypePaiementId = id, Libelletypepaiement = "Test" };
            mockRepository.Setup(repo => repo.GetByIdAsync(id)).ReturnsAsync(typePaiement);
            mockRepository.Setup(repo => repo.DeleteAsync(typePaiement)).Returns(Task.CompletedTask);

            // Act
            var result = await controller.DeleteTypePaiement(id);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NoContentResult));
        }

        [TestMethod]
        public async Task DeleteTypePaiement_ShouldReturnNotFound_WhenTypePaiementDoesNotExist()
        {
            // Arrange
            var id = 1;
            mockRepository.Setup(repo => repo.GetByIdAsync(id)).ReturnsAsync((TypePaiement)null);

            // Act
            var result = await controller.DeleteTypePaiement(id);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }
    }
}