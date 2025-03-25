using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MilibooAPI.Models.EntityFramework;
using MilibooAPI.Models.Repository;

namespace MilibooAPI.Models.DataManager
{
	public class PanierManager : IDataRepository<Panier>
	{
		readonly MilibooDBContext? milibooContext;
		public PanierManager() { }
		public PanierManager(MilibooDBContext context)
		{
			milibooContext = context;
		}

		public async Task<ActionResult<IEnumerable<Panier>>> GetAllAsync()
		{
			return await milibooContext.Paniers.ToListAsync();
		}

        public async Task<ActionResult<Panier>> GetByIdAsync(int clientId)
        {
            var panier = await milibooContext.Paniers
                .FirstOrDefaultAsync(p => p.ClientId == clientId);

            if (panier == null)
            {
                return new NotFoundResult(); // Retourne NotFound si le panier n'est pas trouvé
            }

            return new ActionResult<Panier>(panier); // Retourne le panier avec un statut 200 OK
        }

        public async Task<ActionResult<Panier>> GetByStringAsync(string nom)
		{
			return null;
		}

        public async Task AddAsync(Panier entity)
		{
			await milibooContext.Paniers.AddAsync(entity);
			await milibooContext.SaveChangesAsync();
		}

		public async Task UpdateAsync(Panier panier, Panier entity)
		{
			milibooContext.Entry(panier).State = EntityState.Modified;
			panier.ClientId = entity.ClientId;
			panier.Dateetheure = entity.Dateetheure;
			await milibooContext.SaveChangesAsync();
		}

		public async Task DeleteAsync(Panier panier)
		{
			milibooContext.Paniers.Remove(panier);
			await milibooContext.SaveChangesAsync();
		}
	}
}
