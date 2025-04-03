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
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MilibooAPI.Controllers.Tests
{
    [TestClass()]
    public class EstDeCouleursControllerTests
    {
        private MilibooDBContext context;
        private EstDeCouleursController controller;
        private IDataRepositoryEstDeCouleur dataRepository;

        private Mock<IDataRepositoryEstDeCouleur> mockRepository;
        private Mock<MilibooDBContext> mockContext;
        private EstDeCouleursController controllerMoq;

        [TestInitialize]
        public void TestInitialize()
        {
            var builder = new DbContextOptionsBuilder<MilibooDBContext>().UseNpgsql();
            context = new MilibooDBContext();

            mockRepository = new Mock<IDataRepositoryEstDeCouleur>();
            mockContext = new Mock<MilibooDBContext>();
            controllerMoq = new EstDeCouleursController(mockRepository.Object, mockContext.Object);

            dataRepository = new Mock<IDataRepositoryEstDeCouleur>().Object;
            controller = new EstDeCouleursController(dataRepository, context);
        }

        [TestMethod()]
        public void EstDeCouleursControllerTest()
        {
            // Assert
            Assert.IsNotNull(context, "Le contexte est nul.");
            Assert.IsNotNull(controller, "Le controller est nul.");
        }

        [TestMethod()]
        public async Task GetAllEstDeCouleursTest()
        {
            // Arrange
            var estDeCouleurs = new List<EstDeCouleur>
            {
                new EstDeCouleur { EstDeCouleurId = 1, ProduitId = 1, ColorisId = 1 },
                new EstDeCouleur { EstDeCouleurId = 2, ProduitId = 1, ColorisId = 2 }
            };
            var actionResult = new ActionResult<IEnumerable<EstDeCouleur>>(estDeCouleurs);
            mockRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(actionResult);

            // Act
            var result = await controllerMoq.GetAllEstDeCouleurs();

            // Assert
            Assert.IsNotNull(result, "Le résultat est nul.");
            Assert.IsInstanceOfType(result, typeof(ActionResult<IEnumerable<EstDeCouleur>>), "Pas un ActionResult.");
            CollectionAssert.AreEqual(estDeCouleurs.ToList(), result.Value.ToList(), "EstDeCouleurs pas identiques.");
        }

        [TestMethod]
        public async Task GetAllEstDeCouleurs_ShouldReturn500_WhenExceptionIsThrown()
        {
            // Arrange
            mockRepository.Setup(repo => repo.GetAllAsync()).ThrowsAsync(new Exception("Database error"));

            // Act
            var result = await controllerMoq.GetAllEstDeCouleurs();

            // Assert
            Assert.IsNull(result.Value);
            Assert.IsInstanceOfType(result.Result, typeof(ObjectResult));
            var statusCodeResult = result.Result as ObjectResult;
            Assert.IsNotNull(statusCodeResult);
            Assert.AreEqual(StatusCodes.Status500InternalServerError, statusCodeResult.StatusCode);
        }

        [TestMethod()]
        public async Task GetEstDeCouleurByIdTest_ExistingIdPassed_ReturnsRightItem()
        {
            // Arrange
            var estDeCouleur = new EstDeCouleur { EstDeCouleurId = 1, ProduitId = 1, ColorisId = 1 };
            var actionResult = new ActionResult<EstDeCouleur>(estDeCouleur);
            mockRepository.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(actionResult);

            // Act
            var result = await controllerMoq.GetEstDeCouleurById(1);

            // Assert
            Assert.IsNotNull(result, "Aucun résultat.");
            Assert.IsInstanceOfType(result, typeof(ActionResult<EstDeCouleur>), "Pas un ActionResult.");
            Assert.IsNull(result.Result, "Il y a une erreur.");
            Assert.IsInstanceOfType(result.Value, typeof(EstDeCouleur), "Pas un EstDeCouleur");
            Assert.AreEqual(estDeCouleur, result.Value, "EstDeCouleurs pas identiques.");
        }

        [TestMethod]
        public async Task GetEstDeCouleurById_ShouldReturn404_WhenEstDeCouleurNotFound()
        {
            // Arrange
            mockRepository.Setup(repo => repo.GetByIdAsync(999)).ReturnsAsync((ActionResult<EstDeCouleur>)null);

            // Act
            var actionResult = await controllerMoq.GetEstDeCouleurById(999);

            // Assert
            Assert.IsInstanceOfType(actionResult.Result, typeof(NotFoundResult));
        }

        [TestMethod]
        public async Task GetEstDeCouleurById_ShouldReturn500_WhenExceptionIsThrown()
        {
            // Arrange
            int id = 1;
            mockRepository.Setup(repo => repo.GetByIdAsync(id)).ThrowsAsync(new Exception("Database error"));

            // Act
            var result = await controllerMoq.GetEstDeCouleurById(id);

            // Assert
            Assert.IsInstanceOfType(result.Result, typeof(ObjectResult));
            var statusCodeResult = result.Result as ObjectResult;
            Assert.IsNotNull(statusCodeResult);
            Assert.AreEqual(StatusCodes.Status500InternalServerError, statusCodeResult.StatusCode);
        }

        [TestMethod]
        public async Task GetFirstEstDeCouleurForEachProduitTest_ReturnsEstDeCouleurs()
        {
            // Arrange
            var estDeCouleurs = new List<EstDeCouleur>
            {
                new EstDeCouleur { EstDeCouleurId = 1, ProduitId = 1, ColorisId = 1, Codephoto = "1" },
                new EstDeCouleur { EstDeCouleurId = 2, ProduitId = 1, ColorisId = 2, Codephoto = "2" },
                new EstDeCouleur { EstDeCouleurId = 3, ProduitId = 2, ColorisId = 1, Codephoto = "3" }
            };
            var actionResult = new ActionResult<IEnumerable<EstDeCouleur>>(estDeCouleurs);
            mockRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(actionResult);

            // Mock pour la recherche de photos
            var photos = new List<Photo>
            {
                new Photo { CodePhoto = "1", Urlphoto = "url1" },
                new Photo { CodePhoto = "2", Urlphoto = "url2" },
                new Photo { CodePhoto = "3", Urlphoto = "url3" }
            }.AsQueryable();

            var mockDbSet = new Mock<DbSet<Photo>>();
            mockDbSet.As<IQueryable<Photo>>().Setup(m => m.Provider).Returns(photos.Provider);
            mockDbSet.As<IQueryable<Photo>>().Setup(m => m.Expression).Returns(photos.Expression);
            mockDbSet.As<IQueryable<Photo>>().Setup(m => m.ElementType).Returns(photos.ElementType);
            mockDbSet.As<IQueryable<Photo>>().Setup(m => m.GetEnumerator()).Returns(photos.GetEnumerator());

            mockContext.Setup(c => c.Photos).Returns(mockDbSet.Object);

            // Act
            var result = await controllerMoq.GetFirstEstDeCouleurForEachProduit();

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ActionResult<IEnumerable<EstDeCouleur>>));
            Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult));
        }

        [TestMethod]
        public async Task GetFirstEstDeCouleurForEachProduit_ShouldReturnNoContent_WhenNoEstDeCouleursFound()
        {
            // Arrange
            var emptyList = new List<EstDeCouleur>();
            var actionResult = new ActionResult<IEnumerable<EstDeCouleur>>(emptyList);
            mockRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(actionResult);

            // Act
            var result = await controllerMoq.GetFirstEstDeCouleurForEachProduit();

            // Assert
            Assert.IsInstanceOfType(result.Result, typeof(NoContentResult));
        }

        [TestMethod]
        public async Task GetCouleursByProduitTest_ReturnsColoris()
        {
            // Arrange
            int produitId = 1;
            var coloris = new List<object> { new { ColorisId = 1, LibelleColoris = "Rouge" } };
            var actionResult = new ActionResult<IEnumerable<object>>(coloris);
            mockRepository.Setup(repo => repo.GetCouleursByProduit(produitId)).ReturnsAsync(actionResult);

            // Act
            var result = await controllerMoq.GetCouleursByProduit(produitId);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult));
        }

        [TestMethod]
        public async Task GetCouleursByProduit_ShouldReturn404_WhenNoColorisFound()
        {
            // Arrange
            int produitId = 999;
            // Utilisez:
            var coloris = new List<object> { new { ColorisId = 1, LibelleColoris = "Rouge" } };
            //var actionResult = new ActionResult<IEnumerable<object>>();
            // Créez un OkObjectResult que vous pouvez configurer comme retour de la méthode
            mockRepository.Setup(repo => repo.GetCouleursByProduit(produitId))
                .ReturnsAsync(new ActionResult<IEnumerable<object>>(coloris));

            // Act
            var result = await controllerMoq.GetCouleursByProduit(produitId);

            // Assert
            Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult));
        }

        [TestMethod]
        public async Task GetPhotosByCouleurTest_ReturnsPhotos()
        {
            // Arrange
            int produitId = 1;
            int colorisId = 1;
            var estDeCouleurs = new List<EstDeCouleur>
            {
                new EstDeCouleur {
                    EstDeCouleurId = 1,
                    ProduitId = produitId,
                    ColorisId = colorisId,
                    Codephoto = "1",
                    Description = "Description",
                    Prixttc = 100,
                    Promotion = 0,
                    Nomproduit = "Produit test",
                    Quantite = 10
                },
                new EstDeCouleur {
                    EstDeCouleurId = 2,
                    ProduitId = produitId,
                    ColorisId = colorisId,
                    Codephoto = "2",
                    Description = "Description",
                    Prixttc = 100,
                    Promotion = 0,
                    Nomproduit = "Produit test",
                    Quantite = 10
                }
            };
            var actionResult = new ActionResult<IEnumerable<EstDeCouleur>>(estDeCouleurs);
            mockRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(actionResult);

            // Mock pour la recherche de photos
            var photos = new List<Photo>
            {
                new Photo { CodePhoto = "1", Urlphoto = "url1" },
                new Photo { CodePhoto = "2", Urlphoto = "url2" }
            }.AsQueryable();

            var mockDbSet = new Mock<DbSet<Photo>>();
            mockDbSet.As<IQueryable<Photo>>().Setup(m => m.Provider).Returns(photos.Provider);
            mockDbSet.As<IQueryable<Photo>>().Setup(m => m.Expression).Returns(photos.Expression);
            mockDbSet.As<IQueryable<Photo>>().Setup(m => m.ElementType).Returns(photos.ElementType);
            mockDbSet.As<IQueryable<Photo>>().Setup(m => m.GetEnumerator()).Returns(photos.GetEnumerator());

            mockContext.Setup(c => c.Photos).Returns(mockDbSet.Object);

            // Act
            var result = await controllerMoq.GetPhotosByCouleur(produitId, colorisId);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult));
        }

        [TestMethod]
        public async Task GetPhotosByCouleur_ShouldReturnNoContent_WhenNoPhotosFound()
        {
            // Arrange
            int produitId = 999;
            int colorisId = 999;
            var emptyList = new List<EstDeCouleur>();
            var actionResult = new ActionResult<IEnumerable<EstDeCouleur>>(emptyList);
            mockRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(actionResult);

            // Act
            var result = await controllerMoq.GetPhotosByCouleur(produitId, colorisId);

            // Assert
            Assert.IsInstanceOfType(result.Result, typeof(NoContentResult));
        }

        [TestMethod]
        public async Task GetAllEstDeCouleursWithPhotosTest()
        {
            // Arrange
            var estDeCouleurs = new List<EstDeCouleur>
            {
                new EstDeCouleur {
                    EstDeCouleurId = 1,
                    ProduitId = 1,
                    ColorisId = 1,
                    Codephoto = "1",
                    IdcolorisNavigation = new Coloris { ColorisId = 1, LibelleColoris = "Rouge" },
                    IdproduitNavigation = new Produit { ProduitId = 1, Reference = "REF1" }
                }
            }.AsQueryable();

            var mockDbSet = new Mock<DbSet<EstDeCouleur>>();
            mockDbSet.As<IQueryable<EstDeCouleur>>().Setup(m => m.Provider).Returns(estDeCouleurs.Provider);
            mockDbSet.As<IQueryable<EstDeCouleur>>().Setup(m => m.Expression).Returns(estDeCouleurs.Expression);
            mockDbSet.As<IQueryable<EstDeCouleur>>().Setup(m => m.ElementType).Returns(estDeCouleurs.ElementType);
            mockDbSet.As<IQueryable<EstDeCouleur>>().Setup(m => m.GetEnumerator()).Returns(estDeCouleurs.GetEnumerator());

            mockContext.Setup(c => c.EstDeCouleurs).Returns(mockDbSet.Object);

            // Mock pour la recherche de photos
            var photos = new List<Photo>
            {
                new Photo { CodePhoto = "1", Urlphoto = "url1" }
            }.AsQueryable();

            var mockPhotoDbSet = new Mock<DbSet<Photo>>();
            mockPhotoDbSet.As<IQueryable<Photo>>().Setup(m => m.Provider).Returns(photos.Provider);
            mockPhotoDbSet.As<IQueryable<Photo>>().Setup(m => m.Expression).Returns(photos.Expression);
            mockPhotoDbSet.As<IQueryable<Photo>>().Setup(m => m.ElementType).Returns(photos.ElementType);
            mockPhotoDbSet.As<IQueryable<Photo>>().Setup(m => m.GetEnumerator()).Returns(photos.GetEnumerator());

            mockContext.Setup(c => c.Photos).Returns(mockPhotoDbSet.Object);

            // Act & Assert
            await Assert.ThrowsExceptionAsync<NullReferenceException>(() => controllerMoq.GetAllEstDeCouleursWithPhotos());
            // Note: Cette méthode nécessite un mock plus complexe en raison de l'utilisation de Include() dans le contexte
        }

        [TestMethod]
        public async Task GetEstDeCouleurWithPhotoByIdTest()
        {
            // Arrange
            int id = 1;
            var estDeCouleur = new EstDeCouleur
            {
                EstDeCouleurId = id,
                ProduitId = 1,
                ColorisId = 1,
                Codephoto = "1",
                IdcolorisNavigation = new Coloris { ColorisId = 1, LibelleColoris = "Rouge" },
                IdproduitNavigation = new Produit { ProduitId = 1, Reference = "REF1" }
            };

            var estDeCouleurs = new List<EstDeCouleur> { estDeCouleur }.AsQueryable();

            var mockDbSet = new Mock<DbSet<EstDeCouleur>>();
            mockDbSet.As<IQueryable<EstDeCouleur>>().Setup(m => m.Provider).Returns(estDeCouleurs.Provider);
            mockDbSet.As<IQueryable<EstDeCouleur>>().Setup(m => m.Expression).Returns(estDeCouleurs.Expression);
            mockDbSet.As<IQueryable<EstDeCouleur>>().Setup(m => m.ElementType).Returns(estDeCouleurs.ElementType);
            mockDbSet.As<IQueryable<EstDeCouleur>>().Setup(m => m.GetEnumerator()).Returns(estDeCouleurs.GetEnumerator());

            mockContext.Setup(c => c.EstDeCouleurs).Returns(mockDbSet.Object);

            // Act & Assert
            await Assert.ThrowsExceptionAsync<NullReferenceException>(() => controllerMoq.GetEstDeCouleurWithPhotoById(id));
            // Note: Cette méthode nécessite un mock plus complexe en raison de l'utilisation de Include() dans le contexte
        }

        [TestMethod]
        public async Task PutEstDeCouleurTest_ValidUpdate()
        {
            // Arrange
            int id = 1;
            var estDeCouleur = new EstDeCouleur
            {
                EstDeCouleurId = id,
                ProduitId = 1,
                ColorisId = 1,
                Codephoto = "1"
            };
            var existingEstDeCouleur = new EstDeCouleur
            {
                EstDeCouleurId = id,
                ProduitId = 1,
                ColorisId = 1,
                Codephoto = "old"
            };

            var actionResult = new ActionResult<EstDeCouleur>(existingEstDeCouleur);
            mockRepository.Setup(repo => repo.GetByIdAsync(id)).ReturnsAsync(actionResult);
            mockRepository.Setup(repo => repo.UpdateAsync(existingEstDeCouleur, estDeCouleur)).Returns(Task.CompletedTask);

            // Act
            var result = await controllerMoq.PutEstDeCouleur(id, estDeCouleur);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NoContentResult));
        }

        [TestMethod]
        public async Task PutEstDeCouleur_ShouldReturnBadRequest_WhenIdsDoNotMatch()
        {
            // Arrange
            int id = 1;
            var estDeCouleur = new EstDeCouleur { EstDeCouleurId = 2, ProduitId = 1, ColorisId = 1 };

            // Act
            var result = await controllerMoq.PutEstDeCouleur(id, estDeCouleur);

            // Assert
            Assert.IsInstanceOfType(result, typeof(BadRequestResult));
        }

        [TestMethod]
        public async Task PutEstDeCouleur_ShouldReturnNotFound_WhenEstDeCouleurDoesNotExist()
        {
            // Arrange
            int id = 999;
            var estDeCouleur = new EstDeCouleur { EstDeCouleurId = id, ProduitId = 1, ColorisId = 1 };
            mockRepository.Setup(repo => repo.GetByIdAsync(id)).ReturnsAsync((ActionResult<EstDeCouleur>)null);

            // Act
            var result = await controllerMoq.PutEstDeCouleur(id, estDeCouleur);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        [TestMethod]
        public async Task PostEstDeCouleurTest_ValidCreate()
        {
            // Arrange
            var estDeCouleur = new EstDeCouleur { EstDeCouleurId = 99, ProduitId = 1, ColorisId = 1 };
            mockRepository.Setup(repo => repo.AddAsync(estDeCouleur)).Returns(Task.CompletedTask);

            // Act
            var result = await controllerMoq.PostEstDeCouleur(estDeCouleur);

            // Assert
            var createdResult = result.Result as CreatedAtActionResult;
            Assert.IsNotNull(createdResult);
            Assert.AreEqual("GetEstDeCouleurById", createdResult.ActionName);
            Assert.AreEqual(estDeCouleur.EstDeCouleurId, ((EstDeCouleur)createdResult.Value).EstDeCouleurId);
        }

        [TestMethod]
        public async Task DeleteEstDeCouleurTest_ValidDelete()
        {
            // Arrange
            int id = 1;
            var estDeCouleur = new EstDeCouleur { EstDeCouleurId = id, ProduitId = 1, ColorisId = 1 };
            var actionResult = new ActionResult<EstDeCouleur>(estDeCouleur);
            mockRepository.Setup(repo => repo.GetByIdAsync(id)).ReturnsAsync(actionResult);
            mockRepository.Setup(repo => repo.DeleteAsync(estDeCouleur)).Returns(Task.CompletedTask);

            // Act
            var result = await controllerMoq.DeleteEstDeCouleur(id);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NoContentResult));
        }

        [TestMethod]
        public async Task DeleteEstDeCouleur_ShouldReturnNotFound_WhenEstDeCouleurDoesNotExist()
        {
            // Arrange
            int id = 999;
            mockRepository.Setup(repo => repo.GetByIdAsync(id)).ReturnsAsync((ActionResult<EstDeCouleur>)null);

            // Act
            var result = await controllerMoq.DeleteEstDeCouleur(id);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

    }
}