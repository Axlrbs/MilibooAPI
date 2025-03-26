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
    public class TypePaiementsController : ControllerBase
    {
        private readonly IDataRepository<TypePaiement> dataRepository;

        /// <summary>
        /// Constructeur du controller
        /// </summary>
        public TypePaiementsController(IDataRepository<TypePaiement> dataRepo)
        {
            dataRepository = dataRepo;
        }

        /// <summary>
        /// Récupère (get) tous les types paiements
        /// </summary>
        /// <returns>Réponse http</returns>
        /// <response code="200">Quand tous les types paiements ont été renvoyés avec succès</response>
        /// <response code="500">Quand il y a une erreur de serveur interne</response>
        // GET: api/TypePaiements
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<TypePaiement>>> GetTypePaiements()
        {
            return await dataRepository.GetAllAsync();
        }

        /// <summary>
        /// Récupère (get) un type paiement par son ID
        /// </summary>
        /// <param name="id">L'id du type paiement</param>
        /// <returns>Réponse http</returns>
        /// <response code="200">Quand le type paiement a été trouvé</response>
        /// <response code="404">Quand le type paiement n'a pas été trouvé</response>
        /// <response code="500">Quand il y a une erreur de serveur interne</response>
        // GET: api/TypePaiements/5
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<TypePaiement>> GetTypePaiementById(int id)
        {
            var typePaiement = await dataRepository.GetByIdAsync(id);

            if (typePaiement == null)
            {
                return NotFound();
            }

            return typePaiement;
        }

        /// <summary>
        /// Modifie (put) un type paiement
        /// </summary>
        /// <param name="id">L'id du type paiement à modifier</param>
        /// <param name="client">Le type paiement modifié</param>
        /// <returns>Réponse http</returns>
        /// <response code="204">Quand le type paiement a été modifié avec succès</response>
        /// <response code="400">Quand l'id ne correspond pas ou que le format du type paiement est incorrect</response>
        /// <response code="404">Quand le type paiement n'a pas été trouvé</response>
        /// <response code="500">Quand il y a une erreur de serveur interne</response>
        // PUT: api/TypePaiements/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> PutTypePaiement(int id, TypePaiement typePaiement)
        {
            if (id != typePaiement.TypePaiementId)
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
                await dataRepository.UpdateAsync(userToUpdate.Value, typePaiement);
                return NoContent();
            }
        }

        /// <summary>
        /// Crée (post) un nouveau type paiement
        /// </summary>
        /// <param name="typePaiement">Le type paiement à créer</param>
        /// <returns>Réponse http</returns>
        /// <response code="201">Quand le type paiement a été créé avec succès</response>
        /// <response code="400">Quand le format du type paiement dans le corps de la requête est incorrect</response>
        /// <response code="500">Quand il y a une erreur de serveur interne</response>
        // POST: api/TypePaiements
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<TypePaiement>> PostTypePaiement(TypePaiement typePaiement)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await dataRepository.AddAsync(typePaiement);

            return CreatedAtAction("GetTypePaiementById", new { id = typePaiement.TypePaiementId }, typePaiement);
        }


        /// <summary>
        /// Supprime (delete) un type paiement
        /// </summary>
        /// <param name="id">L'id du type paiement à supprimer</param>
        /// <returns>Réponse http</returns>
        /// <response code="204">Quand le type paiement a été supprimé avec succès</response>
        /// <response code="404">Quand le type paiement n'a pas été trouvé</response>
        /// <response code="500">Quand il y a une erreur de serveur interne</response>
        // DELETE: api/TypePaiements/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteTypePaiement(int id)
        {
            var typePaiement = await dataRepository.GetByIdAsync(id);

            if (typePaiement == null)
            {
                return NotFound();
            }

            await dataRepository.DeleteAsync(typePaiement.Value);

            return NoContent();
        }
    }
}
