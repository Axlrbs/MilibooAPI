using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using MilibooAPI.Models.EntityFramework;
using MilibooAPI.Models.Repository;
using MilibooAPI.Models;

namespace MilibooAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdressesController : ControllerBase
    {
        private readonly IDataRepository<Adresse> dataRepository;

        public AdressesController(IDataRepository<Adresse> dataRepo)
        {
            dataRepository = dataRepo;
        }

        /// <summary>
        /// Récupère toutes les adresses
        /// </summary>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<Adresse>>> GetAllAdresses()
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
        /// Récupère une adresse par son identifiant
        /// </summary>
        /// <param name="id">ID de l'adresse</param>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Adresse>> GetAdresseById(int id)
        {
            try
            {
                var adresse = await dataRepository.GetByIdAsync(id);
                if (adresse.Value == null)
                    return NotFound();

                return adresse;
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erreur interne du serveur");
            }
        }

        /// <summary>
        /// Récupère une adresse par sa rue
        /// </summary>
        /// <param name="rue">Nom de la rue</param>
        [HttpGet("rue/{rue}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Adresse>> GetAdresseByRue(string rue)
        {
            try
            {
                var adresse = await dataRepository.GetByStringAsync(rue);
                if (adresse.Value == null)
                    return NotFound();

                return adresse;
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erreur interne du serveur");
            }
        }

        /// <summary>
        /// Crée une nouvelle adresse
        /// </summary>
        /// <param name="adresse">Objet adresse à créer</param>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Adresse>> PostAdresse(CreateAdresseDTO adresseDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var adresse = new Adresse
            {
                NumeroInsee = adresseDTO.NumeroInsee,
                PaysId = adresseDTO.PaysId,
                Rue = adresseDTO.Rue,
                CodePostal = adresseDTO.CodePostal,
            };

            await dataRepository.AddAsync(adresse);
            return CreatedAtAction(nameof(GetAdresseById), new { id = adresse.AdresseId }, adresse);
        }

        /// <summary>
        /// Modifie une adresse existante
        /// </summary>
        /// <param name="id">ID de l'adresse à modifier</param>
        /// <param name="adresse">Objet avec les données mises à jour</param>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> PutAdresse(int id, Adresse adresse)
        {
            if (id != adresse.AdresseId)
                return BadRequest();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var existing = await dataRepository.GetByIdAsync(id);
                if (existing.Value == null)
                    return NotFound();

                await dataRepository.UpdateAsync(existing.Value, adresse);
                return NoContent();
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erreur interne du serveur");
            }
        }

        /// <summary>
        /// Supprime une adresse
        /// </summary>
        /// <param name="id">ID de l'adresse à supprimer</param>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteAdresse(int id)
        {
            try
            {
                var adresse = await dataRepository.GetByIdAsync(id);
                if (adresse.Value == null)
                    return NotFound();

                await dataRepository.DeleteAsync(adresse.Value);
                return NoContent();
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erreur interne du serveur");
            }
        }
    }
}
