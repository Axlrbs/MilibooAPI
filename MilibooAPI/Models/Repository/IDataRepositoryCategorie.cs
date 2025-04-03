using Microsoft.AspNetCore.Mvc;
using MilibooAPI.Models.EntityFramework;

namespace MilibooAPI.Models.Repository
{
    public interface IDataRepositoryCategorie: IDataRepository<Categorie>
    {
        Task<ActionResult<Categorie>> GetProduitsByIdCategorieAsync(int id);
        Task<ActionResult<IEnumerable<Categorie>>> GetCategoriesByIdCategorieParentAsync(int id);
        Task<ActionResult<Photo>> GetFirstPhotoByCodeAsync(string codePhoto);
    }
}