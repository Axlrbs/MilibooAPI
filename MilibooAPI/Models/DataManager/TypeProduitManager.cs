using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MilibooAPI.Models.EntityFramework;
using MilibooAPI.Models.Repository;

namespace MilibooAPI.Models.DataManager
{
    public class TypeProduitManager : IDataRepository<TypeProduit>
    {
        readonly MilibooDBContext? milibooContext;
        public TypeProduitManager() { }
        public TypeProduitManager(MilibooDBContext context)
        {
            milibooContext = context;
        }
        public async Task<ActionResult<IEnumerable<TypeProduit>>> GetAllAsync()
        {
            return await milibooContext.TypeProduits.ToListAsync();
        }
        public async Task<ActionResult<TypeProduit>> GetByIdAsync(int id)
        {
            return await milibooContext.TypeProduits.FirstOrDefaultAsync(u => u.TypeProduitId == id);
        }
        public async Task<ActionResult<TypeProduit>> GetByStringAsync(string libelleTypeProduit)
        {
            return await milibooContext.TypeProduits.FirstOrDefaultAsync(u => u.LibelleTypeProduit.ToUpper() == libelleTypeProduit.ToUpper());
        }
        public async Task AddAsync(TypeProduit entity)
        {
            await milibooContext.TypeProduits.AddAsync(entity);
            await milibooContext.SaveChangesAsync();
        }
        public async Task UpdateAsync(TypeProduit typeProduit, TypeProduit entity)
        {
            milibooContext.Entry(typeProduit).State = EntityState.Modified;
            typeProduit.TypeProduitId = entity.TypeProduitId;
            typeProduit.LibelleTypeProduit = entity.LibelleTypeProduit;
            await milibooContext.SaveChangesAsync();
        }
        public async Task DeleteAsync(TypeProduit typeProduit)
        {
            milibooContext.TypeProduits.Remove(typeProduit);
            await milibooContext.SaveChangesAsync();
        }
    }
}
