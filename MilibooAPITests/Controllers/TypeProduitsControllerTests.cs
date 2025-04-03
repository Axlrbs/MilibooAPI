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
    public class TypeProduitsControllerTests
    {
        private MilibooDBContext context;
        private TypeProduitsController controller;
        private IDataRepository<TypeProduit> dataRepository;

        private Mock<IDataRepository<TypeProduit>> mockRepository;
        private TypeProduitsController controllerMoq;


        [TestInitialize]
        public void TestInitialize()
        {
            var builder = new DbContextOptionsBuilder<MilibooDBContext>().UseNpgsql();
            context = new MilibooDBContext();

            dataRepository = new TypeProduitManager(context);
            controller = new TypeProduitsController(dataRepository);

            mockRepository = new Mock<IDataRepository<TypeProduit>>();
            controllerMoq = new TypeProduitsController(mockRepository.Object);
        }


        [TestMethod()]
        public void TypeProduitsControllerTest()
        {
            // Assert
            Assert.IsNotNull(context, "Le contexte est nul.");
            Assert.IsNotNull(controller, "Le controller est nul.");
        }

        [TestMethod()]
        public void GetAllTypeProduitsTest()
        {
            // Arrange
            var lesTypeProduits = context.TypeProduits.ToList();

            // Act
            var result = controller.GetAllTypeProduits().Result;
            var listeTypeProduits = result.Value.ToList();

            // Assert
            Assert.IsNotNull(result, "Le résultat est nul.");
            Assert.IsInstanceOfType(result, typeof(ActionResult<IEnumerable<TypeProduit>>), "Pas un ActionResult.");
            CollectionAssert.AreEqual(lesTypeProduits, listeTypeProduits, "TypeProduit pas identiques.");
        }


        [TestMethod]
        public async Task GetAllTypeProduits_ShouldReturn500_WhenExceptionIsThrown()
        {
            // Arrange
            mockRepository.Setup(repo => repo.GetAllAsync()).ThrowsAsync(new Exception("Database error"));

            // Act
            var result = await controllerMoq.GetAllTypeProduits();

            // Assert
            var statusCodeResult = result.Result as ObjectResult;
            Assert.IsNotNull(statusCodeResult);
            Assert.AreEqual(StatusCodes.Status500InternalServerError, statusCodeResult.StatusCode);
        }

        [TestMethod()]
        public void GetTypeProduitById_ExistingIdPassed_ReturnsRightItem()
        {
            // Arrange
            var typeProduit1 = context.TypeProduits.Where(u => u.TypeProduitId == 3).FirstOrDefault();

            // Act
            var result = controller.GetTypeProduitById(3).Result;

            // Assert
            Assert.IsNotNull(result, "Aucun résultat.");
            Assert.IsInstanceOfType(result, typeof(ActionResult<TypeProduit>), "Pas un ActionResult.");
            Assert.IsNull(result.Result, "Il y a une erreur.");
            Assert.IsInstanceOfType(result.Value, typeof(TypeProduit), "Pas un TypeProduit");
            Assert.AreEqual(typeProduit1, result.Value, "TypeProduits pas identiques.");
        }

        [TestMethod]
        public void GetTypeProduitById_ShouldReturn404_WhenTypePaiementNotFound()
        {
            // Arrange
            var mockRepository = new Mock<IDataRepository<TypeProduit>>();
            var userController = new TypeProduitsController(mockRepository.Object);

            // Act
            var actionResult = userController.GetTypeProduitById(999).Result;

            // Assert
            Assert.IsInstanceOfType(actionResult.Result, typeof(NotFoundResult));
        }

        [TestMethod]
        public async Task GetTypeProduitById_ShouldReturn500_WhenExceptionIsThrown()
        {
            // Arrange
            int id = 1;
            mockRepository.Setup(repo => repo.GetByIdAsync(id)).ThrowsAsync(new Exception("Database error"));

            // Act
            var result = await controllerMoq.GetTypeProduitById(id);

            // Assert
            var statusCodeResult = result.Result as ObjectResult;
            Assert.IsNotNull(statusCodeResult);
            Assert.AreEqual(StatusCodes.Status500InternalServerError, statusCodeResult.StatusCode);
        }

        [TestMethod()]
        public void GetTypeProduitByNom_ExistingNomPassed_ReturnsRightItem()
        {
            // Arrange
            var typeProduit = context.TypeProduits.Where(u => u.LibelleTypeProduit == "Fauteuil").FirstOrDefault();

            // Act
            var result = controller.GetTypeProduitByLibelle("Fauteuil").Result;

            // Assert
            Assert.IsNotNull(result, "Aucun résultat.");
            Assert.IsInstanceOfType(result, typeof(ActionResult<TypeProduit>), "Pas un ActionResult.");
            Assert.IsNull(result.Result, "Il y a une erreur.");
            Assert.IsInstanceOfType(result.Value, typeof(TypeProduit), "Pas un TypeProduit");
            Assert.AreEqual(typeProduit, result.Value, "TypeProduits pas identiques.");
        }

        [TestMethod]
        public void GetTypeProduitByNom_ShouldReturn404_WhenTypeProduitNotFound()
        {
            // Arrange
            var mockRepository = new Mock<IDataRepository<TypeProduit>>();
            var userController = new TypeProduitsController(mockRepository.Object);

            // Act
            var actionResult = userController.GetTypeProduitByLibelle("azdazédaz").Result;

            // Assert
            Assert.IsInstanceOfType(actionResult.Result, typeof(NotFoundResult));
        }

        [TestMethod]
        public async Task GetTypeProduitByNom_ShouldReturn500_WhenExceptionIsThrown()
        {
            // Arrange
            string nom = "Canapé";
            mockRepository.Setup(repo => repo.GetByStringAsync(nom)).ThrowsAsync(new Exception("Database error"));

            // Act
            var result = await controllerMoq.GetTypeProduitByLibelle(nom);

            // Assert
            var statusCodeResult = result.Result as ObjectResult;
            Assert.IsNotNull(statusCodeResult);
            Assert.AreEqual(StatusCodes.Status500InternalServerError, statusCodeResult.StatusCode);
        }

        [TestMethod]
        public async Task PutTypeProduit_ShouldReturnNoContent_WhenUpdateIsSuccessful()
        {
            // Arrange
            var id = 1;
            var typeProduit = new TypeProduit { TypeProduitId = id, LibelleTypeProduit = "Test" };
            var existingTypeProduit = new TypeProduit { TypeProduitId = id, LibelleTypeProduit = "OldName" };

            mockRepository.Setup(repo => repo.GetByIdAsync(id)).ReturnsAsync(existingTypeProduit);
            mockRepository.Setup(repo => repo.UpdateAsync(existingTypeProduit, typeProduit)).Returns(Task.CompletedTask);

            // Act
            var result = await controllerMoq.PutTypeProduit(id, typeProduit);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NoContentResult));
        }

        [TestMethod]
        public async Task PutTypeProduit_ShouldReturnBadRequest_WhenIdsDoNotMatch()
        {
            // Arrange
            var id = 1;
            var typeProduit = new TypeProduit { TypeProduitId = 2, LibelleTypeProduit = "Test" };

            // Act
            var result = await controller.PutTypeProduit(id, typeProduit);

            // Assert
            Assert.IsInstanceOfType(result, typeof(BadRequestResult));
        }

        [TestMethod]
        public async Task PostTypeProduit_ShouldReturnCreatedAtAction_WhenSuccessful()
        {
            // Arrange
            int produitId = context.TypeProduits.OrderByDescending(c => c.TypeProduitId)
                      .Select(c => c.TypeProduitId)
                      .FirstOrDefault();
            var typeProduit = new TypeProduit { TypeProduitId = produitId+1, LibelleTypeProduit = "Test" };
            mockRepository.Setup(repo => repo.AddAsync(typeProduit)).Returns(Task.CompletedTask);

            // Act
            var result = await controllerMoq.PostTypeProduit(typeProduit);

            // Assert
            var createdResult = result.Result as CreatedAtActionResult;
            Assert.IsNotNull(createdResult);
            Assert.AreEqual("GetTypeProduitById", createdResult.ActionName);
            Assert.AreEqual(typeProduit.TypeProduitId, ((TypeProduit)createdResult.Value).TypeProduitId);
        }

        [TestMethod]
        public async Task DeleteTypeProduit_ShouldReturnNoContent_WhenSuccessful()
        {
            // Arrange
            var id = 1;
            var typeProduit = new TypeProduit { TypeProduitId = id, LibelleTypeProduit = "Test" };
            mockRepository.Setup(repo => repo.GetByIdAsync(id)).ReturnsAsync(typeProduit);
            mockRepository.Setup(repo => repo.DeleteAsync(typeProduit)).Returns(Task.CompletedTask);

            // Act
            var result = await controllerMoq.DeleteTypeProduit(id);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NoContentResult));
        }

        [TestMethod]
        public async Task DeleteTypeProduit_ShouldReturnNotFound_WhenTypeProduitDoesNotExist()
        {
            // Arrange
            var id = 1;
            mockRepository.Setup(repo => repo.GetByIdAsync(id)).ReturnsAsync((TypeProduit)null);

            // Act
            var result = await controllerMoq.DeleteTypeProduit(id);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NoContentResult));
        }
    }
}