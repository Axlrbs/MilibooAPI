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

        private readonly MilibooDBContext _context;
        /// <summary>
        /// Constructeur du controller
        /// </summary>
        public AvisClientController(MilibooDBContext context)
        {
            _context = context;
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
        public async Task<ActionResult<IEnumerable<AvisClient>>> GetAllAvisClientByProduitId(int id)
        {
            var avisClients = await _context.AvisClients
                                            .Where(a => a.ProduitId == id)
                                            .ToListAsync();

            if (avisClients == null || avisClients.Count == 0)
            {
                return NotFound($"Aucun avis trouvé pour le produit avec l'ID {id}.");
            }

            return Ok(avisClients);
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
        /// Récupère un avis client par son ID
        /// </summary>
        /// <param name="id">ID de l'avis client</param>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<AvisClient>> GetAvisClientById(int id)
        {
            var avisClient = await _context.AvisClients
                .Include(a => a.IdclientNavigation)
                .Include(a => a.IdproduitNavigation)
                .FirstOrDefaultAsync(a => a.AvisId == id);

            if (avisClient == null)
            {
                return NotFound($"Avis client avec l'ID {id} non trouvé.");
            }

            return Ok(avisClient);
        }


        [HttpPost]
        public async Task<ActionResult<AvisClient>> PostAvisClient(AvisClientDto avisClientDto)
        {
            // Validate input
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Verify client exists
            var clientExists = await _context.Clients.AnyAsync(c => c.ClientId == avisClientDto.ClientId);
            if (!clientExists)
            {
                return BadRequest($"Client with ID {avisClientDto.ClientId} not found.");
            }

            // Verify product exists
            var productExists = await _context.Produits.AnyAsync(p => p.ProduitId == avisClientDto.ProduitId);
            if (!productExists)
            {
                return BadRequest($"Product with ID {avisClientDto.ProduitId} not found.");
            }

            // Additional validations
            if (avisClientDto.NoteAvis.HasValue && (avisClientDto.NoteAvis < 1 || avisClientDto.NoteAvis > 5))
            {
                return BadRequest("Rating must be between 1 and 5.");
            }

            // Create new AvisClient
            var avisClient = new AvisClient
            {
                // Explicitly do NOT set AvisId
                ClientId = avisClientDto.ClientId,
                ProduitId = avisClientDto.ProduitId,
                DescriptionAvis = avisClientDto.DescriptionAvis,
                NoteAvis = avisClientDto.NoteAvis,
                TitreAvis = avisClientDto.TitreAvis,
                DateAvis = DateTime.UtcNow
            };

            try
            {
                // Explicitly detach any tracked entities with the same key
                var existingEntry = _context.ChangeTracker.Entries<AvisClient>()
                    .FirstOrDefault(e => e.Entity.AvisId == avisClient.AvisId);

                if (existingEntry != null)
                {
                    _context.Entry(existingEntry.Entity).State = EntityState.Detached;
                }

                // Add and save
                _context.AvisClients.Add(avisClient);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetAvisClientById), new { id = avisClient.AvisId }, avisClient);
            }
            catch (DbUpdateException ex)
            {
                // Log the full exception details
                return StatusCode(500, "An error occurred while saving the review.");
            }
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
