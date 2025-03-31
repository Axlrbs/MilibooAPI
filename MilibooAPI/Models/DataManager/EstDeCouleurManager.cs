using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MilibooAPI.Models.EntityFramework;
using MilibooAPI.Models.Repository;

namespace MilibooAPI.Models.DataManager
{
    public class EstDeCouleurManager: IDataRepositoryEstDeCouleur
    {
        readonly MilibooDBContext? milibooContext;
        public EstDeCouleurManager() { }
        public EstDeCouleurManager(MilibooDBContext context)
        {
            milibooContext = context;
        }
        public async Task<ActionResult<IEnumerable<EstDeCouleur>>> GetAllAsync()
        {
            return await milibooContext.EstDeCouleurs.ToListAsync();
        }
        public async Task<ActionResult<EstDeCouleur>> GetByIdAsync(int id)
        {
            return await milibooContext.EstDeCouleurs.FirstOrDefaultAsync(u => u.EstDeCouleurId == id);
        }
        public async Task<ActionResult<EstDeCouleur>> GetByStringAsync(string nom)
        {
            return await milibooContext.EstDeCouleurs.FirstOrDefaultAsync(u => u.Nomproduit.ToUpper() == nom.ToUpper());
        }

        public async Task<ActionResult<IEnumerable<object>>> GetCouleursByProduit(int produitId)
        {
            return await milibooContext.EstDeCouleurs
                .Include(e => e.IdcolorisNavigation)  
                .Where(e => e.ProduitId == produitId) 
                .Select(e => new
                {
                    e.ColorisId,      
                    LibelleColoris = e.IdcolorisNavigation.LibelleColoris, 
                })
                .Distinct()
                .ToListAsync(); 

            
        }




        public async Task AddAsync(EstDeCouleur entity)
        {
            await milibooContext.EstDeCouleurs.AddAsync(entity);
            await milibooContext.SaveChangesAsync();
        }
        public async Task UpdateAsync(EstDeCouleur utilisateur, EstDeCouleur entity)
        {
            milibooContext.Entry(utilisateur).State = EntityState.Modified;
            utilisateur.EstDeCouleurId = entity.EstDeCouleurId;
            utilisateur.ColorisId = entity.ColorisId;
            utilisateur.ProduitId = entity.ProduitId;
            utilisateur.Codephoto = entity.Codephoto;
            utilisateur.Nomproduit = entity.Nomproduit;
            utilisateur.Prixttc = entity.Prixttc;
            utilisateur.Description = entity.Description;
            utilisateur.Quantite = entity.Quantite;
            utilisateur.Promotion = entity.Promotion;
            await milibooContext.SaveChangesAsync();
        }
        public async Task DeleteAsync(EstDeCouleur produit)
        {
            milibooContext.EstDeCouleurs.Remove(produit);
            await milibooContext.SaveChangesAsync();
        }
    }
}
