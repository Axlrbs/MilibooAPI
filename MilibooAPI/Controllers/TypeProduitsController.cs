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
    public class TypeProduitsController : ControllerBase
    {
        //private readonly MilibooContext _context;
        private readonly IDataRepository<TypeProduit> dataRepository;

        /// <summary>
        /// Constructeur du controller
        /// </summary>
        public TypeProduitsController(IDataRepository<TypeProduit> dataRepo)
        {
            dataRepository = dataRepo;
        }
        /*public ClientsController(MilibooContext context)
        {
            _context = context;
        }*/

        /// <summary>
        /// Récupère (get) tous les types produits
        /// </summary>
        /// <returns>Réponse http</returns>
        /// <response code="200">Quand tous les types produits ont été renvoyés avec succès</response>
        /// <response code="500">Quand il y a une erreur de serveur interne</response>
        // GET: api/TypeProduits/GetAllTypeProduits
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<TypeProduit>>> GetAllTypeProduits()
        {
            try
            {
                return await dataRepository.GetAllAsync();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erreur interne du serveur");
            }
        }

        /// <summary>
        /// Récupère (get) un type produit par son ID
        /// </summary>
        /// <param name="id">L'id du type produit</param>
        /// <returns>Réponse http</returns>
        /// <response code="200">Quand le type produit a été trouvé</response>
        /// <response code="404">Quand le type produit n'a pas été trouvé</response>
        /// <response code="500">Quand il y a une erreur de serveur interne</response>
        // GET: api/TypeProduits/GetTypeProduitById/{id}
        [HttpGet("[action]/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<TypeProduit>> GetTypeProduitById(int id)
        {
            try
            {
                var unTypeProduit = await dataRepository.GetByIdAsync(id);

                if (unTypeProduit == null)
                {
                    return NotFound();
                }

                return unTypeProduit;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erreur interne du serveur");
            }
            
        }

        /// <summary>
        /// Récupère (get) un type produit par son libelle
        /// </summary>
        /// <param name="libelle">Le libelle du type produit</param>
        /// <returns>Réponse http</returns>
        /// <response code="200">Quand le type produit a été trouvé</response>
        /// <response code="404">Quand le type produit n'a pas été trouvé</response>
        /// <response code="500">Quand il y a une erreur de serveur interne</response>
        // GET: api/TypeProduits/GetTypeProduitByLibelle/{libelle}
        [HttpGet("[action]/{libelle}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<TypeProduit>> GetTypeProduitByLibelle(string libelle)
        {
            try
            {
                var unTypeProduit = await dataRepository.GetByStringAsync(libelle);

                if (unTypeProduit == null)
                {
                    return NotFound();
                }

                return unTypeProduit;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erreur interne du serveur");
            }
        }

        /// <summary>
        /// Modifie (put) un type produit
        /// </summary>
        /// <param name="id">L'id du type produit à modifier</param>
        /// <param name="unTypeProduit">Le type produit modifié</param>
        /// <returns>Réponse http</returns>
        /// <response code="204">Quand le type produit a été modifié avec succès</response>
        /// <response code="400">Quand l'id ne correspond pas ou que le format du type produit est incorrect</response>
        /// <response code="404">Quand le type produit n'a pas été trouvé</response>
        /// <response code="500">Quand il y a une erreur de serveur interne</response>
        // PUT: api/TypeProduits/PutTypeProduit/{id}
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> PutTypeProduit(int id, TypeProduit unTypeProduit)
        {
            if (id != unTypeProduit.TypeProduitId)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var typeProduitToUpdate = await dataRepository.GetByIdAsync(id);
            if (typeProduitToUpdate == null)
            {
                return NotFound();
            }
            else
            {
                await dataRepository.UpdateAsync(typeProduitToUpdate.Value, unTypeProduit);
                return NoContent();
            }
        }

        /// <summary>
        /// Crée (post) un nouveau type produit
        /// </summary>
        /// <param name="unTypeProduit">Le type produit à créer</param>
        /// <returns>Réponse http</returns>
        /// <response code="201">Quand le type produit a été créé avec succès</response>
        /// <response code="400">Quand le format du type produit dans le corps de la requête est incorrect</response>
        /// <response code="500">Quand il y a une erreur de serveur interne</response>
        // POST: api/TypeProduits/PostTypeProduit
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<TypeProduit>> PostTypeProduit(TypeProduit unTypeProduit)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await dataRepository.AddAsync(unTypeProduit);

            return CreatedAtAction("GetTypeProduitById", new { id = unTypeProduit.TypeProduitId }, unTypeProduit);
        }

        /// <summary>
        /// Supprime (delete) un type produit
        /// </summary>
        /// <param name="id">L'id du type produit à supprimer</param>
        /// <returns>Réponse http</returns>
        /// <response code="204">Quand le type produit a été supprimé avec succès</response>
        /// <response code="404">Quand le type produit n'a pas été trouvé</response>
        /// <response code="500">Quand il y a une erreur de serveur interne</response>
        // DELETE: api/TypeProduits/DeleteTypeProduit/{id}
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteTypeProduit(int id)
        {
            var typeProduit = await dataRepository.GetByIdAsync(id);
            if (typeProduit == null)
            {
                return NotFound();
            }

            try
            {
                await dataRepository.DeleteAsync(typeProduit.Value);
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Impossible de supprimer l'entité car elle est référencée ailleurs.");
            }

            return NoContent();
        }
    }
}
