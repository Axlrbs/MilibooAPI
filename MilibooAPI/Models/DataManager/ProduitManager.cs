using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MilibooAPI.Models.EntityFramework;
using MilibooAPI.Models.Repository;
using System.Data;

namespace MilibooAPI.Models.DataManager
{
    public class ProduitManager : IDataRepository<Produit>
    {
        readonly MilibooDBContext? milibooContext;
        public ProduitManager() { }
        public ProduitManager(MilibooDBContext context)
        {
            milibooContext = context;
        }
        public async Task<ActionResult<IEnumerable<Produit>>> GetAllAsync()
        {
            return await milibooContext.Produits.ToListAsync();
        }
        public async Task<ActionResult<Produit>> GetByIdAsync(int id)
        {
            return await milibooContext.Produits.FirstOrDefaultAsync(u => u.ProduitId == id);
        }
        public async Task<ActionResult<Produit>> GetByStringAsync(string reference)
        {
            return await milibooContext.Produits.FirstOrDefaultAsync(u => u.Reference.ToUpper() == reference.ToUpper());
        }
        public async Task AddAsync(Produit entity)
        {
            await milibooContext.Produits.AddAsync(entity);
            await milibooContext.SaveChangesAsync();
        }
        public async Task UpdateAsync(Produit produit, Produit entity)
        {
            milibooContext.Entry(produit).State = EntityState.Modified;
            produit.ProduitId = entity.ProduitId;
            produit.Caracteristiquetechnique = entity.Caracteristiquetechnique;
            produit.Reference = entity.Reference;
            produit.Like = entity.Like;
            await milibooContext.SaveChangesAsync();
        }
        public async Task DeleteAsync(Produit produit)
        {
            milibooContext.Produits.Remove(produit);
            await milibooContext.SaveChangesAsync();
        }
    }
}
