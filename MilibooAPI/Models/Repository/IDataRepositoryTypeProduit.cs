using Microsoft.AspNetCore.Mvc;
using MilibooAPI.Models.EntityFramework;

namespace MilibooAPI.Models.Repository
{
    public interface IDataRepositoryTypeProduit : IDataRepository<TypeProduit>
    {
        Task<ActionResult<Categorie>> GetProduitsByIdTypeeAsync(int id);
    }
}
