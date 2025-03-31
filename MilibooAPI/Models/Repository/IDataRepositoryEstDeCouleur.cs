using Microsoft.AspNetCore.Mvc;
using MilibooAPI.Models.EntityFramework;

namespace MilibooAPI.Models.Repository
{
    public interface IDataRepositoryEstDeCouleur : IDataRepository<EstDeCouleur>
    {
        Task<ActionResult<IEnumerable<object>>> GetCouleursByProduit(int id);
    }

}
