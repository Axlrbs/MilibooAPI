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
    public class PaniersControllerTests
    {
        private MilibooDBContext context;

        private PaniersController controller;
        private IDataRepository<Panier> dataRepository;

        private Mock<IDataRepository<Panier>> mockRepository;
        private PaniersController controllerMoq;

        [TestInitialize]
        public void TestInitialize()
        {
            var builder = new DbContextOptionsBuilder<MilibooDBContext>().UseNpgsql();
            context = new MilibooDBContext();

            dataRepository = new PanierManager(context);
            controller = new PaniersController(dataRepository);

            mockRepository = new Mock<IDataRepository<Panier>>();
            controllerMoq = new PaniersController(mockRepository.Object);
        }

        [TestMethod()]
        public void CategoriesControllerTest()
        {
            // Assert
            Assert.IsNotNull(context, "Le contexte est nul.");
            Assert.IsNotNull(controller, "Le controller est nul.");
        }

        [TestMethod()]
        public void GetAllPaniersTest()
        {
            // Arrange
            var lesPaniers = context.Paniers.ToList();

            // Act
            var result = controller.GetAllPaniers().Result;
            var listePaniers = result.Value.ToList();

            // Assert
            Assert.IsNotNull(result, "Le résultat est nul.");
            Assert.IsInstanceOfType(result, typeof(ActionResult<IEnumerable<Panier>>), "Pas un ActionResult.");
            CollectionAssert.AreEqual(lesPaniers, listePaniers, "Paniers pas identiques.");
        }
        [TestMethod]
        public async Task GetAllPaniers_ShouldReturn500_WhenExceptionIsThrown()
        {
            // Arrange
            mockRepository.Setup(repo => repo.GetAllAsync()).ThrowsAsync(new Exception("Database error"));

            // Act
            var result = await controllerMoq.GetAllPaniers();

            // Assert
            var statusCodeResult = result.Result as ObjectResult;
            Assert.IsNotNull(statusCodeResult);
            Assert.AreEqual(StatusCodes.Status500InternalServerError, statusCodeResult.StatusCode);
        }

        [TestMethod()]
        public void GetPanierByIDClient_ExistingIdPassed_ReturnsRightItem()
        {
            // Arrange
            var categorie1 = context.Paniers.Where(u => u.ClientId == 42).FirstOrDefault();

            // Act
            var result = controller.GetPanierByIdClient(42).Result;

            // Assert
            Assert.IsNotNull(result, "Aucun résultat.");
            Assert.IsInstanceOfType(result, typeof(ActionResult<Panier>), "Pas un ActionResult.");
            Assert.IsNull(result.Result, "Il y a une erreur.");
            Assert.IsInstanceOfType(result.Value, typeof(Panier), "Pas un Panier");
            Assert.AreEqual(categorie1, result.Value, "Paniers pas identiques.");
        }

        [TestMethod]
        public void GetPanierByIDClient_ShouldReturn404_WhenPanierNotFound()
        {
            // Arrange
            var mockRepository = new Mock<IDataRepository<Panier>>();
            var userController = new PaniersController(mockRepository.Object);

            // Act
            var actionResult = userController.GetPanierByIdClient(999).Result;

            // Assert
            Assert.IsInstanceOfType(actionResult.Result, typeof(NotFoundResult));
        }

        [TestMethod]
        public async Task GetPanierByIdClient_ShouldReturn500_WhenExceptionIsThrown()
        {
            // Arrange
            int id = 1;
            mockRepository.Setup(repo => repo.GetByIdAsync(id)).ThrowsAsync(new Exception("Database error"));

            // Act
            var result = await controllerMoq.GetPanierByIdClient(id);

            // Assert
            var statusCodeResult = result.Result as ObjectResult;
            Assert.IsNotNull(statusCodeResult);
            Assert.AreEqual(StatusCodes.Status500InternalServerError, statusCodeResult.StatusCode);
        }

        [TestMethod()]
        public async Task PutPanier_ValidUpdate_ReturnsNoContent()
        {
            // Arrange
            var mockContext = new Mock<MilibooDBContext>();
            var mockSet = new Mock<DbSet<Panier>>();
            var mockRepo = new Mock<IDataRepository<Panier>>();

            var panierExistant = new Panier()
            {
                PanierId = 1,
                ClientId = 42,
                Dateetheure = DateOnly.FromDateTime(DateTime.Now)
            };

            var panierMisAJour = new Panier()
            {
                PanierId = 1,
                ClientId = 99, // Changement du client
                Dateetheure = DateOnly.FromDateTime(DateTime.Now.AddDays(1))
            };

            var paniersData = new List<Panier> { panierExistant }.AsQueryable();

            // Configurer le mock du repository pour retourner le panier existant
            mockRepo.Setup(m => m.GetByIdAsync(It.IsAny<int>()))
                    .ReturnsAsync((int id) => paniersData.FirstOrDefault(p => p.PanierId == id));

            mockRepo.Setup(m => m.UpdateAsync(It.IsAny<Panier>(), It.IsAny<Panier>()))
                    .Returns(Task.CompletedTask);

            var controller = new PaniersController(mockRepo.Object);

            // Act
            var result = await controller.PutPanier(1, panierMisAJour);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NoContentResult), "N'est pas un NoContent");
            Assert.AreEqual(StatusCodes.Status204NoContent, ((NoContentResult)result).StatusCode, "N'est pas 204");
        }

        [TestMethod()]
        public async Task PutPanier_NonMatchingId_ReturnsBadRequest()
        {
            // Arrange
            var mockRepo = new Mock<IDataRepository<Panier>>();
            var controller = new PaniersController(mockRepo.Object);

            var panier = new Panier { PanierId = 2, ClientId = 1,Dateetheure = DateOnly.FromDateTime(DateTime.Now) }; // ID différent

            // Act
            var result = await controller.PutPanier(1, panier);

            // Assert
            Assert.IsInstanceOfType(result, typeof(BadRequestResult), "Le résultat n'est pas BadRequest");
        }

        [TestMethod()]
        public async Task PutPanier_NonExistingPanier_ReturnsNotFound()
        {
            // Arrange
            var mockRepo = new Mock<IDataRepository<Panier>>();
            int panierId = 1;

            // Simuler que le panier n'existe pas (retourner null pour simuler un panier introuvable)
            mockRepo.Setup(m => m.GetByIdAsync(panierId))
                    .ReturnsAsync((Panier)null); // Aucun panier trouvé, retourne null

            var controller = new PaniersController(mockRepo.Object);

            // Act
            var result = await controller.PutPanier(panierId, new Panier { PanierId = panierId });

            // Assert
            // On s'attend à un résultat NotFound si le panier n'est pas trouvé
            Assert.IsInstanceOfType(result, typeof(NotFoundResult), "Le résultat n'est pas NotFound");
        }

        [TestMethod()]
        public async Task PostPanier_ValidClient_ReturnsCreated()
        {
            // Arrange
            var mockRepo = new Mock<IDataRepository<Panier>>();
            int clientId = 42;

            mockRepo.Setup(m => m.AddAsync(It.IsAny<Panier>()))
                    .Returns(Task.CompletedTask)
                    .Callback<Panier>(p => p.PanierId = 1); // Simule l'affectation d'un ID après l'ajout

            var controller = new PaniersController(mockRepo.Object);

            // Act
            var result = await controller.PostPanier(clientId);

            // Assert
            var createdResult = result.Result as CreatedAtActionResult;
            Assert.IsNotNull(createdResult, "Le résultat n'est pas un CreatedAtActionResult");
            Assert.AreEqual(StatusCodes.Status201Created, createdResult.StatusCode, "Le statut n'est pas 201");
            Assert.IsInstanceOfType(createdResult.Value, typeof(Panier), "Le résultat ne contient pas un objet Panier");

            var panier = createdResult.Value as Panier;
            Assert.AreEqual(clientId, panier.ClientId, "Le ClientId du panier n'est pas correct");
        }

        [TestMethod()]
        public async Task DeletePanier_ExistingPanier_ReturnsNoContent()
        {
            // Arrange
            var mockRepo = new Mock<IDataRepository<Panier>>();
            int panierId = 1;

            var panier = new Panier { PanierId = panierId, ClientId = 42 };

            mockRepo.Setup(m => m.GetByIdAsync(panierId))
                    .ReturnsAsync(panier); // Simule un panier trouvé

            mockRepo.Setup(m => m.DeleteAsync(It.IsAny<Panier>()))
                    .Returns(Task.CompletedTask);

            var controller = new PaniersController(mockRepo.Object);

            // Act
            var result = await controller.DeletePanier(panierId);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NoContentResult), "Le résultat n'est pas NoContent");
        }

    }
}