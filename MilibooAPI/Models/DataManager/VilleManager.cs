using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using MilibooAPI.Models.EntityFramework;
using MilibooAPI.Models.Repository;

namespace MilibooAPI.Models.DataManager
{
    public class VilleManager : IDataRepository<Ville>
    {
        private readonly MilibooDBContext milibooContext;

        public VilleManager(MilibooDBContext context)
        {
            milibooContext = context;
        }

        public async Task<ActionResult<IEnumerable<Ville>>> GetAllAsync()
        {
        
                return await milibooContext.Villes.ToListAsync();
            
        }

        public async Task<ActionResult<Ville>> GetByIdAsync(int id) // Changer 'int' en 'string'
        {        
            return null;
        }

        public async Task<ActionResult<Ville>> GetByStringAsync(string id) // Changer 'int' en 'string'
        {
           
                return await milibooContext.Villes.FirstOrDefaultAsync(p => p.NumeroInsee == id);
        }

        public async Task AddAsync(Ville entity)
        {
            try
            {
                await milibooContext.Villes.AddAsync(entity);
                await milibooContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                // Log error (ex) if necessary
            }
        }

        public async Task UpdateAsync(Ville existingEntity, Ville updatedEntity)
        {
            try
            {
                milibooContext.Entry(existingEntity).State = EntityState.Modified;

                existingEntity.Libelleville = updatedEntity.Libelleville;

                await milibooContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                // Log error (ex) if necessary
            }
        }

        public async Task DeleteAsync(Ville entity)
        {
            try
            {
                milibooContext.Villes.Remove(entity);
                await milibooContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                // Log error (ex) if necessary
            }
        }
    }
}
