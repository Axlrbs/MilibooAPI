using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MilibooAPI.Models;
using MilibooAPI.Models.EntityFramework;
using MilibooAPI.Models.Repository;

namespace MilibooAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommandesController : ControllerBase
    {
        //private readonly MilibooContext _context;
        //private readonly IDataRepository<Commande> dataRepository;
        private readonly IDataRepositoryCommande dataRepositoryCommande;

        /// <summary>
        /// Constructeur du controller
        /// </summary>
        public CommandesController(IDataRepositoryCommande dataRepoCommande)
        {
            dataRepositoryCommande = dataRepoCommande;
        }

        /// <summary>
        /// Récupère (get) tous les clients
        /// </summary>
        /// <returns>Réponse http</returns>
        /// <response code="200">Quand tous les clients ont été renvoyés avec succès</response>
        /// <response code="500">Quand il y a une erreur de serveur interne</response>
        // GET: api/Commandes/GetAllCommandes
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<Commande>>> GetAllCommandes()
        {
            try
            {
                return await dataRepositoryCommande.GetAllAsync();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erreur interne du serveur");
            }
        }

        /// <summary>
        /// Récupère (get) une commande par son ID
        /// </summary>
        /// <param name="id">L'id de la commande</param>
        /// <returns>Réponse http</returns>
        /// <response code="200">Quand la commande a été trouvé</response>
        /// <response code="404">Quand la commande n'a pas été trouvé</response>
        /// <response code="500">Quand il y a une erreur de serveur interne</response>
        // GET: api/Commandes/GetCommandeById/{id}
        [HttpGet("[action]/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Commande>> GetCommandeById(int id)
        {
            try
            {
                var commande = await dataRepositoryCommande.GetByIdAsync(id);

                if (commande == null)
                {
                    return NotFound();
                }

                return commande;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erreur interne du serveur");
            }
            
        }

        /// <summary>
        /// Récupère (get) une commande par son ID client
        /// </summary>
        /// <param name="idclient">L'idclient de la commande</param>
        /// <returns>Réponse http</returns>
        /// <response code="200">Quand la commande a été trouvé</response>
        /// <response code="404">Quand la commande n'a pas été trouvé</response>
        /// <response code="500">Quand il y a une erreur de serveur interne</response>
        // GET: api/Commandes/GetCommandeByIdClient/{idclient}
        [HttpGet("[action]/{idclient}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Commande>> GetCommandeByIdClient(int idclient)
        {
            try
            {
                var commande = await dataRepositoryCommande.GetByIdClientAsync(idclient);

                if (commande == null)
                {
                    return NotFound();
                }

                return commande;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erreur interne du serveur");
            }
        }

        /// <summary>
        /// Récupère (get) une commande par son statut
        /// </summary>
        /// <param name="status">Le statut de la commande</param>
        /// <returns>Réponse http</returns>
        /// <response code="200">Quand la commande a été trouvé</response>
        /// <response code="404">Quand la commande n'a pas été trouvé</response>
        /// <response code="500">Quand il y a une erreur de serveur interne</response>
        // GET: api/Commandes/GetCommandeByStatus/{status}
        [HttpGet("[action]/{status}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Commande>> GetCommandeByStatus(string status)
        {
            try
            {
                var commande = await dataRepositoryCommande.GetByStringAsync(status);

                if (commande == null)
                {
                    return NotFound();
                }

                return commande;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erreur interne du serveur");
            }
            
        }


        /// <summary>
        /// Modifie (put) une commande
        /// </summary>
        /// <param name="id">L'id de la commande à modifier</param>
        /// <param name="commande">La commande modifiée</param>
        /// <returns>Réponse http</returns>
        /// <response code="204">Quand la commande a été modifié avec succès</response>
        /// <response code="400">Quand l'id ne correspond pas ou que le format de la commande est incorrect</response>
        /// <response code="404">Quand la commande n'a pas été trouvé</response>
        /// <response code="500">Quand il y a une erreur de serveur interne</response>
        // PUT: api/Commandes/PutCommande/{id}
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> PutCommande(int id, Commande commande)
        {
            if (id != commande.CommandeId)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var commandeToUpdate = await dataRepositoryCommande.GetByIdAsync(id);
            if (commandeToUpdate == null)
            {
                return NotFound();
            }
            else
            {
                await dataRepositoryCommande.UpdateAsync(commandeToUpdate.Value, commande);
                return NoContent();
            }
        }

        /// <summary>
        /// Crée (post) une nouvelle commande
        /// </summary>
        /// <param name="commande">La commande à créer</param>
        /// <returns>Réponse http</returns>
        /// <response code="201">Quand la commande a été créé avec succès</response>
        /// <response code="400">Quand le format de la commande dans le corps de la requête est incorrect</response>
        /// <response code="500">Quand il y a une erreur de serveur interne</response>
        // POST: api/Commandes/PostCommande
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Commande>> PostCommande(CreateCommandeDTO commandeDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var commande = new Commande
            {
                PanierId = commandeDTO.PanierId,
                ClientId = commandeDTO.ClientId,
                LivraisonId = commandeDTO.LivraisonId,
                CarteBancaireId = commandeDTO.CarteBancaireId,
                MontantCommande = commandeDTO.MontantCommande,
                DateFacture = DateTime.UtcNow.Date,
                NbPointFidelite = commandeDTO.NbPointFidelite,
                Statut = commandeDTO.Statut,
            };

            await dataRepositoryCommande.AddAsync(commande);

            return CreatedAtAction(nameof(GetCommandeById), new { id = commande.CommandeId }, commande);
        }

        /// <summary>
        /// Supprime (delete) une commande
        /// </summary>
        /// <param name="id">L'id de la commande à supprimer</param>
        /// <returns>Réponse http</returns>
        /// <response code="204">Quand la commande a été supprimé avec succès</response>
        /// <response code="404">Quand la commande n'a pas été trouvé</response>
        /// <response code="500">Quand il y a une erreur de serveur interne</response>
        // DELETE: api/Commandes/DeleteCommande/{id}
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteCommande(int id)
        {
            var commande = await dataRepositoryCommande.GetByIdAsync(id);

            if (commande == null)
            {
                return NotFound();
            }

            await dataRepositoryCommande.DeleteAsync(commande.Value);

            return NoContent();
        }
    }
}
