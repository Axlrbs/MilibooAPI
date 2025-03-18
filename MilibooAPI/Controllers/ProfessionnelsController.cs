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
    public class ProfessionnelsController : ControllerBase
    {
        private readonly IDataRepository<Professionnel> dataRepository;
        private readonly MilibooDBContext _context;

        /// <summary>
        /// Constructeur du controller
        /// </summary>
        public ProfessionnelsController(IDataRepository<Professionnel> dataRepo)
        {
            dataRepository = dataRepo;
        }

        /// <summary>
        /// Récupère (get) tous les professionnels
        /// </summary>
        /// <returns>Réponse http</returns>
        /// <response code="200">Quand tous les professionnels ont été renvoyés avec succès</response>
        /// <response code="500">Quand il y a une erreur de serveur interne</response>
        // GET: api/Professionnels/GetAllProfessionnels
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<Professionnel>>> GetAllProfessionnels()
        {
            return await dataRepository.GetAllAsync();
        }

        /// <summary>
        /// Récupère (get) un professionnel par son ID
        /// </summary>
        /// <param name="id">L'id du professionnel</param>
        /// <returns>Réponse http</returns>
        /// <response code="200">Quand le professionnel a été trouvé</response>
        /// <response code="404">Quand le professionnel n'a pas été trouvé</response>
        /// <response code="500">Quand il y a une erreur de serveur interne</response>
        // GET: api/Professionnels/GetProfessionnelById/{id}
        [HttpGet("[action]/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Professionnel>> GetProfessionnelById(int id)
        {
            var professionnel = await dataRepository.GetByIdAsync(id);

            if (professionnel == null)
            {
                return NotFound();
            }

            return professionnel;
        }
        /// <summary>
        /// Récupère (get) un professionnel par son nom
        /// </summary>
        /// <param name="nom">Le nom du professionnel</param>
        /// <returns>Réponse http</returns>
        /// <response code="200">Quand le professionnel a été trouvé</response>
        /// <response code="404">Quand le professionnel n'a pas été trouvé</response>
        /// <response code="500">Quand il y a une erreur de serveur interne</response>
        // GET: api/Professionnels/GetProfessionnelByNom/{nom}
        [HttpGet("[action]/{nom}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Professionnel>> GetProfessionnelByNom(string nom)
        {
            var professionnel = await dataRepository.GetByStringAsync(nom);

            if (professionnel == null)
            {
                return NotFound();
            }

            return professionnel;
        }

        /// <summary>
        /// Modifie (put) un professionnel
        /// </summary>
        /// <param name="id">L'id du professionnel à modifier</param>
        /// <param name="client">Le professionnel modifié</param>
        /// <returns>Réponse http</returns>
        /// <response code="204">Quand le professionnel a été modifié avec succès</response>
        /// <response code="400">Quand l'id ne correspond pas ou que le format du professionnel est incorrect</response>
        /// <response code="404">Quand le professionnel n'a pas été trouvé</response>
        /// <response code="500">Quand il y a une erreur de serveur interne</response>
        // PUT: api/Professionnels/PutProfessionnel/{id}
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> PutProfessionnel(int id, Professionnel professionnel)
        {
            if (id != professionnel.ProfessionnelId)
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
                await dataRepository.UpdateAsync(userToUpdate.Value, professionnel);
                return NoContent();
            }
        }

        /// <summary>
        /// Crée (post) un nouveau professionnel
        /// </summary>
        /// <param name="professionnel">Le professionnel à créer</param>
        /// <returns>Réponse http</returns>
        /// <response code="201">Quand le professionnel a été créé avec succès</response>
        /// <response code="400">Quand le format du professionnel dans le corps de la requête est incorrect</response>
        /// <response code="500">Quand il y a une erreur de serveur interne</response>
        // POST: api/Professionnels/PostProfessionnel
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Professionnel>> PostProfessionnel(Professionnel professionnel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await dataRepository.AddAsync(professionnel);

            return CreatedAtAction("GetProfessionnelById", new { id = professionnel.ProfessionnelId }, professionnel);
        }

        /// <summary>
        /// Supprime (delete) un professionnel
        /// </summary>
        /// <param name="id">L'id du professionnel à supprimer</param>
        /// <returns>Réponse http</returns>
        /// <response code="204">Quand le professionnel a été supprimé avec succès</response>
        /// <response code="404">Quand le professionnel n'a pas été trouvé</response>
        /// <response code="500">Quand il y a une erreur de serveur interne</response>
        // DELETE: api/Professionnels/DeleteProfessionnel/{id}
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteProfessionnel(int id)
        {
            var professionnel = await dataRepository.GetByIdAsync(id);

            if (professionnel == null)
            {
                return NotFound();
            }

            await dataRepository.DeleteAsync(professionnel.Value);

            return NoContent();
        }

        /*private bool ProfessionnelExists(int id)
        {
            return _context.Professionnels.Any(e => e.Idprofessionnel == id);
        }*/
    }
}
