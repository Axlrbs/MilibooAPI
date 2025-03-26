using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MilibooAPI.Models.EntityFramework;
using MilibooAPI.Models.Repository;

namespace MilibooAPI.Models.DataManager
{
    public class CategorieManager : IDataRepository<Categorie>
    {
        readonly MilibooDBContext? milibooContext;
        public CategorieManager() { }
        public CategorieManager(MilibooDBContext context)
        {
            milibooContext = context;
        }
        public async Task<ActionResult<IEnumerable<Categorie>>> GetAllAsync()
        {
            return await milibooContext.Categories.ToListAsync();
        }
        public async Task<ActionResult<Categorie>> GetByIdAsync(int id)
        {
            return await milibooContext.Categories.FirstOrDefaultAsync(u => u.CategorieId == id);
        }
        public async Task<ActionResult<Categorie>> GetByStringAsync(string nom)
        {
            return await milibooContext.Categories.FirstOrDefaultAsync(u => u.NomCategorie.ToUpper() == nom.ToUpper());
        }
        public async Task AddAsync(Categorie entity)
        {
            await milibooContext.Categories.AddAsync(entity);
            await milibooContext.SaveChangesAsync();
        }
        public async Task UpdateAsync(Categorie categorie, Categorie entity)
        {
            milibooContext.Entry(categorie).State = EntityState.Modified;
            categorie.CategorieId = entity.CategorieId;
            categorie.CatIdCategorie = entity.CatIdCategorie;
            categorie.NomCategorie = entity.NomCategorie;
            await milibooContext.SaveChangesAsync();
        }
        public async Task DeleteAsync(Categorie categorie)
        {
            milibooContext.Categories.Remove(categorie);
            await milibooContext.SaveChangesAsync();
        }
    }
}
