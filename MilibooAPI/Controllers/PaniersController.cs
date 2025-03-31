using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MilibooAPI.Models.EntityFramework;
using MilibooAPI.Models.Repository;


namespace MilibooAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaniersController : ControllerBase
    {
        //private readonly MilibooDBContext _context;
        private readonly IDataRepository<Panier> dataRepository;

        public PaniersController(IDataRepository<Panier> dataRepo)
        {
            dataRepository = dataRepo;
        }

        /// <summary>
        /// Récupère (get) tous les paniers
        /// </summary>
        /// <returns>Réponse http</returns>
        /// <response code="200">Quand tous les panier ont été renvoyés avec succès</response>
        /// <response code="500">Quand il y a une erreur de serveur interne</response>
        // GET: api/Paniers
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<Panier>>> GetAllPaniers()
        {
            return await dataRepository.GetAllAsync();
        }


        /// <summary>
        /// Récupère (get) un Panier par l'ID du client
        /// </summary>
        /// <param name="clientId">L'ID du client</param>
        /// <returns>Réponse HTTP</returns>
        /// <response code="200">Quand le panier a été trouvé</response>
        /// <response code="404">Quand le panier n'a pas été trouvé</response>
        /// <response code="500">Quand il y a une erreur de serveur interne</response>
        // GET: api/Paniers/5
        [HttpGet("[action]/{clientId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Panier>> GetPanierByIdClient(int clientId)
        {
            var panier = await dataRepository.GetByIdAsync(clientId);  

            if (panier == null)
            {
                return NotFound();
            }

            return panier;
        }


        /// <summary>
        /// Modifie (put) un panier
        /// </summary>
        /// <param name="id">L'id du panier à modifier</param>
        /// <param name="panier">Le panier modifié</param>
        /// <returns>Réponse http</returns>
        /// <response code="204">Quand le panier a été modifié avec succès</response>
        /// <response code="400">Quand l'id ne correspond pas ou que le format du panier est incorrect</response>
        /// <response code="404">Quand le panier n'a pas été trouvé</response>
        /// <response code="500">Quand il y a une erreur de serveur interne</response>
        // PUT: api/Paniers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> PutPanier(int id, Panier panier)
        {
            if (id != panier.PanierId)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var opinionToUpdate = await dataRepository.GetByIdAsync(id);
            if (opinionToUpdate == null)
            {
                return NotFound();
            }
            else
            {
                await dataRepository.UpdateAsync(opinionToUpdate.Value, panier);
                return NoContent();
            }
        }

        /// <summary>
        /// Crée un nouveau panier pour un client donné
        /// </summary>
        /// <param name="clientId">L'ID du client</param>
        /// <returns>Réponse HTTP</returns>
        /// <response code="201">Quand le panier est créé avec succès</response>
        /// <response code="400">Quand la requête est invalide</response>
        /// <response code="500">Quand il y a une erreur de serveur interne</response>
        [HttpPost]
        public async Task<ActionResult<Panier>> PostPanier(int clientId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var panier = new Panier
            {
                ClientId = clientId,
                Dateetheure = DateOnly.FromDateTime(DateTime.UtcNow) // Date du panier
            };

            await dataRepository.AddAsync(panier);  // Ajoute le panier via ton DataRepository

            // Renvoie un code 201 et l'URL de l'action pour récupérer ce panier
            return CreatedAtAction(nameof(GetPanierByIdClient), new { clientId = panier.ClientId }, panier);
        }


        /// <summary>
        /// Supprime (delete) un panier
        /// </summary>
        /// <param name="id">L'id du panier à supprimer</param>
        /// <returns>Réponse http</returns>
        /// <response code="204">Quand le panier a été supprimé avec succès</response>
        /// <response code="404">Quand le panier n'a pas été trouvé</response>
        /// <response code="500">Quand il y a une erreur de serveur interne</response>
        // DELETE: api/Paniers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePanier(int id)
        {
            var panier = await dataRepository.GetByIdAsync(id);
            if (panier == null)
            {
                return NotFound();
            }

            await dataRepository.DeleteAsync(panier.Value);

            return NoContent();
        }

       /* private bool PanierExists(int id)
        {
            return _context.Paniers.Any(e => e.PanierId == id);
        }*/
    }
}
