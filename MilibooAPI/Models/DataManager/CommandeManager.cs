using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MilibooAPI.Models.EntityFramework;
using MilibooAPI.Models.Repository;

namespace MilibooAPI.Models.DataManager
{
    public class CommandeManager : IDataRepositoryCommande
    {
        readonly MilibooDBContext? milibooContext;
        public CommandeManager() { }
        public CommandeManager(MilibooDBContext context)
        {
            milibooContext = context;
        }
        public async Task<ActionResult<IEnumerable<Commande>>> GetAllAsync()
        {
            return await milibooContext.Commandes.ToListAsync();
        }
        public async Task<ActionResult<Commande>> GetByIdAsync(int id)
        {
            return await milibooContext.Commandes.FirstOrDefaultAsync(u => u.CommandeId == id);
        }
        public async Task<ActionResult<Commande>> GetByIdClientAsync(int id)
        {
            return await milibooContext.Commandes.FirstOrDefaultAsync(u => u.ClientId == id);
        }
        public async Task<ActionResult<Commande>> GetByStringAsync(string statut)
        {
            return await milibooContext.Commandes.FirstOrDefaultAsync(u => u.Statut.ToUpper() == statut.ToUpper());
        }

        public async Task AddAsync(Commande entity)
        {
            await milibooContext.Commandes.AddAsync(entity);
            await milibooContext.SaveChangesAsync();
        }
        public async Task UpdateAsync(Commande commande, Commande entity)
        {
            milibooContext.Entry(commande).State = EntityState.Modified;
            commande.CommandeId = entity.CommandeId;
            commande.PanierId = entity.PanierId;
            commande.VirementId = entity.VirementId;
            commande.ClientId = entity.ClientId;
            commande.LivraisonId = entity.LivraisonId;
            commande.BoutiqueId = entity.BoutiqueId;
            commande.PaypalId = entity.PaypalId;
            commande.CarteBancaireId = entity.CarteBancaireId;
            commande.MontantCommande = entity.MontantCommande;
            commande.DateFacture = entity.DateFacture;
            commande.NbPointFidelite = entity.NbPointFidelite;
            commande.Statut = entity.Statut;
            await milibooContext.SaveChangesAsync();
        }
        public async Task DeleteAsync(Commande commande)
        {
            milibooContext.Commandes.Remove(commande);
            await milibooContext.SaveChangesAsync();
        }

        
    }
}
