using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using MilibooAPI.Models.EntityFramework;
using MilibooAPI.Models.Repository;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MilibooAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaysController : ControllerBase
    {
        private readonly IDataRepository<Pays> dataRepository;

        public PaysController(IDataRepository<Pays> dataRepo)
        {
            dataRepository = dataRepo;
        }

        /// <summary>
        /// Récupère tous les pays
        /// </summary>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<Pays>>> GetAllPays()
        {
            try
            {
                var paysList = await dataRepository.GetAllAsync();
                return Ok(paysList.Value);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erreur interne du serveur");
            }
        }

        /// <summary>
        /// Récupère un pays par son identifiant
        /// </summary>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Pays>> GetPaysById(string id)
        {
            try
            {
                var pays = await dataRepository.GetByStringAsync(id);
                if (pays == null)
                    return NotFound();

                return pays;
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erreur interne du serveur");
            }
        }

        /// <summary>
        /// Crée un nouveau pays
        /// </summary>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Pays>> PostPays(Pays pays)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                await dataRepository.AddAsync(pays);
                return CreatedAtAction(nameof(GetPaysById), new { id = pays.PaysId }, pays);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erreur lors de la création du pays");
            }
        }

        /// <summary>
        /// Met à jour un pays existant
        /// </summary>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> PutPays(string id, Pays pays)
        {
            if (id != pays.PaysId)
                return BadRequest();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var existingPays = await dataRepository.GetByStringAsync(id);
                if (existingPays == null)
                    return NotFound();

                await dataRepository.UpdateAsync(existingPays.Value, pays);
                return NoContent();
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erreur lors de la mise à jour du pays");
            }
        }

        /// <summary>
        /// Supprime un pays
        /// </summary>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeletePays(string id)
        {
            try
            {
                var pays = await dataRepository.GetByStringAsync(id);
                if (pays == null)
                    return NotFound();

                await dataRepository.DeleteAsync(pays.Value);
                return NoContent();
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erreur lors de la suppression du pays");
            }
        }
    }
}
