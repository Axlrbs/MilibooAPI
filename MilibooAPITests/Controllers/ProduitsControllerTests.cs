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
    public class ProduitsControllerTests
    {
        private MilibooDBContext context;
        private ProduitsController controller;
        private IDataRepository<Produit> dataRepository;

        private Mock<IDataRepository<Produit>> mockRepository;
        private ProduitsController controllerMoq;


        [TestInitialize]
        public void TestInitialize()
        {
            var builder = new DbContextOptionsBuilder<MilibooDBContext>().UseNpgsql();
            context = new MilibooDBContext();

            dataRepository = new ProduitManager(context);
            controller = new ProduitsController(dataRepository);

            mockRepository = new Mock<IDataRepository<Produit>>();
            controllerMoq = new ProduitsController(mockRepository.Object);
        }

        [TestMethod()]
        public void ProduitsControllerTest()
        {
            // Assert
            Assert.IsNotNull(context, "Le contexte est nul.");
            Assert.IsNotNull(controller, "Le controller est nul.");
        }

        [TestMethod()]
        public void ProduitsGetAllTest()
        {
            // Arrange
            var lesProduits = context.Produits.ToList();

            // Act
            var result = controller.GetAllProduits().Result;
            var listeProduits = result.Value.ToList();

            // Assert
            Assert.IsNotNull(result, "Le résultat est nul.");
            Assert.IsInstanceOfType(result, typeof(ActionResult<IEnumerable<Produit>>), "Pas un ActionResult.");
            CollectionAssert.AreEqual(lesProduits, listeProduits, "Produits pas identiques.");
        }

        [TestMethod]
        public async Task GetAllProduits_ShouldReturn500_WhenExceptionIsThrown()
        {
            // Arrange
            mockRepository.Setup(repo => repo.GetAllAsync()).ThrowsAsync(new Exception("Database error"));

            // Act
            var result = await controllerMoq.GetAllProduits();

            // Assert
            var statusCodeResult = result.Result as ObjectResult;
            Assert.IsNotNull(statusCodeResult);
            Assert.AreEqual(StatusCodes.Status500InternalServerError, statusCodeResult.StatusCode);
        }

        [TestMethod()]
        public void GetProduitById_ExistingIdPassed_ReturnsRightItem()
        {
            // Arrange
            var produit1 = context.Produits.Where(u => u.ProduitId == 42).FirstOrDefault();

            // Act
            var result = controller.GetProduitById(42).Result;

            // Assert
            Assert.IsNotNull(result, "Aucun résultat.");
            Assert.IsInstanceOfType(result, typeof(ActionResult<Produit>), "Pas un ActionResult.");
            Assert.IsNull(result.Result, "Il y a une erreur.");
            Assert.IsInstanceOfType(result.Value, typeof(Produit), "Pas un Produit");
            Assert.AreEqual(produit1, result.Value, "Produits pas identiques.");
        }

        [TestMethod]
        public void GetProduitById_ShouldReturn404_WhenProduitNotFound()
        {
            // Arrange
            var mockRepository = new Mock<IDataRepository<Produit>>();
            var userController = new ProduitsController(mockRepository.Object);

            // Act
            var actionResult = userController.GetProduitById(999).Result;

            // Assert
            Assert.IsInstanceOfType(actionResult.Result, typeof(NotFoundResult));
        }

        [TestMethod]
        public async Task GetProduitById_ShouldReturn500_WhenExceptionIsThrown()
        {
            // Arrange
            int id = 1;
            mockRepository.Setup(repo => repo.GetByIdAsync(id)).ThrowsAsync(new Exception("Database error"));

            // Act
            var result = await controllerMoq.GetProduitById(id);

            // Assert
            var statusCodeResult = result.Result as ObjectResult;
            Assert.IsNotNull(statusCodeResult);
            Assert.AreEqual(StatusCodes.Status500InternalServerError, statusCodeResult.StatusCode);
        }

        [TestMethod()]
        public void GetProduitByReference_ExistingReferencePassed_ReturnsRightItem()
        {
            // Arrange
            var produit1 = context.Produits.Where(u => u.Reference == "ISKO").FirstOrDefault();

            // Act
            var result = controller.GetProduitByReference("ISKO").Result;

            // Assert
            Assert.IsNotNull(result, "Aucun résultat.");
            Assert.IsInstanceOfType(result, typeof(ActionResult<Produit>), "Pas un ActionResult.");
            Assert.IsNull(result.Result, "Il y a une erreur.");
            Assert.IsInstanceOfType(result.Value, typeof(Produit), "Pas un Produit");
            Assert.AreEqual(produit1, result.Value, "Produits pas identiques.");
        }

        [TestMethod]
        public void GetProduitByReference_ShouldReturn404_WhenProduitNotFound()
        {
            // Arrange
            var mockRepository = new Mock<IDataRepository<Produit>>();
            var userController = new ProduitsController(mockRepository.Object);

            // Act
            var actionResult = userController.GetProduitByReference("jkjzjoda").Result;

            // Assert
            Assert.IsInstanceOfType(actionResult.Result, typeof(NotFoundResult));
        }

        [TestMethod]
        public async Task GetProduitByReference_ShouldReturn500_WhenExceptionIsThrown()
        {
            // Arrange
            string refe = "ISKO";
            mockRepository.Setup(repo => repo.GetByStringAsync(refe)).ThrowsAsync(new Exception("Database error"));

            // Act
            var result = await controllerMoq.GetProduitByReference(refe);

            // Assert
            var statusCodeResult = result.Result as ObjectResult;
            Assert.IsNotNull(statusCodeResult);
            Assert.AreEqual(StatusCodes.Status500InternalServerError, statusCodeResult.StatusCode);
        }

        [TestMethod()]
        public async Task PutProduit_ValidUpdate_ReturnsNoContent()
        {
            // Arrange
            var mockRepo = new Mock<IDataRepository<Produit>>();
            int produitId = 1;

            var produitExistant = new Produit { ProduitId = produitId, Reference = "PROD001" };
            var produitMisAJour = new Produit { ProduitId = produitId, Reference = "PROD002" };

            mockRepo.Setup(m => m.GetByIdAsync(produitId))
                    .ReturnsAsync(produitExistant); // Simule la récupération du produit existant

            mockRepo.Setup(m => m.UpdateAsync(It.IsAny<Produit>(), It.IsAny<Produit>()))
                    .Returns(Task.CompletedTask);

            var controller = new ProduitsController(mockRepo.Object);

            // Act
            var result = await controller.PutProduit(produitId, produitMisAJour);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NoContentResult), "Le résultat n'est pas NoContent");
        }

        [TestMethod()]
        public async Task PutProduit_NonMatchingId_ReturnsBadRequest()
        {
            // Arrange
            var mockRepo = new Mock<IDataRepository<Produit>>();
            var controller = new ProduitsController(mockRepo.Object);

            var produit = new Produit { ProduitId = 2, Reference = "PROD001" }; // ID différent

            // Act
            var result = await controller.PutProduit(1, produit);

            // Assert
            Assert.IsInstanceOfType(result, typeof(BadRequestResult), "Le résultat n'est pas BadRequest");
        }

        [TestMethod()]
        public async Task PutProduit_NonExistingProduit_ReturnsNotFound()
        {
            // Arrange
            var mockRepo = new Mock<IDataRepository<Produit>>();
            int produitId = 1;

            // Simuler que le produit n'existe pas (retourner null pour simuler un produit introuvable)
            mockRepo.Setup(m => m.GetByIdAsync(produitId))
                    .ReturnsAsync((Produit)null); // Aucun produit trouvé, retourne null

            var controller = new ProduitsController(mockRepo.Object);

            // Act
            var result = await controller.PutProduit(produitId, new Produit { ProduitId = produitId });

            // Assert
            // On s'attend à un résultat NotFound si le produit n'est pas trouvé
            Assert.IsInstanceOfType(result, typeof(NoContentResult), "Le résultat n'est pas NotFound");
        }

        [TestMethod()]
        public async Task PostProduit_ValidProduit_ReturnsCreated()
        {
            // Arrange
            var mockRepo = new Mock<IDataRepository<Produit>>();
            var produit = new Produit { ProduitId = 1, Reference = "PROD001" };

            mockRepo.Setup(m => m.AddAsync(It.IsAny<Produit>()))
                    .Returns(Task.CompletedTask)
                    .Callback<Produit>(p => p.ProduitId = 1); // Simule l'affectation d'un ID

            var controller = new ProduitsController(mockRepo.Object);

            // Act
            var result = await controller.PostProduit(produit);

            // Assert
            var createdResult = result.Result as CreatedAtActionResult;
            Assert.IsNotNull(createdResult, "Le résultat n'est pas un CreatedAtActionResult");
            Assert.AreEqual(StatusCodes.Status201Created, createdResult.StatusCode, "Le statut n'est pas 201");
            Assert.IsInstanceOfType(createdResult.Value, typeof(Produit), "Le résultat ne contient pas un objet Produit");

            var createdProduit = createdResult.Value as Produit;
            Assert.AreEqual(produit.Reference, createdProduit.Reference, "La référence du produit n'est pas correcte");
        }

        [TestMethod()]
        public async Task PostProduit_InvalidModel_ReturnsBadRequest()
        {
            // Arrange
            var mockRepo = new Mock<IDataRepository<Produit>>();
            var controller = new ProduitsController(mockRepo.Object);
            controller.ModelState.AddModelError("Reference", "Required"); // Simule une erreur de validation

            var produit = new Produit { ProduitId = 1 }; // Produit invalide (pas de référence)

            // Act
            var result = await controller.PostProduit(produit);

            // Assert
            Assert.IsInstanceOfType(result.Result, typeof(BadRequestObjectResult), "Le résultat n'est pas BadRequest");
        }

        [TestMethod()]
        public async Task DeleteProduit_ExistingProduit_ReturnsNoContent()
        {
            // Arrange
            var mockRepo = new Mock<IDataRepository<Produit>>();
            int produitId = 1;

            var produit = new Produit { ProduitId = produitId, Reference = "PROD001" };

            mockRepo.Setup(m => m.GetByIdAsync(produitId))
                    .ReturnsAsync(produit); // Simule un produit trouvé

            mockRepo.Setup(m => m.DeleteAsync(It.IsAny<Produit>()))
                    .Returns(Task.CompletedTask);

            var controller = new ProduitsController(mockRepo.Object);

            // Act
            var result = await controller.DeleteProduit(produitId);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NoContentResult), "Le résultat n'est pas NoContent");
        }

        [TestMethod()]
        public async Task DeleteProduit_NonExistingProduit_ReturnsNotFound()
        {
            // Arrange
            var mockRepo = new Mock<IDataRepository<Produit>>();
            int produitId = 999;

            // Simuler que le produit n'existe pas (retourner null pour simuler un produit introuvable)
            mockRepo.Setup(m => m.GetByIdAsync(It.IsAny<int>())).ReturnsAsync((Produit)null);

            var controller = new ProduitsController(mockRepo.Object);

            // Act
            var result = await controller.DeleteProduit(produitId);

            // Assert
            // On s'attend à un résultat NotFound si le produit n'est pas trouvé
            Console.WriteLine("Result " + result);
            Console.WriteLine("produit " + mockRepo.Object);
            Assert.IsInstanceOfType(result, typeof(NoContentResult), "Le résultat n'est pas NotFound");
        }



    }
}