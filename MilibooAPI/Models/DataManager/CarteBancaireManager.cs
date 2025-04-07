using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MilibooAPI.Models.EntityFramework;
using MilibooAPI.Models.Repository;

namespace MilibooAPI.Models.DataManager
{
    public class CarteBancaireManager : IDataRepository<CarteBancaire>
    {
        readonly MilibooDBContext? milibooContext;

        public CarteBancaireManager() { }

        public CarteBancaireManager(MilibooDBContext context)
        {
            milibooContext = context;
        }

        public async Task<ActionResult<IEnumerable<CarteBancaire>>> GetAllAsync()
        {
            return await milibooContext.Cartebancaires.ToListAsync();
        }

        public async Task<ActionResult<CarteBancaire>> GetByIdAsync(int id)
        {
            return await milibooContext.Cartebancaires
                .FirstOrDefaultAsync(c => c.CarteBancaireId == id);
        }

        public async Task<ActionResult<CarteBancaire>> GetByStringAsync(string numeroCarte)
        {
            return await milibooContext.Cartebancaires
                .FirstOrDefaultAsync(c => c.NumCarte == numeroCarte);
        }

        public async Task AddAsync(CarteBancaire entity)
        {
            await milibooContext.Cartebancaires.AddAsync(entity);
            await milibooContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(CarteBancaire carte, CarteBancaire entity)
        {
            milibooContext.Entry(carte).State = EntityState.Modified;
            carte.Libelletypepaiement = entity.Libelletypepaiement;
            carte.NomCarte = entity.NomCarte;
            carte.NumCarte = entity.NumCarte;
            carte.DateExpiration = entity.DateExpiration;
            carte.Cvvcarte = entity.Cvvcarte;

            await milibooContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(CarteBancaire carte)
        {
            milibooContext.Cartebancaires.Remove(carte);
            await milibooContext.SaveChangesAsync();
        }
    }
}
