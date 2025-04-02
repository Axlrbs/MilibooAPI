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
    public class ProfessionnelsControllerTests
    {
        private MilibooDBContext context;
        private ProfessionnelsController controller;
        private IDataRepository<Professionnel> dataRepository;

        private Mock<IDataRepository<Professionnel>> mockRepository;
        private ProfessionnelsController controllerMoq;


        [TestInitialize]
        public void TestInitialize()
        {
            var builder = new DbContextOptionsBuilder<MilibooDBContext>().UseNpgsql();
            context = new MilibooDBContext();

            dataRepository = new ProfessionnelManager(context);
            controller = new ProfessionnelsController(dataRepository);

            mockRepository = new Mock<IDataRepository<Professionnel>>();
            controllerMoq = new ProfessionnelsController(mockRepository.Object);
        }


        [TestMethod()]
        public void ProfessionnelsControllerTest()
        {
            // Assert
            Assert.IsNotNull(context, "Le contexte est nul.");
            Assert.IsNotNull(controller, "Le controller est nul.");
        }

        [TestMethod()]
        public void GetAllProfessionnelsTest()
        {
            // Arrange
            var lesProfessionnels = context.Professionnels.ToList();

            // Act
            var result = controller.GetAllProfessionnels().Result;
            var listeProfessionnels = result.Value.ToList();

            // Assert
            Assert.IsNotNull(result, "Le résultat est nul.");
            Assert.IsInstanceOfType(result, typeof(ActionResult<IEnumerable<Professionnel>>), "Pas un ActionResult.");
            CollectionAssert.AreEqual(lesProfessionnels, listeProfessionnels, "Produits pas identiques.");
        }

        [TestMethod]
        public async Task GetAllProfessionnel_ShouldReturn500_WhenExceptionIsThrown()
        {
            // Arrange
            mockRepository.Setup(repo => repo.GetAllAsync()).ThrowsAsync(new Exception("Database error"));

            // Act
            var result = await controllerMoq.GetAllProfessionnels();

            // Assert
            var statusCodeResult = result.Result as ObjectResult;
            Assert.IsNotNull(statusCodeResult);
            Assert.AreEqual(StatusCodes.Status500InternalServerError, statusCodeResult.StatusCode);
        }

        [TestMethod()]
        public void GetProfessionnelById_ExistingIdPassed_ReturnsRightItem()
        {
            // Arrange
            var professionnel1 = context.Professionnels.Where(u => u.ProfessionnelId == 43).FirstOrDefault();

            // Act
            var result = controller.GetProfessionnelById(43).Result;

            // Assert
            Assert.IsNotNull(result, "Aucun résultat.");
            Assert.IsInstanceOfType(result, typeof(ActionResult<Professionnel>), "Pas un ActionResult.");
            Assert.IsNull(result.Result, "Il y a une erreur.");
            Assert.IsInstanceOfType(result.Value, typeof(Professionnel), "Pas un Professionnel");
            Assert.AreEqual(professionnel1, result.Value, "Professionnels pas identiques.");
        }

        [TestMethod]
        public void GetProfessionnelById_ShouldReturn404_WhenProfessionnelNotFound()
        {
            // Arrange
            var mockRepository = new Mock<IDataRepository<Professionnel>>();
            var userController = new ProfessionnelsController(mockRepository.Object);

            // Act
            var actionResult = userController.GetProfessionnelById(999).Result;

            // Assert
            Assert.IsInstanceOfType(actionResult.Result, typeof(NotFoundResult));
        }

        [TestMethod]
        public async Task GetProfessionnelById_ShouldReturn500_WhenExceptionIsThrown()
        {
            // Arrange
            int id = 1;
            mockRepository.Setup(repo => repo.GetByIdAsync(id)).ThrowsAsync(new Exception("Database error"));

            // Act
            var result = await controllerMoq.GetProfessionnelById(id);

            // Assert
            var statusCodeResult = result.Result as ObjectResult;
            Assert.IsNotNull(statusCodeResult);
            Assert.AreEqual(StatusCodes.Status500InternalServerError, statusCodeResult.StatusCode);
        }

        [TestMethod()]
        public void GetProfessionnelByNom_ExistingNomPassed_ReturnsRightItem()
        {
            // Arrange
            var professionnel = context.Professionnels.Where(u => u.Nompersonne == "Tran").FirstOrDefault();

            // Act
            var result = controller.GetProfessionnelByNom("Tran").Result;

            // Assert
            Assert.IsNotNull(result, "Aucun résultat.");
            Assert.IsInstanceOfType(result, typeof(ActionResult<Professionnel>), "Pas un ActionResult.");
            Assert.IsNull(result.Result, "Il y a une erreur.");
            Assert.IsInstanceOfType(result.Value, typeof(Professionnel), "Pas un Professionnel");
            Assert.AreEqual(professionnel, result.Value, "Professionnels pas identiques.");
        }

        [TestMethod]
        public void GetProfessionnelByNom_ShouldReturn404_WhenProfessionnelNotFound()
        {
            // Arrange
            var mockRepository = new Mock<IDataRepository<Professionnel>>();
            var userController = new ProfessionnelsController(mockRepository.Object);

            // Act
            var actionResult = userController.GetProfessionnelByNom("zldmald").Result;

            // Assert
            Assert.IsInstanceOfType(actionResult.Result, typeof(NotFoundResult));
        }

        [TestMethod]
        public async Task GetProfessionnelByNom_ShouldReturn500_WhenExceptionIsThrown()
        {
            // Arrange
            string nom = "Tran";
            mockRepository.Setup(repo => repo.GetByStringAsync(nom)).ThrowsAsync(new Exception("Database error"));

            // Act
            var result = await controllerMoq.GetProfessionnelByNom(nom);

            // Assert
            var statusCodeResult = result.Result as ObjectResult;
            Assert.IsNotNull(statusCodeResult);
            Assert.AreEqual(StatusCodes.Status500InternalServerError, statusCodeResult.StatusCode);
        }

        [TestMethod]
        public async Task PutProfessionnel_ShouldReturnNoContent_WhenUpdateIsSuccessful()
        {
            // Arrange
            var id = 1;
            var professionnel = new Professionnel { ProfessionnelId = id, Nompersonne = "Test" };
            var existingProfessionnel = new Professionnel { ProfessionnelId = id, Nompersonne = "OldName" };

            mockRepository.Setup(repo => repo.GetByIdAsync(id)).ReturnsAsync(existingProfessionnel);
            mockRepository.Setup(repo => repo.UpdateAsync(existingProfessionnel, professionnel)).Returns(Task.CompletedTask);

            // Act
            var result = await controller.PutProfessionnel(id, professionnel);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NoContentResult));
        }

        [TestMethod]
        public async Task PutProfessionnel_ShouldReturnBadRequest_WhenIdsDoNotMatch()
        {
            // Arrange
            var id = 1;
            var professionnel = new Professionnel { ProfessionnelId = 2, Nompersonne = "Test" };

            // Act
            var result = await controller.PutProfessionnel(id, professionnel);

            // Assert
            Assert.IsInstanceOfType(result, typeof(BadRequestResult));
        }

        [TestMethod]
        public async Task PostProfessionnel_ShouldReturnCreatedAtAction_WhenSuccessful()
        {
            // Arrange
            var professionnel = new Professionnel { ProfessionnelId = 1, Nompersonne = "Test" };
            mockRepository.Setup(repo => repo.AddAsync(professionnel)).Returns(Task.CompletedTask);

            // Act
            var result = await controller.PostProfessionnel(professionnel);

            // Assert
            var createdResult = result.Result as CreatedAtActionResult;
            Assert.IsNotNull(createdResult);
            Assert.AreEqual("GetProfessionnelById", createdResult.ActionName);
            Assert.AreEqual(professionnel.ProfessionnelId, ((Professionnel)createdResult.Value).ProfessionnelId);
        }

        [TestMethod]
        public async Task DeleteProfessionnel_ShouldReturnNoContent_WhenSuccessful()
        {
            // Arrange
            var id = 1;
            var professionnel = new Professionnel { ProfessionnelId = id, Nompersonne = "Test" };
            mockRepository.Setup(repo => repo.GetByIdAsync(id)).ReturnsAsync(professionnel);
            mockRepository.Setup(repo => repo.DeleteAsync(professionnel)).Returns(Task.CompletedTask);

            // Act
            var result = await controller.DeleteProfessionnel(id);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NoContentResult));
        }

        [TestMethod]
        public async Task DeleteProfessionnel_ShouldReturnNotFound_WhenProfessionnelDoesNotExist()
        {
            // Arrange
            var id = 1;
            mockRepository.Setup(repo => repo.GetByIdAsync(id)).ReturnsAsync((Professionnel)null);

            // Act
            var result = await controller.DeleteProfessionnel(id);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }
    }
}