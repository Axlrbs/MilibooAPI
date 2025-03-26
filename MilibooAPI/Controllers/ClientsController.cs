using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MilibooAPI.Models;
using MilibooAPI.Models.EntityFramework;
using MilibooAPI.Models.Repository;
using System.Security.Claims;
using System.ComponentModel.DataAnnotations;


namespace MilibooAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientsController : ControllerBase
    {
        //private readonly MilibooContext _context;
        private readonly IDataRepository<Client> dataRepository;
        private readonly IDataRepositoryClient<Client> dataRepositoryClient;
        private readonly ILogger<ClientsController> _logger;

        /// <summary>
        /// Constructeur du controller
        /// </summary>
        public ClientsController(IDataRepository<Client> dataRepo, IDataRepositoryClient<Client> dataRepoClient, ILogger<ClientsController> logger)
        {
            dataRepository = dataRepo;
            dataRepositoryClient = dataRepoClient;
            _logger = logger;  // Injection du logger
        }
        /*public ClientsController(MilibooContext context)
        {
            _context = context;
        }*/

        /// <summary>
        /// Récupère (get) tous les clients
        /// </summary>
        /// <returns>Réponse http</returns>
        /// <response code="200">Quand tous les clients ont été renvoyés avec succès</response>
        /// <response code="500">Quand il y a une erreur de serveur interne</response>
        // GET: api/Clients/GetAllClients
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<Client>>> GetAllClients()
        {
            return await dataRepository.GetAllAsync();
        }

        /// <summary>
        /// Récupère (get) un client par son ID
        /// </summary>
        /// <param name="id">L'id du client</param>
        /// <returns>Réponse http</returns>
        /// <response code="200">Quand le client a été trouvé</response>
        /// <response code="404">Quand le client n'a pas été trouvé</response>
        /// <response code="500">Quand il y a une erreur de serveur interne</response>
        // GET: api/Clients/GetClientById/{id}
        [HttpGet("[action]/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Client>> GetClientById(int id)
        {
            var client = await dataRepository.GetByIdAsync(id);

            if (client == null)
            {
                return NotFound();
            }

            return client;
        }

        /// <summary>
        /// Récupère (get) un client par son nom
        /// </summary>
        /// <param name="nom">Le nom du client</param>
        /// <returns>Réponse http</returns>
        /// <response code="200">Quand le client a été trouvé</response>
        /// <response code="404">Quand le client n'a pas été trouvé</response>
        /// <response code="500">Quand il y a une erreur de serveur interne</response>
        // GET: api/Clients/GetClientByNom/{nom}
        [HttpGet("[action]/{nom}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Client>> GetClientByNom(string nom)
        {
            var client = await dataRepository.GetByStringAsync(nom);

            if (client == null)
            {
                return NotFound();
            }

            return client;
        }

        /// <summary>
        /// Récupère (get) un client par son email
        /// </summary>
        /// <param name="email">Le email du client</param>
        /// <returns>Réponse http</returns>
        /// <response code="200">Quand le client a été trouvé</response>
        /// <response code="404">Quand le client n'a pas été trouvé</response>
        /// <response code="500">Quand il y a une erreur de serveur interne</response>
        // GET: api/Clients/GetClientByNom/{email}
        [HttpGet("[action]/{email}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Client>> GetClientByEmail(string email)
        {
            var client = await dataRepositoryClient.GetByStringBisAsync(email);

            if (client == null)
            {
                return NotFound();
            }

            return client;
        }

        /// <summary>
        /// Modifie (put) un client
        /// </summary>
        /// <param name="id">L'id du client à modifier</param>
        /// <param name="client">Le client modifié</param>
        /// <returns>Réponse http</returns>
        /// <response code="204">Quand le client a été modifié avec succès</response>
        /// <response code="400">Quand l'id ne correspond pas ou que le format du client est incorrect</response>
        /// <response code="404">Quand le client n'a pas été trouvé</response>
        /// <response code="500">Quand il y a une erreur de serveur interne</response>
        // PUT: api/Clients/PutClient/{id}
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> PutClient(int id, Client client)
        {
            if (id != client.ClientId)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userToUpdate = await dataRepository.GetByIdAsync(id);
            if (userToUpdate == null)
            {
                return NotFound();
            }
            else
            {
                await dataRepository.UpdateAsync(userToUpdate.Value, client);
                return NoContent();
            }
        }

        /// <summary>
        /// Crée (post) un nouveau client
        /// </summary>
        /// <param name="clientDTO">Le client à créer</param>
        /// <returns>Réponse http</returns>
        /// <response code="201">Quand le client a été créé avec succès</response>
        /// <response code="400">Quand le format du client dans le corps de la requête est incorrect</response>
        /// <response code="500">Quand il y a une erreur de serveur interne</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Client>> PostClient([FromBody] CreateClientDTO clientDTO)
        {
            try
            {
                // Validation des données reçues
                if (string.IsNullOrEmpty(clientDTO.NomPersonne) || string.IsNullOrEmpty(clientDTO.PrenomPersonne) ||
                    string.IsNullOrEmpty(clientDTO.TelPersonne) || string.IsNullOrEmpty(clientDTO.EmailClient) ||
                    string.IsNullOrEmpty(clientDTO.MdpClient))
                {
                    _logger.LogWarning("Les champs obligatoires sont manquants.");
                    return BadRequest("Tous les champs sont obligatoires.");
                }

                // Validation de l'email
                if (!new EmailAddressAttribute().IsValid(clientDTO.EmailClient))
                {
                    _logger.LogWarning($"L'email fourni est invalide : {clientDTO.EmailClient}");
                    return BadRequest("L'email fourni est invalide.");
                }

                // Créer un nouveau client avec les données du DTO
                var newClient = new Client
                {
                    NomPersonne = clientDTO.NomPersonne,
                    PrenomPersonne = clientDTO.PrenomPersonne,
                    TelPersonne = clientDTO.TelPersonne,
                    EmailClient = clientDTO.EmailClient,
                    MdpClient = clientDTO.MdpClient, // Assurez-vous de crypter le mot de passe avant de l'enregistrer
                    NbTotalPointsFidelite = 0,
                    MoyenneAvis = 0,
                    NombreAvisDepose = 0,
                    IsVerified = false,
                    DateDerniereUtilisation = DateTime.UtcNow,
                    DateAnonymisation = DateTime.UtcNow
                };

                _logger.LogInformation($"Création d'un nouveau client : {newClient.EmailClient}");

                // Ajouter le client dans la base de données
                await dataRepository.AddAsync(newClient);

                _logger.LogInformation($"Client créé avec succès : {newClient.ClientId}");

                return CreatedAtAction("GetClientById", new { id = newClient.ClientId }, newClient);
            }
            catch (Exception ex)
            {
                // Log de l'erreur si quelque chose échoue
                _logger.LogError($"Erreur lors de la création du client : {ex.Message}", ex);
                return StatusCode(StatusCodes.Status500InternalServerError, "Une erreur s'est produite lors de la création du client.");
            }
        }



        /// <summary>
        /// Supprime (delete) un client
        /// </summary>
        /// <param name="id">L'id du client à supprimer</param>
        /// <returns>Réponse http</returns>
        /// <response code="204">Quand le client a été supprimé avec succès</response>
        /// <response code="404">Quand le client n'a pas été trouvé</response>
        /// <response code="500">Quand il y a une erreur de serveur interne</response>
        // DELETE: api/Clients/DeleteClient/{id}
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteClient(int id)
        {
            var client = await dataRepository.GetByIdAsync(id);

            if (client == null)
            {
                return NotFound();
            }

            await dataRepository.DeleteAsync(client.Value);

            return NoContent();
        }

        /*
        [HttpGet]
        [Authorize(Policy = Policies.Authorized)]
        public async Task<ActionResult<Client>> GetClientData()
        {
            var userName = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var compte = await dataRepository.GetByStringAsync(userName);

            return compte;
        }
        */

        /*private bool ClientExists(int id)
        {
            return _context.Clients.Any(e => e.IdClient == id);
        }*/
    }
}
