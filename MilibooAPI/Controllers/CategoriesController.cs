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
    public class CategorieController : ControllerBase
    {
        //private readonly MilibooContext _context;
        private readonly IDataRepositoryCategorie dataRepositoryCategorie;

        /// <summary>
        /// Constructeur du controller
        /// </summary>
        public CategorieController(IDataRepositoryCategorie dataRepoCategorie)
        {
            dataRepositoryCategorie = dataRepoCategorie;
        }
        /*public CategorieController(MilibooDBContext context)
        {
            _context = context;
        }*/

        /// <summary>
        /// Récupère (get) toutes les catégories
        /// </summary>
        /// <returns>Réponse http</returns>
        /// <response code="200">Quand toutes les catégories ont été renvoyés avec succès</response>
        /// <response code="500">Quand il y a une erreur de serveur interne</response>
        // GET: api/Categories/GetAllCategories
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<Categorie>>> GetAllCategories()
        {
            try
            {
                return await dataRepositoryCategorie.GetAllAsync();
            }
            catch(Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erreur interne du serveur");
            }

        }

        /// <summary>
        /// Récupère (get) une catégorie par son ID
        /// </summary>
        /// <param name="id">L'id de la catégorie</param>
        /// <returns>Réponse http</returns>
        /// <response code="200">Quand la catégorie a été trouvée</response>
        /// <response code="404">Quand la catégorie n'a pas été trouvée</response>
        /// <response code="500">Quand il y a une erreur de serveur interne</response>
        // GET: api/Categories/GetCategorieById/3
        [HttpGet("[action]/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Categorie>> GetCategorieById(int id)
        {
            try
            {
                var categorie = await dataRepositoryCategorie.GetByIdAsync(id);

                Console.WriteLine($"Valeur de categorie dans GetCategorieById : {categorie}");
                if (categorie == null)
                {
                    Console.WriteLine("NotFound() est retourné !");
                    return NotFound();
                }

                return categorie;
            }
            catch(Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erreur interne du serveur");
            }
        }

        /// <summary>
        /// Récupère (get) les produits d'une catégorie par son ID
        /// </summary>
        /// <param name="id">L'id de la catégorie</param>
        /// <returns>Réponse http</returns>
        /// <response code="200">Quand la catégorie a été trouvée</response>
        /// <response code="404">Quand la catégorie n'a pas été trouvée</response>
        /// <response code="500">Quand il y a une erreur de serveur interne</response>
        // GET: api/Categories/GetProduitsByIdCategorie/5
        [HttpGet("[action]/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Categorie>> GetProduitsByIdCategorie(int id)
        {
            try
            {
                var produits = await dataRepositoryCategorie.GetProduitsByIdCategorieAsync(id);

                if (produits == null)
                {
                    return NotFound();
                }

                return produits;
            }
            catch(Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erreur interne du serveur");
            }
        }

        /// <summary>
        /// Récupère (get) les sous-catégories d'une catégorie par son ID
        /// </summary>
        /// <param name="id">L'id de la catégorie parent</param>
        /// <returns>Réponse http</returns>
        /// <response code="200">Quand la catégorie parent a été trouvée</response>
        /// <response code="404">Quand la catégorie parent n'a pas été trouvée</response>
        /// <response code="500">Quand il y a une erreur de serveur interne</response>
        // GET: api/Categories/GetCategoriesByIdCategorieParent/5
        [HttpGet("[action]/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<Categorie>>> GetCategoriesByIdCategorieParent(int id)
        {
            try
            {
                var categories = await dataRepositoryCategorie.GetCategoriesByIdCategorieParentAsync(id);

                if (categories == null)
                {
                    return NotFound();
                }

                return categories;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erreur interne du serveur");
            }
        }

        /// <summary>
        /// Récupère (get) une photo par son code
        /// </summary>
        /// <param name="codePhoto>Le code de la photo</param>
        /// <returns>Réponse http</returns>
        /// <response code="200">Quand la photo a été trouvée</response>
        /// <response code="404">Quand la photo n'a pas été trouvée</response>
        /// <response code="500">Quand il y a une erreur de serveur interne</response>
        // GET: api/Categories/8
        [HttpGet("[action]/{codePhoto}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Photo>> GetFirstPhotoByCode(string codePhoto)
        {
            try
            {
                var photo = await dataRepositoryCategorie.GetFirstPhotoByCodeAsync(codePhoto);

                if (photo == null)
                {
                    return NotFound();
                }
                return photo;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erreur interne du serveur");
            }
        }

        /// <summary>
        /// Récupère (get) une catégorie par son nom
        /// </summary>
        /// <param name="nom">Le nom de la catégorie</param>
        /// <returns>Réponse http</returns>
        /// <response code="200">Quand la catégorie a été trouvée</response>
        /// <response code="404">Quand la catégorie n'a pas été trouvée</response>
        /// <response code="500">Quand il y a une erreur de serveur interne</response>
        // GET: api/Categories/GetCategorieByNom/{nom}
        [HttpGet("[action]/{nom}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Categorie>> GetCategorieByNom(string nom)
        {
            try
            {
                var categorie = await dataRepositoryCategorie.GetByStringAsync(nom);

                if (categorie == null)
                {
                    return NotFound();
                }

                return categorie;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erreur interne du serveur");
            }
        }

        /// <summary>
        /// Modifie (put) une catégorie
        /// </summary>
        /// <param name="id">L'id de la catégorie à modifier</param>
        /// <param name="categorie">La catégorie modifié</param>
        /// <returns>Réponse http</returns>
        /// <response code="204">Quand la catégorie a été modifiée avec succès</response>
        /// <response code="400">Quand l'id ne correspond pas ou que le format de la catégorie est incorrect</response>
        /// <response code="404">Quand la catégorie n'a pas été trouvée</response>
        /// <response code="500">Quand il y a une erreur de serveur interne</response>
        // PUT: api/Categories/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> PutCategorie(int id, Categorie categorie)
        {
            if (id != categorie.CategorieId)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var opinionToUpdate = await dataRepositoryCategorie.GetByIdAsync(id);
            if (opinionToUpdate == null)
            {
                return NotFound();
            }
            else
            {
                await dataRepositoryCategorie.UpdateAsync(opinionToUpdate.Value, categorie);
                return NoContent();
            }
        }

        /// <summary>
        /// Crée (post) une nouvelle catégorie
        /// </summary>
        /// <param name="categorie">La catégorie à créer</param>
        /// <returns>Réponse http</returns>
        /// <response code="201">Quand la catégorie a été créée avec succès</response>
        /// <response code="400">Quand le format de la catégorie dans le corps de la requête est incorrect</response>
        /// <response code="500">Quand il y a une erreur de serveur interne</response>
        // POST: api/Categories
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Categorie>> PostCategorie(Categorie categorie)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await dataRepositoryCategorie.AddAsync(categorie);

            return CreatedAtAction("GetCategorieById", new { id = categorie.CategorieId }, categorie);
        }


        /// <summary>
        /// Supprime (delete) une catégorie
        /// </summary>
        /// <param name="id">L'id de la catégorie à supprimer</param>
        /// <returns>Réponse http</returns>
        /// <response code="204">Quand la catégorie a été supprimée avec succès</response>
        /// <response code="404">Quand la catégorie n'a pas été trouvée</response>
        /// <response code="500">Quand il y a une erreur de serveur interne</response>
        // DELETE: api/Categories/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteCategorie(int id)
        {
            var categorie = await dataRepositoryCategorie.GetByIdAsync(id);

            if (categorie == null)
            {
                return NotFound();
            }

            await dataRepositoryCategorie.DeleteAsync(categorie.Value);

            return NoContent();
        }
    }
}
