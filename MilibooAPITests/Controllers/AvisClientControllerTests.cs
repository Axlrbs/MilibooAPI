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
using MilibooAPI.Models.EntityFramework;
using MilibooAPI.Models.Repository;
using Microsoft.EntityFrameworkCore;
using Moq;
using Microsoft.AspNetCore.Mvc;
using MilibooAPI.Models.DataManager;
using MilibooAPI.Models;
using System.Text.Json;
using Microsoft.AspNetCore.Http;

namespace MilibooAPI.Controllers.Tests
{
    [TestClass()]
    public class AvisClientControllerTests
    {
        private Mock<IDataRepositoryAvisClient> _mockRepo;
        private AvisClientController _controller;
        private List<AvisClient> _avisClients;
        private List<AvisClientDto> _avisClientDtos;

        [TestInitialize]
        public void Setup()
        {
            _mockRepo = new Mock<IDataRepositoryAvisClient>();
            _controller = new AvisClientController(_mockRepo.Object);

            // Préparation des données de test
            _avisClients = new List<AvisClient>
            {
                new AvisClient
                {
                    AvisId = 1,
                    ClientId = 10,
                    ProduitId = 100,
                    TitreAvis = "Super produit",
                    DescriptionAvis = "Très satisfait de mon achat",
                    NoteAvis = 5,
                    DateAvis = DateTime.Parse("2023-01-15")
                },
                new AvisClient
                {
                    AvisId = 2,
                    ClientId = 11,
                    ProduitId = 100,
                    TitreAvis = "Bon rapport qualité/prix",
                    DescriptionAvis = "Produit conforme à mes attentes",
                    NoteAvis = 4,
                    DateAvis = DateTime.Parse("2023-02-20")
                },
                new AvisClient
                {
                    AvisId = 3,
                    ClientId = 12,
                    ProduitId = 101,
                    TitreAvis = "Déçu",
                    DescriptionAvis = "Qualité inférieure à ce que j'attendais",
                    NoteAvis = 2,
                    DateAvis = DateTime.Parse("2023-03-10")
                }
            };

            _avisClientDtos = new List<AvisClientDto>
            {
                new AvisClientDto
                {
                    ClientId = 10,
                    ProduitId = 100,
                    TitreAvis = "Super produit",
                    DescriptionAvis = "Très satisfait de mon achat",
                    NoteAvis = 5
                },
                new AvisClientDto
                {
                    ClientId = 11,
                    ProduitId = 100,
                    TitreAvis = "Bon rapport qualité/prix",
                    DescriptionAvis = "Produit conforme à mes attentes",
                    NoteAvis = 4
                }
            };
        }

        [TestMethod]
        public async Task GetAllAvisClients_ReturnsAllAvisClients()
        {
            // Arrange
            _mockRepo.Setup(repo => repo.GetAllAsync())
                .ReturnsAsync(_avisClients);

            // Act
            var result = await _controller.GetAllAvisClients();

            // Assert
            Assert.IsInstanceOfType(result, typeof(ActionResult<IEnumerable<AvisClient>>));
            var actionResult = result as ActionResult<IEnumerable<AvisClient>>;
            Assert.IsInstanceOfType(actionResult.Value, typeof(IEnumerable<AvisClient>));
            Assert.AreEqual(3, (actionResult.Value as IEnumerable<AvisClient>).Count());
        }

        [TestMethod]
        public async Task GetAllAvisClientByProduitId_WithValidId_ReturnsAvisClients()
        {
            // Arrange
            int produitId = 100;
            var filteredAvis = _avisClients.Where(a => a.ProduitId == produitId).ToList();
            var avisClientDtos = new List<AvisClientDto>(); // Simulez les DTOs correspondants

            _mockRepo.Setup(repo => repo.GetAllByProduitIdAsync(produitId))
                .ReturnsAsync(new ActionResult<IEnumerable<AvisClientDto>>(avisClientDtos));

            // Act
            var result = await _controller.GetAllAvisClientByProduitId(produitId);

            // Assert
            Assert.IsInstanceOfType(result, typeof(ActionResult<IEnumerable<AvisClientDto>>));
            var actionResult = result as ActionResult<IEnumerable<AvisClientDto>>;
            Assert.IsInstanceOfType(actionResult.Value, typeof(IEnumerable<AvisClientDto>));
        }

        [TestMethod]
        public async Task GetAllAvisClientByProduitId_WithInvalidId_ReturnsNotFound()
        {
            // Arrange
            int produitId = 999;
            _mockRepo.Setup(repo => repo.GetAllByProduitIdAsync(produitId))
                .ReturnsAsync(new ActionResult<IEnumerable<AvisClientDto>>(new NotFoundResult()));

            // Act
            var result = await _controller.GetAllAvisClientByProduitId(produitId);

            // Assert
            Assert.IsInstanceOfType(result.Result, typeof(NotFoundObjectResult));
        }

        [TestMethod]
        public async Task GetAvisClientById_WithValidId_ReturnsAvisClient()
        {
            // Arrange
            int avisId = 1;
            var avisClient = _avisClients.FirstOrDefault(a => a.AvisId == avisId);

            _mockRepo.Setup(repo => repo.GetByIdAsync(avisId))
                .ReturnsAsync(new ActionResult<AvisClient>(avisClient));

            // Act
            var result = await _controller.GetAvisClientById(avisId);

            // Assert
            Assert.IsInstanceOfType(result, typeof(ActionResult<AvisClient>));
            var actionResult = result as ActionResult<AvisClient>;
            Assert.IsInstanceOfType(actionResult.Value, typeof(AvisClient));
            Assert.AreEqual(avisId, (actionResult.Value as AvisClient).AvisId);
        }

        [TestMethod]
        public async Task GetAvisClientById_WithInvalidId_ReturnsNotFound()
        {
            // Arrange
            int avisId = 999;
            _mockRepo.Setup(repo => repo.GetByIdAsync(avisId))
                .ReturnsAsync(new ActionResult<AvisClient>(new NotFoundResult()));

            // Act
            var result = await _controller.GetAvisClientById(avisId);

            // Assert
            Assert.IsInstanceOfType(result.Result, typeof(NotFoundObjectResult));
        }

        [TestMethod]
        public async Task PutAvisClient_WithValidIdAndModel_ReturnsNoContent()
        {
            // Arrange
            int avisId = 1;
            var avisClient = _avisClients.FirstOrDefault(a => a.AvisId == avisId);

            _mockRepo.Setup(repo => repo.GetByIdAsync(avisId))
                .ReturnsAsync(new ActionResult<AvisClient>(avisClient));

            _mockRepo.Setup(repo => repo.UpdateAsync(It.IsAny<AvisClient>(), It.IsAny<AvisClient>()))
                .Returns(Task.CompletedTask);

            // Act
            var result = await _controller.PutAvisClient(avisId, avisClient);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NoContentResult));
        }

        [TestMethod]
        public async Task PutAvisClient_WithInvalidId_ReturnsBadRequest()
        {
            // Arrange
            int avisId = 1;
            var avisClient = _avisClients.FirstOrDefault(a => a.AvisId == 2); // ID différent

            // Act
            var result = await _controller.PutAvisClient(avisId, avisClient);

            // Assert
            Assert.IsInstanceOfType(result, typeof(BadRequestResult));
        }

        [TestMethod]
        public async Task PutAvisClient_WithNonExistentId_ReturnsNotFound()
        {
            // Arrange
            int avisId = 999;
            var avisClient = new AvisClient { AvisId = avisId };

            _mockRepo.Setup(repo => repo.GetByIdAsync(avisId))
                .ReturnsAsync(new ActionResult<AvisClient>(new NotFoundResult()));

            // Act
            var result = await _controller.PutAvisClient(avisId, avisClient);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NotFoundObjectResult));
        }

        [TestMethod]
        public async Task PostAvisClient_WithValidModel_ReturnsCreatedAtAction()
        {
            // Arrange
            var avisClientDto = _avisClientDtos.First();
            var newAvisClient = new AvisClient
            {
                AvisId = 4,
                ClientId = avisClientDto.ClientId,
                ProduitId = avisClientDto.ProduitId,
                TitreAvis = avisClientDto.TitreAvis,
                DescriptionAvis = avisClientDto.DescriptionAvis,
                NoteAvis = avisClientDto.NoteAvis,
                DateAvis = DateTime.UtcNow
            };

            _mockRepo.Setup(repo => repo.AddAsync(It.IsAny<AvisClient>()))
                .Callback<AvisClient>(a => a.AvisId = newAvisClient.AvisId)
                .Returns(Task.CompletedTask);

            // Act
            var result = await _controller.PostAvisClient(avisClientDto);

            // Assert
            Assert.IsInstanceOfType(result, typeof(ActionResult<AvisClient>));
            var actionResult = result as ActionResult<AvisClient>;
            Assert.IsInstanceOfType(actionResult.Result, typeof(CreatedAtActionResult));
            var createdAtActionResult = actionResult.Result as CreatedAtActionResult;
            Assert.IsInstanceOfType(createdAtActionResult.Value, typeof(AvisClient));
            var model = createdAtActionResult.Value as AvisClient;
            Assert.AreEqual(newAvisClient.AvisId, model.AvisId);
            Assert.AreEqual(avisClientDto.ClientId, model.ClientId);
            Assert.AreEqual(avisClientDto.ProduitId, model.ProduitId);
        }

        [TestMethod]
        public async Task DeleteAvisClient_WithValidId_ReturnsNoContent()
        {
            // Arrange
            int avisId = 1;
            var avisClient = _avisClients.FirstOrDefault(a => a.AvisId == avisId);

            _mockRepo.Setup(repo => repo.GetByIdAsync(avisId))
                .ReturnsAsync(new ActionResult<AvisClient>(avisClient));

            _mockRepo.Setup(repo => repo.DeleteAsync(It.IsAny<AvisClient>()))
                .Returns(Task.CompletedTask);

            // Act
            var result = await _controller.DeleteAvisClient(avisId);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NoContentResult));
        }

        [TestMethod]
        public async Task DeleteAvisClient_WithInvalidId_ReturnsNotFound()
        {
            // Arrange
            /*int avisId = 999;

            _mockRepo.Setup(repo => repo.GetByIdAsync(avisId))
                .ReturnsAsync((ActionResult<AvisClient>)null);

            // Act
            var result = await _controller.DeleteAvisClient(avisId);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NotFoundObjectResult));*/
            // Arrange
            int avisId = 999;
            var avisClient = _avisClients.FirstOrDefault(a => a.AvisId == avisId);

            _mockRepo.Setup(repo => repo.GetByIdAsync(avisId))
                .ReturnsAsync(new ActionResult<AvisClient>(avisClient));

            _mockRepo.Setup(repo => repo.DeleteAsync(It.IsAny<AvisClient>()))
                .Returns(Task.CompletedTask);

            // Act
            var result = await _controller.DeleteAvisClient(avisId);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NotFoundObjectResult));
        }

    }
}