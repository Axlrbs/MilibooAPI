﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MilibooAPI.Models.EntityFramework;
using MilibooAPI.Models.Repository;

namespace MilibooAPI.Models.DataManager
{
    public class AvisClientManager : IDataRepositoryAvisClient
    {
        readonly MilibooDBContext _milibooContext;

        public AvisClientManager(MilibooDBContext context)
        {
            _milibooContext = context;
        }

        public async Task<ActionResult<IEnumerable<AvisClient>>> GetAllAsync()
        {
            var avisClients = await _milibooContext.AvisClients.ToListAsync();
            return avisClients;
        }

        public async Task<ActionResult<AvisClient>> GetByIdAsync(int id)
        {
            var avisClient = await _milibooContext.AvisClients
                .FirstOrDefaultAsync(u => u.AvisId == id);

            if (avisClient == null)
            {
                return new NotFoundObjectResult($"Avis client avec l'ID {id} non trouvé.");
            }

            return avisClient;
        }

        public async Task<ActionResult<IEnumerable<AvisClient>>> GetAllByProduitIdAsync(int produitId)
        {
            return await _milibooContext.AvisClients.Where(u => u.ProduitId == produitId).ToListAsync();
        }




        public async Task<ActionResult<AvisClient>> GetByStringAsync(string str)
        {
            // Cette méthode n'est pas applicable pour AvisClient, mais est requise par l'interface
            return new NotFoundResult();
        }

        public async Task AddAsync(AvisClient entity)
        {
            await _milibooContext.AvisClients.AddAsync(entity);
            await _milibooContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(AvisClient entityToUpdate, AvisClient entity)
        {
            _milibooContext.Entry(entityToUpdate).State = EntityState.Modified;
            entityToUpdate.ClientId = entity.ClientId;
            entityToUpdate.ProduitId = entity.ProduitId;
            entityToUpdate.DescriptionAvis = entity.DescriptionAvis;
            entityToUpdate.NoteAvis = entity.NoteAvis;
            entityToUpdate.DateAvis = entity.DateAvis;
            entityToUpdate.TitreAvis = entity.TitreAvis;
            entityToUpdate.IdAvisParent = entity.IdAvisParent;
            await _milibooContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(AvisClient entity)
        {
            _milibooContext.AvisClients.Remove(entity);
            await _milibooContext.SaveChangesAsync();
        }
    }
}