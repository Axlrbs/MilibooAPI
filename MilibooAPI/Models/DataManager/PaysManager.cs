using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MilibooAPI.Models.EntityFramework;
using MilibooAPI.Models.Repository;

namespace MilibooAPI.Models.DataManager
{
    public class PaysManager : IDataRepository<Pays>
    {
        private readonly MilibooDBContext milibooContext;

        public PaysManager() { }

        public PaysManager(MilibooDBContext context)
        {
            milibooContext = context;
        }

        public async Task<ActionResult<IEnumerable<Pays>>> GetAllAsync()
        {
            return await milibooContext.Pays.ToListAsync();
        }

        public async Task<ActionResult<Pays>> GetByIdAsync(int id)
        {
            // Id de type string ici, donc on convertit en string
            return await milibooContext.Pays.FirstOrDefaultAsync(p => p.PaysId == id.ToString());
        }

        public async Task<ActionResult<Pays>> GetByStringAsync(string libelle)
        {
            return await milibooContext.Pays.FirstOrDefaultAsync(p => p.Libellepays == libelle);
        }

        public async Task AddAsync(Pays entity)
        {
            await milibooContext.Pays.AddAsync(entity);
            await milibooContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(Pays pays, Pays entity)
        {
            milibooContext.Entry(pays).State = EntityState.Modified;

            pays.Libellepays = entity.Libellepays;

            await milibooContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Pays pays)
        {
            milibooContext.Pays.Remove(pays);
            await milibooContext.SaveChangesAsync();
        }
    }
}
