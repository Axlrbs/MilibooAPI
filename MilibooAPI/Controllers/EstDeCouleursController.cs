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
        private readonly IDataRepository<EstDeCouleur> dataRepository;
        private readonly MilibooDBContext _context; // Ajout du contexte pour pouvoir faire des requêtes plus complexes

        /// <summary>
        /// Constructeur du controller
        /// </summary>
        public EstDeCouleursController(IDataRepository<EstDeCouleur> dataRepo, MilibooDBContext context)
        {
            dataRepository = dataRepo;
            _context = context;
        }

        /// <summary>
        /// Récupère (get) tous les produits de chaque couleur avec leurs photos
        /// </summary>
        /// <returns>Réponse http</returns>
        /// <response code="200">Quand tous les produits ont été renvoyés avec succès</response>
        /// <response code="500">Quand il y a une erreur de serveur interne</response>
        // GET: api/EstDeCouleurs/GetAllEstDeCouleursWithPhotos
        [HttpGet("[action]")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<object>>> GetAllEstDeCouleursWithPhotos()
        {
            try
            {
                var estDeCouleurs = await _context.EstDeCouleurs
                    .Include(e => e.IdproduitNavigation) // Inclure les informations du produit
                    .Include(e => e.IdcolorisNavigation) // Inclure les informations de la couleur
                    .ToListAsync();

                var result = estDeCouleurs.Select(e => new {
                    e.EstDeCouleurId,
                    e.ColorisId,
                    e.ProduitId,
                    e.Codephoto,
                    e.Nomproduit,
                    e.Prixttc,
                    e.Description,
                    e.Quantite,
                    e.Promotion,
                    PhotoUrl = GetPhotoUrl(e.Codephoto), // Ajouter l'URL de la photo
                    Coloris = e.IdcolorisNavigation != null ? new
                    {
                        e.IdcolorisNavigation.ColorisId,
                        e.IdcolorisNavigation.LibelleColoris
                    } : null,
                    Produit = e.IdproduitNavigation != null ? new
                    {
                        e.IdproduitNavigation.ProduitId,
                        e.IdproduitNavigation.Reference
                    } : null
                });

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }

        /// <summary>
        /// Récupère (get) un produit par son ID avec sa photo
        /// </summary>
        /// <param name="id">L'id du produit</param>
        /// <returns>Réponse http</returns>
        /// <response code="200">Quand le produit a été trouvé</response>
        /// <response code="404">Quand le produit n'a pas été trouvé</response>
        /// <response code="500">Quand il y a une erreur de serveur interne</response>
        // GET: api/EstDeCouleurs/GetEstDeCouleurWithPhotoById/{id}
        [HttpGet("[action]/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<object>> GetEstDeCouleurWithPhotoById(int id)
        {
            try
            {
                var estDeCouleur = await _context.EstDeCouleurs
                    .Include(e => e.IdproduitNavigation)
                    .Include(e => e.IdcolorisNavigation)
                    .FirstOrDefaultAsync(e => e.EstDeCouleurId == id);

                if (estDeCouleur == null)
                {
                    return NotFound(new { message = $"EstDeCouleur avec l'ID {id} n'a pas été trouvé" });
                }

                var result = new
                {
                    estDeCouleur.EstDeCouleurId,
                    estDeCouleur.ColorisId,
                    estDeCouleur.ProduitId,
                    estDeCouleur.Codephoto,
                    estDeCouleur.Nomproduit,
                    estDeCouleur.Prixttc,
                    estDeCouleur.Description,
                    estDeCouleur.Quantite,
                    estDeCouleur.Promotion,
                    PhotoUrl = GetPhotoUrl(estDeCouleur.Codephoto),
                    Coloris = estDeCouleur.IdcolorisNavigation != null ? new
                    {
                        estDeCouleur.IdcolorisNavigation.ColorisId,
                        estDeCouleur.IdcolorisNavigation.LibelleColoris
                    } : null,
                    Produit = estDeCouleur.IdproduitNavigation != null ? new
                    {
                        estDeCouleur.IdproduitNavigation.ProduitId,
                        estDeCouleur.IdproduitNavigation.Reference
                    } : null
                };

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }

        /// <summary>
        /// Récupère l'URL de la photo à partir du code photo
        /// </summary>
        /// <param name="codePhoto">Le code de la photo</param>
        /// <returns>L'URL de la photo</returns>
        private string GetPhotoUrl(string codePhoto)
        {
            if (string.IsNullOrEmpty(codePhoto))
                return null;

            // Récupérer la photo depuis la base de données
            var photo = _context.Photos.FirstOrDefault(p => p.CodePhoto == codePhoto);

            if (photo == null)
                return null;

            // Retourner l'URL de la photo
            return $"{photo.Urlphoto}";
        }

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
        /// Récupère (get) la première ligne de chaque EstDeCouleur par ProduitId
        /// </summary>
        /// <returns>Réponse http</returns>
        /// <response code="200">Quand les premières lignes ont été renvoyées avec succès</response>
        /// <response code="500">Quand il y a une erreur de serveur interne</response>
        // GET: api/EstDeCouleurs/FirstForEachProduit
        [HttpGet("FirstForEachProduit")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<EstDeCouleur>>> GetFirstEstDeCouleurForEachProduit()
        {
            // Récupérer toutes les EstDeCouleur
            var result = await dataRepository.GetAllAsync();

            // Assure-toi que la réponse contient des données
            if (result.Value == null || !result.Value.Any())
            {
                return NoContent();
            }

            // Appliquer GroupBy sur la collection de EstDeCouleur
            var firstEstDeCouleurs = result.Value
                .GroupBy(e => e.ProduitId)  // Groupement par ProduitId
                .Select(g => g.OrderBy(e =>
                {
                    // Tenter de convertir Codephoto en entier pour un tri numérique
                    int codephotoInt;
                    return int.TryParse(e.Codephoto, out codephotoInt) ? codephotoInt : int.MaxValue; // Si ça échoue, on met une valeur très élevée
                })
                .FirstOrDefault())  // Sélectionner la première ligne (avec le Codephoto le plus bas après conversion)
                .ToList();

            // Si aucun résultat n'est trouvé après le groupement
            if (!firstEstDeCouleurs.Any())
            {
                return NoContent();
            }

            // Joindre les EstDeCouleurs avec l'URL de la photo en utilisant la méthode GetPhotoUrl
            var estDeCouleursWithPhotos = firstEstDeCouleurs
                .Select(e => new
                {
                    EstDeCouleur = e,
                    // Récupérer l'URL de la photo à l'aide de la méthode GetPhotoUrl
                    UrlPhoto = GetPhotoUrl(e.Codephoto)
                })
                .ToList();

            // Retourner les résultats avec l'URL de la photo
            return Ok(estDeCouleursWithPhotos);
        }



        /// <summary>
        /// Récupère (get) toutes les photos (URLs) associées à un ProduitId et un ColorisId spécifique, 
        /// ainsi que d'autres informations sur le produit.
        /// </summary>
        /// <param name="produitId">L'ID du produit</param>
        /// <param name="colorisId">L'ID du coloris</param>
        /// <returns>Réponse HTTP</returns>
        /// <response code="200">Quand les URLs des photos et les informations du produit ont été renvoyées avec succès</response>
        /// <response code="204">Quand aucune photo n'est trouvée</response>
        /// <response code="500">Quand il y a une erreur de serveur interne</response>
        /// GET: api/EstDeCouleurs/GetPhotosByCouleur/{produitId}/{colorisId}
        [HttpGet("GetPhotosByCouleur/{produitId}/{colorisId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<object>>> GetPhotosByCouleur(int produitId, int colorisId)
        {
            // Récupérer toutes les EstDeCouleur depuis le repository
            var result = await dataRepository.GetAllAsync();

            // Filtrer par ProduitId et ColorisId
            var filteredResult = result.Value?
                .Where(e => e.ProduitId == produitId && e.ColorisId == colorisId)
                .ToList();

            // Vérifier s'il y a des résultats
            if (filteredResult == null || !filteredResult.Any())
            {
                return NoContent();
            }

            // Récupérer et trier les informations nécessaires : URLs des photos, Description, Prixttc, Promotion, Nomproduit, Quantite
            var photosInfos = filteredResult
                .Select(x => new
                {
                    // Informations sur la photo
                    Url = GetPhotoUrl(x.Codephoto), // Utilisation de GetPhotoUrl
                    Codephoto = x.Codephoto, // Codephoto pour le tri et la référence
                    Description = x.Description,
                    Prixttc = x.Prixttc,
                    Promotion = x.Promotion,
                    Nomproduit = x.Nomproduit,
                    Quantite = x.Quantite
                })
                .Where(photo => photo.Url != null) // Supprimer les photos nulles
                .OrderBy(photo => photo.Codephoto) // Tri par Codephoto croissant
                .ToList();

            // Si aucune photo n'est trouvée
            if (!photosInfos.Any())
            {
                return NoContent();
            }

            // Retourner les informations sous forme d'objet
            return Ok(photosInfos);
        }

        /// <summary>
        /// Récupère (get) toutes les couleurs (Coloris) associées à un ProduitId spécifique.
        /// </summary>
        /// <param name="produitId">L'ID du produit</param>
        /// <returns>Réponse HTTP</returns>
        /// <response code="200">Quand les couleurs ont été renvoyées avec succès</response>
        /// <response code="404">Quand aucune couleur n'a été trouvée pour ce produit</response>
        /// <response code="500">Quand il y a une erreur de serveur interne</response>
        [HttpGet("GetCouleursByProduit/{produitId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<object>>> GetCouleursByProduit(int produitId)
        {
            var result = await _context.EstDeCouleurs
                .Where(e => e.ProduitId == produitId)
                .Include(e => e.IdcolorisNavigation) // Jointure avec Coloris
                .Select(e => new
                {
                    e.ColorisId,
                    LibelleColoris = e.IdcolorisNavigation.LibelleColoris
                })
                .Distinct()
                .ToListAsync();

            if (result == null || !result.Any())
            {
                return NotFound();
            }

            return Ok(result);
        }




        /// <summary>
        /// Modifie (put) un produit
        /// </summary>
        /// <param name="id">L'id du client à produit</param>
        /// <param name="estDeCouleur">Le produita modifié </param>
        /// <returns>Réponse http</returns>
        /// <response code="204">Quand le produit a été modifié avec succès</response>
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
    }
}