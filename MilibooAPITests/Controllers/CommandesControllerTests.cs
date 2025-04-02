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
    public class CommandesControllerTests
    {
        private MilibooDBContext context;

        private CommandesController controller;
        private IDataRepositoryCommande dataRepository;

        private Mock<IDataRepositoryCommande> mockRepository;
        private CommandesController controllerMoq;

        [TestInitialize]
        public void TestInitialize()
        {
            var builder = new DbContextOptionsBuilder<MilibooDBContext>().UseNpgsql();
            context = new MilibooDBContext();

            dataRepository = new CommandeManager(context);
            controller = new CommandesController(dataRepository);

            mockRepository = new Mock<IDataRepositoryCommande>();
            controllerMoq = new CommandesController(mockRepository.Object);
        }

        [TestMethod()]
        public void CategoriesControllerTest()
        {
            // Assert
            Assert.IsNotNull(context, "Le contexte est nul.");
            Assert.IsNotNull(controller, "Le controller est nul.");
        }

        [TestMethod()]
        public void GetAllCommandesTest()
        {
            // Arrange
            var lesCommandes = context.Commandes.ToList();

            // Act
            var result = controller.GetAllCommandes().Result;
            var listeCommandes = result.Value.ToList();

            // Assert
            Assert.IsNotNull(result, "Le résultat est nul.");
            Assert.IsInstanceOfType(result, typeof(ActionResult<IEnumerable<Commande>>), "Pas un ActionResult.");
            CollectionAssert.AreEqual(lesCommandes, listeCommandes, "Commandes pas identiques.");
        }

        [TestMethod]
        public async Task GetAllCommandes_ShouldReturn500_WhenExceptionIsThrown()
        {
            // Arrange
            mockRepository.Setup(repo => repo.GetAllAsync()).ThrowsAsync(new Exception("Database error"));

            // Act
            var result = await controllerMoq.GetAllCommandes();

            // Assert
            var statusCodeResult = result.Result as ObjectResult;
            Assert.IsNotNull(statusCodeResult);
            Assert.AreEqual(StatusCodes.Status500InternalServerError, statusCodeResult.StatusCode);
        }

        [TestMethod()]
        public void GetCommandeById_ExistingIdPassed_ReturnsRightItem()
        {
            // Arrange
            var commande1 = context.Commandes.Where(u => u.CommandeId == 98).FirstOrDefault();

            // Act
            var result = controller.GetCommandeById(98).Result;

            // Assert
            Assert.IsNotNull(result, "Aucun résultat.");
            Assert.IsInstanceOfType(result, typeof(ActionResult<Commande>), "Pas un ActionResult.");
            Assert.IsNull(result.Result, "Il y a une erreur.");
            Assert.IsInstanceOfType(result.Value, typeof(Commande), $"{result.Value} Pas une Commande");
            Assert.AreEqual(commande1, result.Value, "Commandes pas identiques.");
        }

        [TestMethod]
        public void GetCommandeById_ShouldReturn404_WhenCommandeNotFound()
        {
            // Arrange
            var mockRepository = new Mock<IDataRepositoryCommande>();
            var userController = new CommandesController(mockRepository.Object);

            // Act
            var actionResult = userController.GetCommandeById(999).Result;

            // Assert
            Assert.IsInstanceOfType(actionResult.Result, typeof(NotFoundResult));
        }

        [TestMethod]
        public async Task GetCategorieById_ShouldReturn500_WhenExceptionIsThrown()
        {
            // Arrange
            int id = 1;
            mockRepository.Setup(repo => repo.GetByIdAsync(id)).ThrowsAsync(new Exception("Database error"));

            // Act
            var result = await controllerMoq.GetCommandeById(id);

            // Assert
            var statusCodeResult = result.Result as ObjectResult;
            Assert.IsNotNull(statusCodeResult);
            Assert.AreEqual(StatusCodes.Status500InternalServerError, statusCodeResult.StatusCode);
        }

        [TestMethod()]
        public void GetCommandeByIdClient_ExistingIdPassed_ReturnsRightItem()
        {
            // Arrange
            var commande1 = context.Commandes.Where(u => u.ClientId == 9).FirstOrDefault();

            // Act
            var result = controller.GetCommandeByIdClient(9).Result;

            // Assert
            Assert.IsNotNull(result, "Aucun résultat.");
            Assert.IsInstanceOfType(result, typeof(ActionResult<Commande>), "Pas un ActionResult.");
            Assert.IsNull(result.Result, "Il y a une erreur.");
            Assert.IsInstanceOfType(result.Value, typeof(Commande), $"{result.Value} Pas une Commande");
            Assert.AreEqual(commande1, result.Value, "Commandes pas identiques.");
        }

        [TestMethod]
        public void GetCommandeByIdClient_ShouldReturn404_WhenCommandeNotFound()
        {
            // Arrange
            var mockRepository = new Mock<IDataRepositoryCommande>();
            var userController = new CommandesController(mockRepository.Object);

            // Act
            var actionResult = userController.GetCommandeByIdClient(999).Result;

            // Assert
            Assert.IsInstanceOfType(actionResult.Result, typeof(NotFoundResult));
        }

        [TestMethod]
        public async Task GetCategorieByIdClient_ShouldReturn500_WhenExceptionIsThrown()
        {
            // Arrange
            int id = 1;
            mockRepository.Setup(repo => repo.GetByIdClientAsync(id)).ThrowsAsync(new Exception("Database error"));

            // Act
            var result = await controllerMoq.GetCommandeByIdClient(id);

            // Assert
            var statusCodeResult = result.Result as ObjectResult;
            Assert.IsNotNull(statusCodeResult);
            Assert.AreEqual(StatusCodes.Status500InternalServerError, statusCodeResult.StatusCode);
        }

        [TestMethod()]
        public void GetCommandeByStatut_ExistingStatutPassed_ReturnsRightItem()
        {
            // Arrange
            var commande1 = context.Commandes.Where(u => u.Statut == "accepté").FirstOrDefault();

            // Act
            var result = controller.GetCommandeByStatus("accepté").Result;

            // Assert
            Assert.IsNotNull(result, "Aucun résultat.");
            Assert.IsInstanceOfType(result, typeof(ActionResult<Commande>), "Pas un ActionResult.");
            Assert.IsNull(result.Result, "Il y a une erreur.");
            Assert.IsInstanceOfType(result.Value, typeof(Commande), "Pas une Commande");
            Assert.AreEqual(commande1, result.Value, "Commandes pas identiques.");
        }

        [TestMethod]
        public void GetCommandeByStatut_ShouldReturn404_WhenCommandeNotFound()
        {
            // Arrange
            var mockRepository = new Mock<IDataRepositoryCommande>();
            var userController = new CommandesController(mockRepository.Object);

            // Act
            var actionResult = userController.GetCommandeByStatus("alalallaa").Result;

            // Assert
            Assert.IsInstanceOfType(actionResult.Result, typeof(NotFoundResult));
        }

        [TestMethod]
        public async Task GetCommandeByStatut_ShouldReturn500_WhenExceptionIsThrown()
        {
            // Arrange
            string statut = "accepté";
            mockRepository.Setup(repo => repo.GetByStringAsync(statut)).ThrowsAsync(new Exception("Database error"));

            // Act
            var result = await controllerMoq.GetCommandeByStatus(statut);

            // Assert
            var statusCodeResult = result.Result as ObjectResult;
            Assert.IsNotNull(statusCodeResult);
            Assert.AreEqual(StatusCodes.Status500InternalServerError, statusCodeResult.StatusCode);
        }

        [TestMethod()]
        public void PutCommande_ValidUpdate_ReturnsNoContent()
        {
            // Arrange
            Random rnd = new Random();
            int chiffre = rnd.Next(1, 100);

            Commande commandeATester = new Commande()
            {
                CommandeId = 150,
                PanierId = 152,
                ClientId = 54,
                BoutiqueId = 1,
                CarteBancaireId = 20,
                MontantCommande = (decimal)374.995,
                Statut = "Expédiée " + chiffre + " fois"
            };

            // Act
            var result = controller.PutCommande(150, commandeATester).Result;

            // Assert
            var commande1 = context.Commandes.Where(u => u.CommandeId == 150).FirstOrDefault();
            Assert.IsInstanceOfType(result, typeof(NoContentResult), "N'est pas un NoContent");
            Assert.AreEqual(((NoContentResult)result).StatusCode, StatusCodes.Status204NoContent, "N'est pas 204");
            Assert.AreNotEqual(commande1, commandeATester, "La commande n'a pas été modifiée !");
        }

        [TestMethod()]
        public void PutCommande_ValidUpdate_ReturnsNoContent_Moq()
        {
            // Arrange
            Random rnd = new Random();
            int chiffre = rnd.Next(1, 1000000000);

            Commande commandeAvant = new Commande()
            {
                CommandeId = 105,
                PanierId = 107,
                ClientId = 54,
                LivraisonId = 27,
                VirementId = 1,
                MontantCommande = (decimal)3899.97,
                Statut = null
            };

            Commande commandeApres = new Commande()
            {
                CommandeId = 105,
                PanierId = 107,
                ClientId = 54,
                LivraisonId = 27,
                VirementId = 1,
                MontantCommande = (decimal)3899.97,
                Statut = "En cours de préparation" // statut modifié
            };
            mockRepository.Setup(x => x.GetByIdAsync(105).Result).Returns(commandeAvant);

            // Act
            var result = controllerMoq.PutCommande(105, commandeApres).Result;

            // Assert
            Assert.IsInstanceOfType(result, typeof(NoContentResult), "N'est pas un NoContent");
            Assert.AreEqual(((NoContentResult)result).StatusCode, StatusCodes.Status204NoContent, "N'est pas 204");

            mockRepository.Verify(x => x.UpdateAsync(It.IsAny<Commande>(), commandeApres), Times.Once, "La mise à jour n'a pas été effectuée !");
        }
    }
}