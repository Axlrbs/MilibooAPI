using Microsoft.AspNetCore.Mvc;
using MilibooAPI.Models.EntityFramework;

namespace MilibooAPI.Models.Repository
{
    public interface IDataRepositoryCommande : IDataRepository<Commande>
    {
        Task<ActionResult<Commande>> GetByIdClientAsync(int id);
    }
}
