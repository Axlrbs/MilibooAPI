using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MilibooAPI.Models.EntityFramework;
using MilibooAPI.Models.Repository;

namespace MilibooAPI.Models.DataManager
{
    public class AdresseManager : IDataRepository<Adresse>
    {
        private readonly MilibooDBContext milibooContext;

        public AdresseManager() { }

        public AdresseManager(MilibooDBContext context)
        {
            milibooContext = context;
        }

        public async Task<ActionResult<IEnumerable<Adresse>>> GetAllAsync()
        {
            return await milibooContext.Adresses.ToListAsync();
        }

        public async Task<ActionResult<Adresse>> GetByIdAsync(int id)
        {
            return await milibooContext.Adresses
                .FirstOrDefaultAsync(a => a.AdresseId == id);
        }

        public async Task<ActionResult<Adresse>> GetByStringAsync(string rue)
        {
            return await milibooContext.Adresses
                .FirstOrDefaultAsync(a => a.Rue == rue);
        }

        public async Task AddAsync(Adresse entity)
        {
            await milibooContext.Adresses.AddAsync(entity);
            await milibooContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(Adresse adresse, Adresse entity)
        {
            milibooContext.Entry(adresse).State = EntityState.Modified;

            adresse.NumeroInsee = entity.NumeroInsee;
            adresse.PaysId = entity.PaysId;
            adresse.Rue = entity.Rue;
            adresse.CodePostal = entity.CodePostal;

            await milibooContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Adresse adresse)
        {
            milibooContext.Adresses.Remove(adresse);
            await milibooContext.SaveChangesAsync();
        }
    }
}
