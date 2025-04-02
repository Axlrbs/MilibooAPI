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
        private readonly IDataRepositoryAvisClient _dataRepository;

        /// <summary>
        /// Constructeur du controller
        /// </summary>
        public AvisClientController(
            IDataRepositoryAvisClient dataRepository)
        {
            _dataRepository = dataRepository;
        }

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
            return await _dataRepository.GetAllAsync();
        }

        /// <summary>
        /// Récupère (get) tous les avis des clients par son produit ID
        /// </summary>
        /// <param name="id">L'id du produit</param>
        /// <returns>Réponse http</returns>
        /// <response code="200">Quand le produit a été trouvé</response>
        /// <response code="404">Quand le produit n'a pas été trouvé</response>
        /// <response code="500">Quand il y a une erreur de serveur interne</response>
        // GET: api/AvisClient/ByProduct/5
        [HttpGet("ByProduct/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<AvisClientDto>>> GetAllAvisClientByProduitId(int id)
        {
            var avisClient = await _dataRepository.GetAllByProduitIdAsync(id);

            if (avisClient.Value == null || avisClient.Result is NotFoundResult)
            {
                return NotFound($"Avis client avec l'ID du produit {id} non trouvé.");
            }

            return avisClient;
        }

        /// <summary>
        /// Récupère un avis client par son ID
        /// </summary>
        /// <param name="id">ID de l'avis client</param>
        /// <returns>Réponse http</returns>
        /// <response code="200">Quand l'avis client a été trouvé</response>
        /// <response code="404">Quand l'avis client n'a pas été trouvé</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<AvisClient>> GetAvisClientById(int id)
        {
            var avisClient = await _dataRepository.GetByIdAsync(id);

            if (avisClient.Value == null || avisClient.Result is NotFoundResult)
            {
                return NotFound($"Avis client avec l'ID {id} non trouvé.");
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

            var avisToUpdate = await _dataRepository.GetByIdAsync(id);
            if (avisToUpdate.Value == null || avisToUpdate.Result is NotFoundResult)
            {
                return NotFound($"Avis client avec l'ID {id} non trouvé.");
            }

            await _dataRepository.UpdateAsync(avisToUpdate.Value, avisClient);
            return NoContent();
        }

        /// <summary>
        /// Crée (post) un nouvel avis client
        /// </summary>
        /// <param name="avisClientDto">Les données du nouvel avis client</param>
        /// <returns>Réponse http</returns>
        /// <response code="201">Quand l'avis client a été créé avec succès</response>
        /// <response code="400">Quand le format est incorrect</response>
        /// <response code="500">Quand il y a une erreur de serveur interne</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<AvisClient>> PostAvisClient(AvisClientDto avisClientDto)
        {
            // Validate input
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var avisClient = new AvisClient
            {
                ClientId = avisClientDto.ClientId,
                ProduitId = avisClientDto.ProduitId,
                DescriptionAvis = avisClientDto.DescriptionAvis,
                NoteAvis = avisClientDto.NoteAvis,
                TitreAvis = avisClientDto.TitreAvis,
                DateAvis = DateTime.UtcNow,
            };

                await _dataRepository.AddAsync(avisClient);
                return CreatedAtAction(nameof(GetAvisClientById), new { id = avisClient.AvisId }, avisClient);
                        
        }

        /// <summary>
        /// Supprime (delete) un avis client
        /// </summary>
        /// <param name="id">L'id de l'avis client à supprimer</param>
        /// <returns>Réponse http</returns>
        /// <response code="204">Quand l'avis client a été supprimé avec succès</response>
        /// <response code="404">Quand l'avis client n'a pas été trouvé</response>
        /// <response code="500">Quand il y a une erreur de serveur interne</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteAvisClient(int id)
        {
            var avisClient = await _dataRepository.GetByIdAsync(id);

            if (avisClient.Value == null || avisClient.Result is NotFoundResult)
            {
                return NotFound($"Avis client avec l'ID {id} non trouvé.");
            }

            await _dataRepository.DeleteAsync(avisClient.Value);
            return NoContent();
        }
    }
}