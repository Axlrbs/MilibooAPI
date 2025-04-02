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
    public class ClientsControllerTests
    {
        private MilibooDBContext context;
        private ClientsController controller;
        private IDataRepositoryClient dataRepository;

        private Mock<IDataRepositoryClient> mockRepository;
        private ClientsController controllerMoq;

        [TestInitialize]
        public void TestInitialize()
        {
            var builder = new DbContextOptionsBuilder<MilibooDBContext>().UseNpgsql();
            context = new MilibooDBContext();
            //controller = new UtilisateursController(context);
            dataRepository = new ClientManager(context);
            controller = new ClientsController(dataRepository);

            mockRepository = new Mock<IDataRepositoryClient>();
            controllerMoq = new ClientsController(mockRepository.Object);
        }

        [TestMethod()]
        public void ClientsControllerTest()
        {
            // Assert
            Assert.IsNotNull(context, "Le contexte est nul.");
            Assert.IsNotNull(controller, "Le controller est nul.");
        }

        [TestMethod()]
        public void GetAllClientsTest()
        {
            // Arrange
            var lesClients = context.Clients.ToList();

            // Act
            var result = controller.GetAllClients().Result;
            var listeClients = result.Value.ToList();

            // Assert
            Assert.IsNotNull(result, "Le résultat est nul.");
            Assert.IsInstanceOfType(result, typeof(ActionResult<IEnumerable<Client>>), "Pas un ActionResult.");
            CollectionAssert.AreEqual(lesClients, listeClients, "Clients pas identiques.");
        }

        [TestMethod]
        public async Task GetAllClients_ShouldReturn500_WhenExceptionIsThrown()
        {
            // Arrange
            mockRepository.Setup(repo => repo.GetAllAsync()).ThrowsAsync(new Exception("Database error"));

            // Act
            var result = await controllerMoq.GetAllClients();

            // Assert
            var statusCodeResult = result.Result as ObjectResult;
            Assert.IsNotNull(statusCodeResult);
            Assert.AreEqual(StatusCodes.Status500InternalServerError, statusCodeResult.StatusCode);
        }

        [TestMethod()]
        public void GetClientById_ExistingIdPassed_ReturnsRightItem()
        {
            // Arrange
            var client1 = context.Clients.Where(u => u.ClientId == 1).FirstOrDefault();

            // Act
            var result = controller.GetClientById(1).Result;

            // Assert
            Assert.IsNotNull(result, "Aucun résultat.");
            Assert.IsInstanceOfType(result, typeof(ActionResult<Client>), "Pas un ActionResult.");
            Assert.IsNull(result.Result, "Il y a une erreur.");
            Assert.IsInstanceOfType(result.Value, typeof(Client), "Pas un Client");
            Assert.AreEqual(client1, result.Value, "Clients pas identiques.");
        }

        [TestMethod()]
        public void GetClientById_ExistingIdPassed_ReturnsRightItem_AvecMoq()
        {
            // Arrange
            Client client = new Client
            {
                ClientId = 5,
                NomPersonne = "Anonymized",
                PrenomPersonne = "Anonymized",
                TelPersonne = null,
                EmailClient = "anonymized.5@example.org",
                MdpClient = "IXT75PFE6LC!",
                NbTotalPointsFidelite = 185,
                MoyenneAvis = 3,
                NombreAvisDepose = 46,
                IsVerified = false,
                DateDerniereUtilisation = new DateTime(2022, 7, 26, 13, 10, 33, 434),
                DateAnonymisation = null
            };
            mockRepository.Setup(x => x.GetByIdAsync(5).Result).Returns(client);

            // Act
            var result = controllerMoq.GetClientById(5).Result;

            // Assert
            Assert.IsNotNull(result, "Aucun résultat.");
            Assert.IsInstanceOfType(result, typeof(ActionResult<Client>), "Pas un ActionResult.");
            Assert.IsNull(result.Result, "Il y a une erreur.");
            Assert.IsInstanceOfType(result.Value, typeof(Client), "Pas un Client");
            Assert.AreEqual(client, result.Value, "Clients pas identiques.");
        }

        [TestMethod]
        public void GetClientById_UnknownIdPassed_ReturnsNotFoundResult_Moq()
        {
            var mockRepository = new Mock<IDataRepositoryClient>();
            var clientController = new ClientsController(mockRepository.Object);

            // Act
            var actionResult = clientController.GetClientById(0).Result;

            // Assert
            Assert.IsInstanceOfType(actionResult.Result, typeof(NotFoundResult));
        }

        [TestMethod]
        public async Task GetClientById_ShouldReturn500_WhenExceptionIsThrown()
        {
            // Arrange
            int id = 1;
            mockRepository.Setup(repo => repo.GetByIdAsync(id)).ThrowsAsync(new Exception("Database error"));

            // Act
            var result = await controllerMoq.GetClientById(id);

            // Assert
            var statusCodeResult = result.Result as ObjectResult;
            Assert.IsNotNull(statusCodeResult);
            Assert.AreEqual(StatusCodes.Status500InternalServerError, statusCodeResult.StatusCode);
        }

        [TestMethod()]
        public void GetClientByNom_ExistingIdPassed_ReturnsRightItem()
        {
            // Arrange
            var client1 = context.Clients.Where(u => u.NomPersonne == "CHAZOT").FirstOrDefault();

            // Act
            var result = controller.GetClientByNom("CHAZOT").Result;

            // Assert
            Assert.IsInstanceOfType(result, typeof(ActionResult<Client>), "Pas un ActionResult.");
            Assert.IsNull(result.Result, "Il y a une erreur.");
            Assert.IsInstanceOfType(result.Value, typeof(Client), "Pas un client");
            Assert.AreEqual(client1, result.Value, "Clients pas identiques.");
        }

        [TestMethod()]
        public void GetUClientByNom_ExistingNomPassed_ReturnsRightItem_AvecMoq()
        {
            // Arrange
            Client client = new Client
            {
                ClientId = 5,
                NomPersonne = "Anonymized",
                PrenomPersonne = "Anonymized",
                TelPersonne = null,
                EmailClient = "anonymized.5@example.org",
                MdpClient = "IXT75PFE6LC!",
                NbTotalPointsFidelite = 185,
                MoyenneAvis = 3,
                NombreAvisDepose = 46,
                IsVerified = false,
                DateDerniereUtilisation = new DateTime(2022, 7, 26, 13, 10, 33, 434),
                DateAnonymisation = null
            };
            mockRepository.Setup(x => x.GetByStringAsync("Anonymized").Result).Returns(client);

            // Act
            var result = controllerMoq.GetClientByNom("Anonymized").Result;

            // Assert
            Assert.IsNotNull(result, "Aucun résultat.");
            Assert.IsInstanceOfType(result, typeof(ActionResult<Client>), "Pas un ActionResult.");
            Assert.IsNull(result.Result, "Il y a une erreur.");
            Assert.IsInstanceOfType(result.Value, typeof(Client), "Pas un Client");
            Assert.AreEqual(client, result.Value, "Clients pas identiques.");
        }

        [TestMethod]
        public void GetClientByNom_UnknownNomPassed_ReturnsNotFoundResult_Moq()
        {
            var mockRepository = new Mock<IDataRepositoryClient>();
            var clientController = new ClientsController(mockRepository.Object);

            // Act
            var actionResult = clientController.GetClientByNom("a").Result;

            // Assert
            Assert.IsInstanceOfType(actionResult.Result, typeof(NotFoundResult));
        }

        [TestMethod]
        public async Task GetClientByNom_ShouldReturn500_WhenExceptionIsThrown()
        {
            // Arrange
            string nom = "CHAZOT";
            mockRepository.Setup(repo => repo.GetByStringAsync(nom)).ThrowsAsync(new Exception("Database error"));

            // Act
            var result = await controllerMoq.GetClientByNom(nom);

            // Assert
            var statusCodeResult = result.Result as ObjectResult;
            Assert.IsNotNull(statusCodeResult);
            Assert.AreEqual(StatusCodes.Status500InternalServerError, statusCodeResult.StatusCode);
        }

        [TestMethod()]
        public void GetClientByEmail_ExistingEmailPassed_ReturnsRightItem()
        {
            // Arrange
            var client1 = context.Clients.Where(u => u.EmailClient == "graziaem@gmail.com").FirstOrDefault();

            // Act
            var result = controller.GetClientByEmail("graziaem@gmail.com").Result;

            // Assert
            Assert.IsInstanceOfType(result, typeof(ActionResult<Client>), "Pas un ActionResult.");
            Assert.IsNull(result.Result, "Il y a une erreur.");
            Assert.IsInstanceOfType(result.Value, typeof(Client), "Pas un client");
            Assert.AreEqual(client1, result.Value, "Clients pas identiques.");
        }

        [TestMethod]
        public void GetClientByEmail_UnknownEmailPassed_ReturnsNotFoundResult_Moq()
        {
            var mockRepository = new Mock<IDataRepositoryClient>();
            var clientController = new ClientsController(mockRepository.Object);

            // Act
            var actionResult = clientController.GetClientByEmail("graziaem@gmail.comdazdadz").Result;

            // Assert
            Assert.IsInstanceOfType(actionResult.Result, typeof(NotFoundResult));
        }

        [TestMethod]
        public async Task GetClientByMail_ShouldReturn500_WhenExceptionIsThrown()
        {
            // Arrange
            string email = "graziaem@gmail.com";
            mockRepository.Setup(repo => repo.GetByStringBisAsync(email)).ThrowsAsync(new Exception("Database error"));

            // Act
            var result = await controllerMoq.GetClientByEmail(email);

            // Assert
            var statusCodeResult = result.Result as ObjectResult;
            Assert.IsNotNull(statusCodeResult);
            Assert.AreEqual(StatusCodes.Status500InternalServerError, statusCodeResult.StatusCode);
        }

        [TestMethod()]
        public void PutClient_ValidUpdate_ReturnsNoContent()
        {
            // Arrange
            Random rnd = new Random();
            int chiffre = rnd.Next(1, 1000000000);

            Client clientATester = new Client()
            {
                ClientId = 13,
                NomPersonne = "Walter",
                PrenomPersonne = "Gilbert",
                TelPersonne = "0721481415",
                EmailClient = "gilbert_walter" + chiffre + "@hotmail.org",
                MdpClient = "WHG36MIH8FO",
                NbTotalPointsFidelite = 61,
                MoyenneAvis = (decimal)2.2,
                NombreAvisDepose = 41
            };

            // Act
            var result = controller.PutClient(13, clientATester).Result;

            // Assert
            var utilisateur1 = context.Clients.Where(u => u.ClientId == 13).FirstOrDefault();
            Assert.IsInstanceOfType(result, typeof(NoContentResult), "N'est pas un NoContent");
            Assert.AreEqual(((NoContentResult)result).StatusCode, StatusCodes.Status204NoContent, "N'est pas 204");
            Assert.AreNotEqual(utilisateur1, clientATester, "L'Utilisateur n'a pas été modifié !");
        }

        [TestMethod()]
        public void PutClient_ValidUpdate_ReturnsNoContent_Moq()
        {
            // Arrange
            Random rnd = new Random();
            int chiffre = rnd.Next(1, 1000000000);

            Client clientAvant = new Client()
            {
                ClientId = 13,
                NomPersonne = "Walter",
                PrenomPersonne = "Gilbert",
                TelPersonne = "0721481415",
                EmailClient = "gilbert_walter@hotmail.org",
                MdpClient = "WHG36MIH8FO",
                NbTotalPointsFidelite = 61,
                MoyenneAvis = (decimal)2.2,
                NombreAvisDepose = 41
            };

            Client clientApres = new Client()
            {
                ClientId = 13,
                NomPersonne = "Walter",
                PrenomPersonne = "Gilbert",
                TelPersonne = "0721481415",
                EmailClient = "g.walter@hotmail.org", //email mofifié
                MdpClient = "WHG36MIH8FO",
                NbTotalPointsFidelite = 61,
                MoyenneAvis = (decimal)2.2,
                NombreAvisDepose = 41
            };
            mockRepository.Setup(x => x.GetByIdAsync(13).Result).Returns(clientAvant);

            // Act
            var result = controllerMoq.PutClient(13, clientApres).Result;

            // Assert
            Assert.IsInstanceOfType(result, typeof(NoContentResult), "N'est pas un NoContent");
            Assert.AreEqual(((NoContentResult)result).StatusCode, StatusCodes.Status204NoContent, "N'est pas 204");

            mockRepository.Verify(x => x.UpdateAsync(It.IsAny<Client>(), clientApres), Times.Once, "La mise à jour n'a pas été effectuée !");
        }

        [TestMethod]
        public void PostClient_ModelValidated_CreationOK()
        {
            // Arrange
            Random rnd = new Random();
            int chiffre = rnd.Next(1, 1000000000);

            CreateClientDTO clientATester = new CreateClientDTO()
            {
                NomPersonne = "Berthet",
                PrenomPersonne = "Mano",
                TelPersonne = "0626910251",
                EmailClient = "mano.berthet@etu.univ-smb.fr",
                MdpClient = "IXT75PFE6LC!"
            };

            // Act
            var result = controller.PostClient(clientATester).Result;

            // Assert
            Client? clientRecupere = context.Clients.Where(u => u.EmailClient.ToUpper() == clientATester.EmailClient.ToUpper()).FirstOrDefault();

            int clientId = context.Clients.OrderByDescending(c => c.ClientId)
                      .Select(c => c.ClientId)
                      .FirstOrDefault();

            clientId = clientRecupere.ClientId;
            Assert.IsInstanceOfType(result, typeof(ActionResult<Client>), "Pas un ActionResult<Utilisateur>");
            Assert.IsInstanceOfType(result.Result, typeof(CreatedAtActionResult), "Pas un CreatedAtActionResult");

            var createdAtRouteResult = result.Result as CreatedAtActionResult;

            CreateClientDTO dernierClientEnDTO = new CreateClientDTO()
            {
                NomPersonne = clientRecupere.NomPersonne,
                PrenomPersonne = clientRecupere.PrenomPersonne, 
                TelPersonne = clientRecupere.TelPersonne,
                EmailClient = clientRecupere.EmailClient,
                MdpClient = clientRecupere.MdpClient
            };

            //Assert.IsInstanceOfType(result.Value, typeof(Client), "Pas un Client");
            Assert.AreEqual(clientATester, dernierClientEnDTO, $"Clients pas identiques, email : {clientRecupere.NomPersonne} - {clientATester.NomPersonne}" +
                $", prenom : {clientRecupere.PrenomPersonne} - {clientATester.PrenomPersonne}" +
                $", tel : {clientRecupere.TelPersonne} - {clientATester.TelPersonne}" +
                $", Email : {clientRecupere.EmailClient} - {clientATester.EmailClient}" +
                $", MdpClient : {clientRecupere.MdpClient} - {clientATester.MdpClient}");
        }

        [TestMethod]
        public void DeleteClientTest_OK()
        {
            // Arrange
            CreateClientDTO nouveauClient = new CreateClientDTO()
            {
                NomPersonne = "Nouveau",
                PrenomPersonne = "Client",
                TelPersonne = "0626910251",
                EmailClient = "mano.berthet@etu.univ-smb.fr",
                MdpClient = "IXT75PFE6LC!"
            };
            var post = controller.PostClient(nouveauClient).Result;

            int clientId = context.Clients.OrderByDescending(c => c.ClientId)
                     .Select(c => c.ClientId)
                     .FirstOrDefault();

            // Act
            var result = controller.DeleteClient(clientId).Result;

            // Assert
            Assert.IsInstanceOfType(result, typeof(NoContentResult));
            var utilisateurSupprime = context.Clients.Find(clientId);
            Assert.IsNull(utilisateurSupprime);
        }

    }
}