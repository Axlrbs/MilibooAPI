


/*using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using MilibooAPI.Models.EntityFramework;
using MilibooAPI.Models.Repository;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MilibooAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VillesController : ControllerBase
    {
        private readonly IDataRepository<Ville> dataRepository;

        public VillesController(IDataRepository<Ville> dataRepo)
        {
            dataRepository = dataRepo;
        }

        /// <summary>
        /// Récupère toutes les villes
        /// </summary>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<Ville>>> GetAllVilles()
        {
            try
            {
                var villes = await dataRepository.GetAllAsync();
                return Ok(villes.Value); // Retourne la liste des villes
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erreur interne du serveur");
            }
        }

        /// <summary>
        /// Récupère une ville par son identifiant (Numéro INSEE)
        /// </summary>
        /// <param name="id">Numéro INSEE de la ville</param>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Ville>> GetVilleById(string id)
        {
            try
            {
                var ville = await dataRepository.GetByStringAsync(id);
                if (ville == null)
                    return NotFound();

                return ville; // Retourner directement l'objet Ville
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erreur interne du serveur");
            }
        }

        /// <summary>
        /// Crée une nouvelle ville
        /// </summary>
        /// <param name="ville">Objet ville à créer</param>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Ville>> PostVille(Ville ville)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                await dataRepository.AddAsync(ville);
                return CreatedAtAction(nameof(GetVilleById), new { id = ville.NumeroInsee }, ville);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erreur interne du serveur");
            }
        }

        /// <summary>
        /// Modifie une ville existante
        /// </summary>
        /// <param name="id">Numéro INSEE de la ville à modifier</param>
        /// <param name="ville">Objet avec les données mises à jour</param>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> PutVille(string id, Ville ville)
        {
            if (id != ville.NumeroInsee)
                return BadRequest();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var existing = await dataRepository.GetByStringAsync(id);
                if (existing == null)
                    return NotFound();

                await dataRepository.UpdateAsync(existing, ville);
                return NoContent();
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erreur interne du serveur");
            }
        }

        /// <summary>
        /// Supprime une ville
        /// </summary>
        /// <param name="id">Numéro INSEE de la ville à supprimer</param>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteVille(string id)
        {
            try
            {
                var ville = await dataRepository.GetByStringAsync(id);
                if (ville == null)
                    return NotFound();

                await dataRepository.DeleteAsync(ville);
                return NoContent();
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erreur interne du serveur");
            }
        }
    }
}
*/