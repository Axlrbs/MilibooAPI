using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MilibooAPI.Controllers;
using MilibooAPI.Models;
using MilibooAPI.Models.DataManager;
using MilibooAPI.Models.EntityFramework;
using MilibooAPI.Models.Repository;
using Moq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MilibooAPI.Controllers.Tests
{
    [TestClass()]
    public class CategorieControllerTests
    {
        private MilibooDBContext context;

        private CategorieController controller;
        private IDataRepositoryCategorie dataRepository;

        private Mock<IDataRepositoryCategorie> mockRepository;
        private CategorieController controllerMoq;

        [TestInitialize]
        public void TestInitialize()
        {
            var builder = new DbContextOptionsBuilder<MilibooDBContext>().UseNpgsql();
            context = new MilibooDBContext();

            dataRepository = new CategorieManager(context);
            controller = new CategorieController(dataRepository);

            mockRepository = new Mock<IDataRepositoryCategorie>();
            controllerMoq = new CategorieController(mockRepository.Object);
        }

        [TestMethod()]
        public void CategoriesControllerTest()
        {
            // Assert
            Assert.IsNotNull(context, "Le contexte est nul.");
            Assert.IsNotNull(controller, "Le controller est nul.");
        }


        [TestMethod()]
        public void GetAllCategoriesTest()
        {
            // Arrange
            var lesUtilisateurs = context.Categories.ToList();

            // Act
            var result = controller.GetAllCategories().Result;
            var listeUtilisateurs = result.Value.ToList();

            // Assert
            Assert.IsNotNull(result, "Le résultat est nul.");
            Assert.IsInstanceOfType(result, typeof(ActionResult<IEnumerable<Categorie>>), "Pas un ActionResult.");
            CollectionAssert.AreEqual(lesUtilisateurs, listeUtilisateurs, "Categories pas identiques.");
        }

        [TestMethod]
        public async Task GetAllCategories_ShouldReturn500_WhenExceptionIsThrown()
        {
            // Arrange
            mockRepository.Setup(repo => repo.GetAllAsync()).ThrowsAsync(new Exception("Database error"));

            // Act
            var result = await controllerMoq.GetAllCategories();

            // Assert
            var statusCodeResult = result.Result as ObjectResult;
            Assert.IsNotNull(statusCodeResult);
            Assert.AreEqual(StatusCodes.Status500InternalServerError, statusCodeResult.StatusCode);
        }

        [TestMethod()]
        public void GetCategorieById_ExistingIdPassed_ReturnsRightItem()
        {
            // Arrange
            var categorie1 = context.Categories.Where(u => u.CategorieId == 1).FirstOrDefault();

            // Act
            var result = controller.GetCategorieById(1).Result;

            // Assert
            Assert.IsNotNull(result, "Aucun résultat.");
            Assert.IsInstanceOfType(result, typeof(ActionResult<Categorie>), "Pas un ActionResult.");
            Assert.IsNull(result.Result, "Il y a une erreur.");
            Assert.IsInstanceOfType(result.Value, typeof(Categorie), "Pas une Categorie");
            Assert.AreEqual(categorie1, result.Value, "Categories pas identiques.");
        }

        [TestMethod]
        public void GetCategorieById_ShouldReturn404_WhenCategoryNotFound()
        {
            // Arrange
            var mockRepository = new Mock<IDataRepositoryCategorie>();
            var userController = new CategorieController(mockRepository.Object);

            // Act
            var actionResult = userController.GetCategorieById(999).Result;

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
            var result = await controllerMoq.GetCategorieById(id);

            // Assert
            var statusCodeResult = result.Result as ObjectResult;
            Assert.IsNotNull(statusCodeResult);
            Assert.AreEqual(StatusCodes.Status500InternalServerError, statusCodeResult.StatusCode);
        }

        [TestMethod()]
        public async Task GetProduitsByIdCategorie_ExistingIdPassed_ReturnsRightItem()
        {
            // Arrange
            var id = 1;
            var expectedCategorie = await context.Categories
                                                 .Include(c => c.ACommes)
                                                 .ThenInclude(a => a.IdproduitNavigation)
                                                 .FirstOrDefaultAsync(c => c.CategorieId == id);
            Assert.IsNotNull(expectedCategorie, "La catégorie avec l'ID spécifié n'existe pas dans la base de données.");

            // Act
            var result = await controller.GetProduitsByIdCategorie(id);

            // Assert
            Assert.IsNotNull(result);

            var actualCategorie = result.Value as Categorie;
            Assert.IsNotNull(actualCategorie, "La catégorie retournée est null.");
            Assert.AreEqual(expectedCategorie.CategorieId, actualCategorie.CategorieId, "Les IDs de catégorie ne correspondent pas.");
        }

        [TestMethod]
        public void GetProduitsByIdCategorie_ShouldReturn404_WhenCategoryNotFound()
        {
            
            // Arrange
            var mockRepository = new Mock<IDataRepositoryCategorie>();
            var userController = new CategorieController(mockRepository.Object);

            // Act
            var actionResult = userController.GetProduitsByIdCategorie(999).Result;

            // Assert
            Assert.IsInstanceOfType(actionResult.Result, typeof(NotFoundResult));
        }
        

        [TestMethod]
        public async Task GetProduitsByIdCategorie_ShouldReturn500_WhenServerErrorOccurs()
        {
            // Arrange
            int id = 5;
            mockRepository.Setup(repo => repo.GetProduitsByIdCategorieAsync(id)).ThrowsAsync(new Exception("Erreur interne du serveur"));

            // Act
            var result = await controllerMoq.GetProduitsByIdCategorie(id);

            // Assert
            var statusCodeResult = result.Result as ObjectResult;
            Assert.IsNotNull(statusCodeResult);
            Assert.AreEqual(StatusCodes.Status500InternalServerError, statusCodeResult.StatusCode);
        }

        [TestMethod()]
        public async Task GetFirstPhotoByCode_ExistingIdPassed_ReturnsRightItem()
        {
            // Arrange
            var codePhoto = "2"; // Code de photo que tu veux tester
            var expectedPhoto = await context.Photos
                                              .FirstOrDefaultAsync(p => p.CodePhoto == codePhoto);

            // Si la photo n'existe pas dans la base de données, échoue immédiatement
            Assert.IsNotNull(expectedPhoto, "Aucune photo trouvée avec ce code dans la base de données.");

            // Act
            var result = await controller.GetFirstPhotoByCode(codePhoto);

            // Assert
            Assert.IsNotNull(result);  // Vérifie que le résultat n'est pas null
            var actualPhoto = result.Value as Photo;  // Récupère la photo depuis le résultat
            Assert.IsNotNull(actualPhoto, "La photo retournée est null.");  // Vérifie que la photo n'est pas null
            Assert.AreEqual(expectedPhoto.CodePhoto, actualPhoto.CodePhoto, "Les codes de photo ne correspondent pas.");
        }

        [TestMethod]
        public void GetFirstPhotoByCode_ShouldReturn404_WhenCodePhotoNotFound()
        {
            // Arrange
            var mockRepository = new Mock<IDataRepositoryCategorie>();
            var userController = new CategorieController(mockRepository.Object);

            // Act
            var actionResult = userController.GetFirstPhotoByCode("5465").Result;

            // Assert
            Assert.IsInstanceOfType(actionResult.Result, typeof(NotFoundResult));
        }

        [TestMethod]
        public async Task GetFirstPhotoByCode_ShouldReturn500_WhenServerErrorOccurs()
        {
            // Arrange
            string codePhoto = "5";
            mockRepository.Setup(repo => repo.GetFirstPhotoByCodeAsync(codePhoto)).ThrowsAsync(new Exception("Erreur interne du serveur"));

            // Act
            var result = await controllerMoq.GetFirstPhotoByCode(codePhoto);

            // Assert
            var statusCodeResult = result.Result as ObjectResult;
            Assert.IsNotNull(statusCodeResult);
            Assert.AreEqual(StatusCodes.Status500InternalServerError, statusCodeResult.StatusCode);
        }

        [TestMethod()]
        public void GetCategorieByNom_ExistingNomPassed_ReturnsRightItem()
        {
            // Arrange
            var categorie1 = context.Categories.Where(u => u.NomCategorie == "Canapé").FirstOrDefault();

            // Act
            var result = controller.GetCategorieByNom("Canapé").Result;

            // Assert
            Assert.IsNotNull(result, "Aucun résultat.");
            Assert.IsInstanceOfType(result, typeof(ActionResult<Categorie>), "Pas un ActionResult.");
            Assert.IsNull(result.Result, "Il y a une erreur.");
            Assert.IsInstanceOfType(result.Value, typeof(Categorie), "Pas une Categorie");
            Assert.AreEqual(categorie1, result.Value, "Categories pas identiques.");
        }

        [TestMethod]
        public void GetCategorieByNom_ShouldReturn404_WhenCategoryNotFound()
        {
            // Arrange
            var mockRepository = new Mock<IDataRepositoryCategorie>();
            var userController = new CategorieController(mockRepository.Object);

            // Act
            var actionResult = userController.GetFirstPhotoByCode("5465").Result;

            // Assert
            Assert.IsInstanceOfType(actionResult.Result, typeof(NotFoundResult));
        }

        [TestMethod]
        public async Task GetCategorieByNom_ShouldReturn500_WhenServerErrorOccurs()
        {
            // Arrange
            string nom = "Canapé";
            mockRepository.Setup(repo => repo.GetByStringAsync(nom)).ThrowsAsync(new Exception("Erreur interne du serveur"));

            // Act
            var result = await controllerMoq.GetCategorieByNom(nom);

            // Assert
            var statusCodeResult = result.Result as ObjectResult;
            Assert.IsNotNull(statusCodeResult);
            Assert.AreEqual(StatusCodes.Status500InternalServerError, statusCodeResult.StatusCode);
        }
    }
}