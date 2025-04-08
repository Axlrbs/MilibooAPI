using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MilibooAPI.Models.EntityFramework;
using MilibooAPI.Models.Repository;

namespace MilibooAPI.Models.DataManager
{
    public class EstAjouteDansManager : IDataRepository<EstAjouteDans>
    {
        readonly MilibooDBContext? milibooContext;

        public EstAjouteDansManager() { }

        public EstAjouteDansManager(MilibooDBContext context)
        {
            milibooContext = context;
        }

        public async Task<ActionResult<IEnumerable<EstAjouteDans>>> GetAllAsync()
        {
            return await milibooContext.EstAjouteDans.ToListAsync();
        }

        public async Task<ActionResult<EstAjouteDans>> GetByIdAsync(int id)
        {
            return await Task.FromResult<ActionResult<EstAjouteDans>>(null!);
        }

        public async Task<ActionResult<EstAjouteDans>> GetByCompositeIdAsync(int produitId, int panierId, int colorisId)
        {
            return await milibooContext.EstAjouteDans
                .FirstOrDefaultAsync(e =>
                    e.ProduitId == produitId &&
                    e.PanierId == panierId &&
                    e.ColorisId == colorisId);
        }

        public async Task<ActionResult<EstAjouteDans>> GetByStringAsync(string unused)
        {
            // Non applicable
            return await Task.FromResult<ActionResult<EstAjouteDans>>(null!);
        }

        public async Task AddAsync(EstAjouteDans entity)
        {
            await milibooContext.EstAjouteDans.AddAsync(entity);
            await milibooContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(EstAjouteDans original, EstAjouteDans entity)
        {
            milibooContext.Entry(original).State = EntityState.Modified;
            original.Quantitepanier = entity.Quantitepanier;

            await milibooContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(EstAjouteDans entity)
        {
            milibooContext.EstAjouteDans.Remove(entity);
            await milibooContext.SaveChangesAsync();
        }
    }
}
