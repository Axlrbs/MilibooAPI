using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MilibooAPI.Models.EntityFramework;
using MilibooAPI.Models.Repository;

namespace MilibooAPI.Models.DataManager
{
    public class EstDansManager : IDataRepositoryEstDans
    {
        readonly MilibooDBContext? milibooContext;
        public EstDansManager() { }
        public EstDansManager(MilibooDBContext context)
        {
            milibooContext = context;
        }
        public async Task<ActionResult<IEnumerable<EstDans>>> GetIdCategorieByIdTypeProduit(int id)
        {
            return await milibooContext.EstDans.Include(b => b.IdcategorieNavigation).Where(a => a.TypeProduitId == id).ToListAsync();
        }
    }
}
