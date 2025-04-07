using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using MilibooAPI.Models.EntityFramework;
using MilibooAPI.Models.Repository;

namespace MilibooAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LivraisonDomicilesController : ControllerBase
    {
        private readonly IDataRepository<LivraisonDomicile> dataRepository;

        public LivraisonDomicilesController(IDataRepository<LivraisonDomicile> dataRepo)
        {
            dataRepository = dataRepo;
        }

        /// <summary>
        /// Récupère toutes les livraisons à domicile
        /// </summary>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<LivraisonDomicile>>> GetAllLivraisonDomiciles()
        {
            try
            {
                return await dataRepository.GetAllAsync();
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erreur interne du serveur");
            }
        }

        /// <summary>
        /// Récupère une livraison à domicile par son identifiant
        /// </summary>
        /// <param name="id">ID de la livraison</param>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<LivraisonDomicile>> GetLivraisonDomicileById(int id)
        {
            try
            {
                var livraison = await dataRepository.GetByIdAsync(id);
                if (livraison.Value == null)
                    return NotFound();

                return livraison;
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erreur interne du serveur");
            }
        }

        /// <summary>
        /// Crée une nouvelle livraison à domicile
        /// </summary>
        /// <param name="livraison">Objet livraison à créer</param>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<LivraisonDomicile>> PostLivraisonDomicile(LivraisonDomicile livraison)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                await dataRepository.AddAsync(livraison);
                return CreatedAtAction(nameof(GetLivraisonDomicileById), new { id = livraison.LivraisonId }, livraison);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erreur interne du serveur");
            }
        }

        /// <summary>
        /// Modifie une livraison à domicile existante
        /// </summary>
        /// <param name="id">ID de la livraison à modifier</param>
        /// <param name="livraison">Objet avec les données mises à jour</param>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> PutLivraisonDomicile(int id, LivraisonDomicile livraison)
        {
            if (id != livraison.LivraisonId)
                return BadRequest();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var existing = await dataRepository.GetByIdAsync(id);
                if (existing.Value == null)
                    return NotFound();

                await dataRepository.UpdateAsync(existing.Value, livraison);
                return NoContent();
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erreur interne du serveur");
            }
        }

        /// <summary>
        /// Supprime une livraison à domicile
        /// </summary>
        /// <param name="id">ID de la livraison à supprimer</param>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteLivraisonDomicile(int id)
        {
            try
            {
                var livraison = await dataRepository.GetByIdAsync(id);
                if (livraison.Value == null)
                    return NotFound();

                await dataRepository.DeleteAsync(livraison.Value);
                return NoContent();
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erreur interne du serveur");
            }
        }
    }
}
