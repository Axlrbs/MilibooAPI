using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MilibooAPI.Models.EntityFramework;
using MilibooAPI.Models.Repository;
using System.Data;

namespace MilibooAPI.Models.DataManager
{
    public class TypePaiementManager : IDataRepository<TypePaiement>
    {
        readonly MilibooDBContext? milibooContext;
        public TypePaiementManager() { }
        public TypePaiementManager(MilibooDBContext context)
        {
            milibooContext = context;
        }
        public async Task<ActionResult<IEnumerable<TypePaiement>>> GetAllAsync()
        {
            return await milibooContext.TypePaiements.ToListAsync();
        }
        public async Task<ActionResult<TypePaiement>> GetByIdAsync(int id)
        {
            return await milibooContext.TypePaiements.FirstOrDefaultAsync(u => u.TypePaiementId == id);
        }
        public async Task<ActionResult<TypePaiement>> GetByStringAsync(string reference)
        {
            return await milibooContext.TypePaiements.FirstOrDefaultAsync(u => u.Libelletypepaiement.ToUpper() == reference.ToUpper());
        }
        public async Task AddAsync(TypePaiement entity)
        {
            await milibooContext.TypePaiements.AddAsync(entity);
            await milibooContext.SaveChangesAsync();
        }
        public async Task UpdateAsync(TypePaiement typePaiement, TypePaiement entity)
        {
            milibooContext.Entry(typePaiement).State = EntityState.Modified;
            typePaiement.TypePaiementId = entity.TypePaiementId;
            typePaiement.Libelletypepaiement = entity.Libelletypepaiement;
            await milibooContext.SaveChangesAsync();
        }
        public async Task DeleteAsync(TypePaiement typePaiement)
        {
            milibooContext.TypePaiements.Remove(typePaiement);
            await milibooContext.SaveChangesAsync();
        }
    }
}
