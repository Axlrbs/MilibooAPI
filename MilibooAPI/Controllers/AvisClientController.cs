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
    public class AvisClientController : ControllerBase
    {
        //private readonly MilibooContext _context;
        private readonly IDataRepository<AvisClient> dataRepository;

        /// <summary>
        /// Constructeur du controller
        /// </summary>
        public AvisClientController(IDataRepository<AvisClient> dataRepo)
        {
            dataRepository = dataRepo;
        }
        /*public AvisClientController(MilibooDBContext context)
        {
            _context = context;
        }*/

        /// <summary>
        /// Récupère (get) tous les avis clients
        /// </summary>
        /// <returns>Réponse http</returns>
        /// <response code="200">Quand tous les avis clients ont été renvoyés avec succès</response>
        /// <response code="500">Quand il y a une erreur de serveur interne</response>
        // GET: api/AvisClient
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<AvisClient>>> GetAllAvisClients()
        {
            return await dataRepository.GetAllAsync();

        }

        /// <summary>
        /// Récupère (get) tous les avis des clients par son produit ID
        /// </summary>
        /// <param name="id">L'id du produit</param>
        /// <returns>Réponse http</returns>
        /// <response code="200">Quand le produit a été trouvé</response>
        /// <response code="404">Quand le produit n'a pas été trouvé</response>
        /// <response code="500">Quand il y a une erreur de serveur interne</response>
        // GET: api/AvisClient/5
        [HttpGet("[action]/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<AvisClient>> GetAllAvisClientByProduitId(int id)
        {
            var avisClient = await dataRepository.GetByIdAsync(id);

            if (avisClient == null)
            {
                return NotFound();
            }

            return avisClient;
        }

        /// <summary>
        /// Modifie (put) un avis client
        /// </summary>
        /// <param name="id">L'id de l'avis client à modifier</param>
        /// <param name="avisClient">L'avis client modifié</param>
        /// <returns>Réponse http</returns>
        /// <response code="204">Quand l'avis client a été modifié avec succès</response>
        /// <response code="400">Quand l'id ne correspond pas ou que le format de 'l'avis client est incorrect</response>
        /// <response code="404">Quand l'avis client n'a pas été trouvé</response>
        /// <response code="500">Quand il y a une erreur de serveur interne</response>
        // PUT: api/AvisClient/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> PutAvisClient(int id, AvisClient avisClient)
        {
            if (id != avisClient.AvisId)
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
                await dataRepository.UpdateAsync(opinionToUpdate.Value, avisClient);
                return NoContent();
            }
        }

        /// <summary>
        /// Crée (post) un nouvel avis client
        /// </summary>
        /// <param name="avisClient">L'avis client à créer</param>
        /// <returns>Réponse http</returns>
        /// <response code="201">Quand l'avis client a été créé avec succès</response>
        /// <response code="400">Quand le format de l'avis client dans le corps de la requête est incorrect</response>
        /// <response code="500">Quand il y a une erreur de serveur interne</response>
        // POST: api/AvisClient
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<AvisClient>> PostAvisClient(AvisClient avisClient)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await dataRepository.AddAsync(avisClient);

            return CreatedAtAction("GetAvisClientById", new { id = avisClient.AvisId }, avisClient);
        }

        /// <summary>
        /// Supprime (delete) un avis client
        /// </summary>
        /// <param name="id">L'id de l'avis client à supprimer</param>
        /// <returns>Réponse http</returns>
        /// <response code="204">Quand l'avis client a été supprimé avec succès</response>
        /// <response code="404">Quand l'avis client n'a pas été trouvé</response>
        /// <response code="500">Quand il y a une erreur de serveur interne</response>
        // DELETE: api/AvisClient/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteAvisClient(int id)
        {
            var avisClient = await dataRepository.GetByIdAsync(id);
            
            if (avisClient == null)
            {
                return NotFound();
            }

            await dataRepository.DeleteAsync(avisClient.Value);

            return NoContent();
        }

        /*private bool AvisClientExists(int id)
        {
            return _context.AvisClients.Any(e => e.AvisId == id);
        }*/
    }
}
