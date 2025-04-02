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
    public class ProduitsController : ControllerBase
    {
        //private readonly MilibooContext _context;
        private readonly IDataRepository<Produit> dataRepository;

        /// <summary>
        /// Constructeur du controller
        /// </summary>
        public ProduitsController(IDataRepository<Produit> dataRepo)
        {
            dataRepository = dataRepo;
        }
        /*public ClientsController(MilibooContext context)
        {
            _context = context;
        }*/

        /// <summary>
        /// Récupère (get) tous les produits
        /// </summary>
        /// <returns>Réponse http</returns>
        /// <response code="200">Quand tous les produits ont été renvoyés avec succès</response>
        /// <response code="500">Quand il y a une erreur de serveur interne</response>
        // GET: api/Produits/GetAllProduits
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<Produit>>> GetAllProduits()
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
        /// Récupère (get) un produit par son ID
        /// </summary>
        /// <param name="id">L'id du produit</param>
        /// <returns>Réponse http</returns>
        /// <response code="200">Quand le produit a été trouvé</response>
        /// <response code="404">Quand le produit n'a pas été trouvé</response>
        /// <response code="500">Quand il y a une erreur de serveur interne</response>
        // GET: api/Produits/GetProduitById/{id}
        [HttpGet("[action]/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Produit>> GetProduitById(int id)
        {
            try
            {
                var produit = await dataRepository.GetByIdAsync(id);

                if (produit == null)
                {
                    return NotFound();
                }

                return produit;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erreur interne du serveur");
            }
            
        }

        /// <summary>
        /// Récupère (get) un produit par sa référence
        /// </summary>
        /// <param name="reference">Le nom du produit</param>
        /// <returns>Réponse http</returns>
        /// <response code="200">Quand le produit a été trouvé</response>
        /// <response code="404">Quand le produit n'a pas été trouvé</response>
        /// <response code="500">Quand il y a une erreur de serveur interne</response>
        // GET: api/Produits/GetProduitByReference/{reference}
        [HttpGet("[action]/{reference}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Produit>> GetProduitByReference(string reference)
        {
            try
            {
                var produit = await dataRepository.GetByStringAsync(reference);

                if (produit == null)
                {
                    return NotFound();
                }

                return produit;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erreur interne du serveur");
            }
        }

        /// <summary>
        /// Modifie (put) un produit
        /// </summary>
        /// <param name="id">L'id du produit à modifier</param>
        /// <param name="produit">Le produit modifié</param>
        /// <returns>Réponse http</returns>
        /// <response code="204">Quand le produit a été modifié avec succès</response>
        /// <response code="400">Quand l'id ne correspond pas ou que le format du produit est incorrect</response>
        /// <response code="404">Quand le produit n'a pas été trouvé</response>
        /// <response code="500">Quand il y a une erreur de serveur interne</response>
        // PUT: api/Produits/PutProduit/{id}
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> PutProduit(int id, Produit produit)
        {
            if (id != produit.ProduitId)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var produitToUpdate = await dataRepository.GetByIdAsync(id);
            if (produitToUpdate == null)
            {
                return NotFound();
            }
            else
            {
                await dataRepository.UpdateAsync(produitToUpdate.Value, produit);
                return NoContent();
            }
        }

        /// <summary>
        /// Crée (post) un nouveau produit
        /// </summary>
        /// <param name="produit">Le produit à créer</param>
        /// <returns>Réponse http</returns>
        /// <response code="201">Quand le produit a été créé avec succès</response>
        /// <response code="400">Quand le format du produit dans le corps de la requête est incorrect</response>
        /// <response code="500">Quand il y a une erreur de serveur interne</response>
        // POST: api/Produits/PostProduit
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Produit>> PostProduit(Produit produit)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await dataRepository.AddAsync(produit);

            return CreatedAtAction("GetProduitById", new { id = produit.ProduitId }, produit);
        }

        /// <summary>
        /// Supprime (delete) un produit
        /// </summary>
        /// <param name="id">L'id du produit à supprimer</param>
        /// <returns>Réponse http</returns>
        /// <response code="204">Quand le produit a été supprimé avec succès</response>
        /// <response code="404">Quand le produit n'a pas été trouvé</response>
        /// <response code="500">Quand il y a une erreur de serveur interne</response>
        // DELETE: api/Produits/DeleteProduit/{id}
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteProduit(int id)
        {
            var produit = await dataRepository.GetByIdAsync(id);

            if (produit == null)
            {
                return NotFound();
            }

            await dataRepository.DeleteAsync(produit.Value);

            return NoContent();
        }
    }
}
