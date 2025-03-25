using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MilibooAPI.Models.EntityFramework;
using MilibooAPI.Models.Repository;

namespace MilibooAPI.Models.DataManager
{
    public class AvisClientManager : IDataRepository<AvisClient>
    {
        readonly MilibooDBContext? milibooContext;
        public AvisClientManager() { }
        public AvisClientManager(MilibooDBContext context)
        {
            milibooContext = context;
        }
        public async Task<ActionResult<IEnumerable<AvisClient>>> GetAllAsync()
        {
            return await milibooContext.AvisClients.ToListAsync();
        }
        public async Task<ActionResult<AvisClient>> GetByIdAsync(int id)
        {
            return await milibooContext.AvisClients.FirstOrDefaultAsync(u => u.AvisId == id);
        }
        public async Task<ActionResult<AvisClient>> GetByStringAsync(string nom)
        {
            return null;
        }
        public async Task AddAsync(AvisClient entity)
        {
            await milibooContext.AvisClients.AddAsync(entity);
            await milibooContext.SaveChangesAsync();
        }
        public async Task UpdateAsync(AvisClient avisclient, AvisClient entity)
        {
            milibooContext.Entry(avisclient).State = EntityState.Modified;
            avisclient.AvisId = entity.AvisId;
            avisclient.ClientId = entity.ClientId;
            avisclient.ProduitId = entity.ProduitId;
            avisclient.DescriptionAvis = entity.DescriptionAvis;
            avisclient.NoteAvis = entity.NoteAvis;
            avisclient.DateAvis = entity.DateAvis;
            avisclient.TitreAvis = entity.TitreAvis;
            avisclient.IdAvisParent = entity.IdAvisParent;
            await milibooContext.SaveChangesAsync();
        }
        public async Task DeleteAsync(AvisClient avisclient)
        {
            milibooContext.AvisClients.Remove(avisclient);
            await milibooContext.SaveChangesAsync();
        }
    }
}
