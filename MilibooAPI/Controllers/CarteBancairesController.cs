using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MilibooAPI.Models;
using MilibooAPI.Models.EntityFramework;
using MilibooAPI.Models.Repository;

namespace MilibooAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarteBancairesController : ControllerBase
    {
        private readonly IDataRepository<CarteBancaire> dataRepository;

        public CarteBancairesController(IDataRepository<CarteBancaire> dataRepo)
        {
            dataRepository = dataRepo;
        }

        /// <summary>
        /// Récupère toutes les cartes bancaires
        /// </summary>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<CarteBancaire>>> GetAllCarteBancaires()
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
        /// Récupère une carte bancaire par son identifiant
        /// </summary>
        /// <param name="id">ID de la carte</param>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<CarteBancaire>> GetCarteBancaireById(int id)
        {
            try
            {
                var carte = await dataRepository.GetByIdAsync(id);
                if (carte.Value == null)
                    return NotFound();

                return carte;
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erreur interne du serveur");
            }
        }


        /// <summary>
        /// Crée une nouvelle carte bancaire
        /// </summary>
        /// <param name="carteDTO">Objet carte bancaire à créer</param>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<CarteBancaire>> PostCarte(CreateCarteDTO carteDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Créer une nouvelle carte bancaire à partir du DTO
            var carteBancaire = new CarteBancaire
            {
                NomCarte = carteDTO.NomCarte,
                NumCarte = carteDTO.NumCarte,
                DateExpiration = carteDTO.DateExpiration,
                Cvvcarte = carteDTO.Cvvcarte
            };

            try
            {
                // Ajouter la carte bancaire à la base de données
                await dataRepository.AddAsync(carteBancaire);
                return CreatedAtAction(nameof(GetCarteBancaireById), new { id = carteBancaire.CarteBancaireId }, carteBancaire);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erreur interne du serveur");
            }
        }


        /// <summary>
        /// Modifie une carte bancaire existante
        /// </summary>
        /// <param name="id">ID de la carte à modifier</param>
        /// <param name="carteBancaire">Données mises à jour</param>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> PutCarteBancaire(int id, CarteBancaire carteBancaire)
        {
            if (id != carteBancaire.CarteBancaireId)
                return BadRequest();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var carteToUpdate = await dataRepository.GetByIdAsync(id);
                if (carteToUpdate.Value == null)
                    return NotFound();

                await dataRepository.UpdateAsync(carteToUpdate.Value, carteBancaire);
                return NoContent();
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erreur interne du serveur");
            }
        }

        /// <summary>
        /// Supprime une carte bancaire
        /// </summary>
        /// <param name="id">ID de la carte à supprimer</param>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteCarteBancaire(int id)
        {
            try
            {
                var carte = await dataRepository.GetByIdAsync(id);
                if (carte.Value == null)
                    return NotFound();

                await dataRepository.DeleteAsync(carte.Value);
                return NoContent();
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erreur interne du serveur");
            }
        }
    }
}
