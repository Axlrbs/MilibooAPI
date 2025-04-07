using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MilibooAPI.Models.EntityFramework;
using MilibooAPI.Models.Repository;

namespace MilibooAPI.Models.DataManager
{
    public class LivraisonDomicileManager : IDataRepository<LivraisonDomicile>
    {
        private readonly MilibooDBContext milibooContext;

        public LivraisonDomicileManager() { }

        public LivraisonDomicileManager(MilibooDBContext context)
        {
            milibooContext = context;
        }

        public async Task<ActionResult<IEnumerable<LivraisonDomicile>>> GetAllAsync()
        {
            return await milibooContext.LivraisonDomiciles.ToListAsync();
        }

        public async Task<ActionResult<LivraisonDomicile>> GetByIdAsync(int id)
        {
            return await milibooContext.LivraisonDomiciles
                .Include(l => l.IdadresseNavigation)
                .FirstOrDefaultAsync(l => l.LivraisonId == id);
        }

        public async Task<ActionResult<LivraisonDomicile>> GetByStringAsync(string libelle)
        {
            return await milibooContext.LivraisonDomiciles
                .FirstOrDefaultAsync(l => l.Libelletypelivraison == libelle);
        }

        public async Task AddAsync(LivraisonDomicile entity)
        {
            await milibooContext.LivraisonDomiciles.AddAsync(entity);
            await milibooContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(LivraisonDomicile livraison, LivraisonDomicile entity)
        {
            milibooContext.Entry(livraison).State = EntityState.Modified;

            livraison.TypeLivraisonId = entity.TypeLivraisonId;
            livraison.Adresseid = entity.Adresseid;
            livraison.Libelletypelivraison = entity.Libelletypelivraison;
            livraison.Estexpress = entity.Estexpress;

            await milibooContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(LivraisonDomicile livraison)
        {
            milibooContext.LivraisonDomiciles.Remove(livraison);
            await milibooContext.SaveChangesAsync();
        }
    }
}
