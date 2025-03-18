using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MilibooAPI.Models.EntityFramework;
using MilibooAPI.Models.Repository;

namespace MilibooAPI.Models.DataManager
{
    public class ProfessionnelManager : IDataRepository<Professionnel>
    {
        readonly MilibooDBContext? milibooContext;
        public ProfessionnelManager() { }
        public ProfessionnelManager(MilibooDBContext context)
        {
            milibooContext = context;
        }
        public async Task<ActionResult<IEnumerable<Professionnel>>> GetAllAsync()
        {
            return await milibooContext.Professionnels.ToListAsync();
        }
        public async Task<ActionResult<Professionnel>> GetByIdAsync(int id)
        {
            return await milibooContext.Professionnels.FirstOrDefaultAsync(u => u.ProfessionnelId == id);
        }
        public async Task<ActionResult<Professionnel>> GetByStringAsync(string nom)
        {
            return await milibooContext.Professionnels.FirstOrDefaultAsync(u => u.Nompersonne.ToUpper() == nom.ToUpper());
        }
        public async Task AddAsync(Professionnel entity)
        {
            await milibooContext.Professionnels.AddAsync(entity);
            await milibooContext.SaveChangesAsync();
        }
        public async Task UpdateAsync(Professionnel professionnel, Professionnel entity)
        {
            milibooContext.Entry(professionnel).State = EntityState.Modified;
            professionnel.ProfessionnelId = entity.ProfessionnelId;
            professionnel.Nompersonne = entity.Nompersonne;
            professionnel.Prenompersonne = entity.Prenompersonne;
            professionnel.Telpersonne = entity.Telpersonne;
            professionnel.Raisonsociale = entity.Raisonsociale;
            professionnel.Telfixe = entity.Telfixe;
            await milibooContext.SaveChangesAsync();
        }
        public async Task DeleteAsync(Professionnel professionnel)
        {
            milibooContext.Professionnels.Remove(professionnel);
            await milibooContext.SaveChangesAsync();
        }
    }
}
