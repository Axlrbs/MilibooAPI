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
    public class EstDeCouleursController : ControllerBase
    {
        //private readonly MilibooDBContext _context;
        private readonly IDataRepository<EstDeCouleur> dataRepository;

        /// <summary>
        /// Constructeur du controller
        /// </summary>
        public EstDeCouleursController(IDataRepository<EstDeCouleur> dataRepo)
        {
            dataRepository = dataRepo;
        }

        /*public EstDeCouleursController(MilibooDBContext context)
        {
            _context = context;
        }*/

        /// <summary>
        /// Récupère (get) tous les produits de chaque couleur
        /// </summary>
        /// <returns>Réponse http</returns>
        /// <response code="200">Quand tous les produits ont été renvoyés avec succès</response>
        /// <response code="500">Quand il y a une erreur de serveur interne</response>
        // GET: api/EstDeCouleurs/GetAllEstDeCouleurs
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<EstDeCouleur>>> GetAllEstDeCouleurs()
        {
            return await dataRepository.GetAllAsync();
        }

        /// <summary>
        /// Récupère (get) un produit par son ID
        /// </summary>
        /// <param name="id">L'id du produit</param>
        /// <returns>Réponse http</returns>
        /// <response code="200">Quand le produit a été trouvé</response>
        /// <response code="404">Quand le produit n'a pas été trouvé</response>
        /// <response code="500">Quand il y a une erreur de serveur interne</response>
        // GET: api/EstDeCouleurs/GetEstDeCouleurById/{id}
        [HttpGet("[action]/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<EstDeCouleur>> GetEstDeCouleurById(int id)
        {
            var estdecouleur = await dataRepository.GetByIdAsync(id);

            if (estdecouleur == null)
            {
                return NotFound();
            }

            return estdecouleur;
        }

        /// <summary>
        /// Modifie (put) un produit
        /// </summary>
        /// <param name="id">L'id du client à produit</param>
        /// <param name="estDeCouleur">Le client produit</param>
        /// <returns>Réponse http</returns>
        /// <response code="204">Quand le client a été produit avec succès</response>
        /// <response code="400">Quand l'id ne correspond pas ou que le format du produit est incorrect</response>
        /// <response code="404">Quand le produit n'a pas été trouvé</response>
        /// <response code="500">Quand il y a une erreur de serveur interne</response>
        // PUT: api/EstDeCouleurs/PutEstDeCouleur/{id}
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> PutEstDeCouleur(int id, EstDeCouleur estDeCouleur)
        {
            if (id != estDeCouleur.EstDeCouleurId)
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
                await dataRepository.UpdateAsync(userToUpdate.Value, estDeCouleur);
                return NoContent();
            }
        }

        /// <summary>
        /// Crée (post) un nouveau produit
        /// </summary>
        /// <param name="estDeCouleur">Le produit à créer</param>
        /// <returns>Réponse http</returns>
        /// <response code="201">Quand le produit a été créé avec succès</response>
        /// <response code="400">Quand le format du produit dans le corps de la requête est incorrect</response>
        /// <response code="500">Quand il y a une erreur de serveur interne</response>
        // POST: api/EstDeCouleurs/PostEstDeCouleur
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<EstDeCouleur>> PostEstDeCouleur(EstDeCouleur estDeCouleur)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await dataRepository.AddAsync(estDeCouleur);

            return CreatedAtAction("GetEstDeCouleurById", new { id = estDeCouleur.EstDeCouleurId }, estDeCouleur);
        }

        /// <summary>
        /// Supprime (delete) un produit
        /// </summary>
        /// <param name="id">L'id du produit à supprimer</param>
        /// <returns>Réponse http</returns>
        /// <response code="204">Quand le produit a été supprimé avec succès</response>
        /// <response code="404">Quand le produit n'a pas été trouvé</response>
        /// <response code="500">Quand il y a une erreur de serveur interne</response>
        // DELETE: api/EstDeCouleurs/DeleteEstDeCouleur/{id}
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteEstDeCouleur(int id)
        {
            var produit = await dataRepository.GetByIdAsync(id);

            if (produit == null)
            {
                return NotFound();
            }

            await dataRepository.DeleteAsync(produit.Value);

            return NoContent();
        }

        /*private bool EstDeCouleurExists(int id)
        {
            return _context.EstDeCouleurs.Any(e => e.EstDeCouleurId == id);
        }*/
    }
}
