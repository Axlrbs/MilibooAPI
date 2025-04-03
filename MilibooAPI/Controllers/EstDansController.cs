using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MilibooAPI.Models;
using MilibooAPI.Models.EntityFramework;
using MilibooAPI.Models.Repository;
using System.Security.Claims;

namespace MilibooAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EstDansController : ControllerBase
    {
        //private readonly MilibooContext _context;
        //private readonly IDataRepository<Client> dataRepository;
        private readonly IDataRepositoryEstDans dataRepositoryEstDans;

        /// <summary>
        /// Constructeur du controller
        /// </summary>
        public EstDansController(IDataRepositoryEstDans dataRepoEstDans)
        {

            dataRepositoryEstDans = dataRepoEstDans;
        }


        /// <summary>
        /// Récupère (get) tous les id catégorie
        /// </summary>
        /// <returns>Réponse http</returns>
        /// <response code="200">Quand tous les ids ont été renvoyés avec succès</response>
        /// <response code="500">Quand il y a une erreur de serveur interne</response>
        // GET: api/EstDans/GetIdCategorieByIdTypeProduit/5
        [HttpGet("[action]/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<EstDans>>> GetIdCategorieByIdTypeProduit(int id)
        {
            try
            {
                return await dataRepositoryEstDans.GetIdCategorieByIdTypeProduit(id);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erreur interne du serveur");
            }

        }

    }
}