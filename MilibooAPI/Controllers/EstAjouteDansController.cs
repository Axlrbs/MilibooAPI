using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using MilibooAPI.Models.EntityFramework;
using MilibooAPI.Models.Repository;
using MilibooAPI.Models.DataManager;

namespace MilibooAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EstAjouteDansController : ControllerBase
    {
        private readonly EstAjouteDansManager dataRepository;

        public EstAjouteDansController(IDataRepository<EstAjouteDans> dataRepo)
        {
            dataRepository = (EstAjouteDansManager)dataRepo;
        }

        /// <summary>
        /// Récupère tous les éléments ajoutés dans un panier
        /// </summary>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<EstAjouteDans>>> GetAllEstAjouteDans()
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
        /// Récupère un élément ajouté à un panier via la clé composite
        /// </summary>
        [HttpGet("{produitId}/{panierId}/{colorisId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<EstAjouteDans>> GetEstAjouteDans(int produitId, int panierId, int colorisId)
        {
            try
            {
                var item = await dataRepository.GetByCompositeIdAsync(produitId, panierId, colorisId);
                if (item == null || item.Value == null)
                    return NotFound();

                return item;
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erreur interne du serveur");
            }
        }

        /// <summary>
        /// Ajoute un élément à un panier
        /// </summary>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<EstAjouteDans>> PostEstAjouteDans(EstAjouteDans entity)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                await dataRepository.AddAsync(entity);
                return CreatedAtAction(nameof(GetEstAjouteDans),
                    new { produitId = entity.ProduitId, panierId = entity.PanierId, colorisId = entity.ColorisId },
                    entity);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erreur interne du serveur");
            }
        }

        /// <summary>
        /// Met à jour un élément ajouté dans un panier
        /// </summary>
        [HttpPut("{produitId}/{panierId}/{colorisId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> PutEstAjouteDans(int produitId, int panierId, int colorisId, EstAjouteDans updated)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (produitId != updated.ProduitId || panierId != updated.PanierId || colorisId != updated.ColorisId)
                return BadRequest("Clé composite invalide.");

            try
            {
                var existing = await dataRepository.GetByCompositeIdAsync(produitId, panierId, colorisId);
                if (existing.Value == null)
                    return NotFound();

                await dataRepository.UpdateAsync(existing.Value, updated);
                return NoContent();
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erreur interne du serveur");
            }
        }

        /// <summary>
        /// Supprime un élément ajouté dans un panier
        /// </summary>
        [HttpDelete("{produitId}/{panierId}/{colorisId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteEstAjouteDans(int produitId, int panierId, int colorisId)
        {
            try
            {
                var existing = await dataRepository.GetByCompositeIdAsync(produitId, panierId, colorisId);
                if (existing.Value == null)
                    return NotFound();

                await dataRepository.DeleteAsync(existing.Value);
                return NoContent();
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erreur interne du serveur");
            }
        }
    }
}
